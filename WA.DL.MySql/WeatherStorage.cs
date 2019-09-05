using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WA.Common.DataLayer;
using WA.Common.WeatherGrabber;

namespace WA.DL.MySql
{
    public class WeatherStorage : IStorage
    {
        private readonly string _connString;

        public WeatherStorage(string connectionString)
        {
            _connString = connectionString;
        }

        public List<CityInfo> GetCitiesForTomorrow(DateTime today)
        {
            using (var cn = new MySqlConnection(_connString))
            {
                var prms = new { date = today.AddDays(1).Date };
                return cn.Query<CityInfo>("select distinct cityname as name, cityouterid as cityid from weather where day = @date", prms).ToList();
            }
        }

        public WeatherInfo GetWeatherForTomorowByCityName(string cityName, DateTime today)
        {
            using (var cn = new MySqlConnection(_connString))
            {
                var result = cn.QueryFirst<WeatherInfoInStorage>("select id, data, day from weather where cityname = @name and day = @day",
                                                                 new { name = cityName, day = today.AddDays(1).Date });
                return JsonConvert.DeserializeObject<WeatherInfo>(result.Data);
            }
        }

        public void SetCityWeather(CityInfo city, DateTime date, WeatherInfo weather)
        {
            using (var cn = new MySqlConnection(_connString))
            {
                ActualizeCity(city, cn);

                var weatherid = ActualizeWeather(city, date, weather, cn);

                ActualizeWeatherByHours(weatherid, weather, cn);
            }
        }

        private static void ActualizeCity(CityInfo city, MySqlConnection cn)
        {
            var cityid = cn.QueryFirstOrDefault<long?>("select id from cities where name = @name", new { name = city.Name });
            if (cityid.HasValue)
            {
                cn.Execute("update cities set outerid = @oid, url = @url where id = @id", new { id = cityid.Value, url = city.Url, oid = city.OuterId });
            }
            else
            {
                cn.Execute("insert into cities (name, outerid, url) values (@name, @oid, @url)", new { name = city.Name, url = city.Url, oid = city.OuterId });
            }
        }

        private static long ActualizeWeather(CityInfo city, DateTime date, WeatherInfo weather, MySqlConnection cn)
        {
            var wthrid = cn.QueryFirstOrDefault<long?>("select id from weather where cityname = @name and day = @day",
                                                        new { name = city.Name, day = date.Date });
            if (wthrid.HasValue)
            {
                cn.Execute("update weather set data = @data, day = @day, importdate = @impdt, cityouterid = @oid, cityname = @cname where id = @id",
                    new
                    {
                        data = JsonConvert.SerializeObject(weather),
                        id = wthrid.Value,
                        day = date.Date,
                        impdt = DateTime.UtcNow,
                        oid = city.OuterId,
                        cname = city.Name,
                    });
            }
            else
            {
                wthrid = cn.QueryFirst<long>("insert into weather (data, day, importdate, cityouterid, cityname) " +
                                             " values (@data, @day, @impdt, @oid, @cname); " +
                                             " SELECT LAST_INSERT_ID();",
                    new
                    {
                        data = JsonConvert.SerializeObject(weather),
                        day = date.Date,
                        impdt = DateTime.UtcNow,
                        oid = city.OuterId,
                        cname = city.Name,
                    });
            }
            return wthrid.Value;
        }

        private static void ActualizeWeatherByHours(long weatherid, WeatherInfo weather, MySqlConnection cn)
        {
            MySqlTransaction tr = null;
            try
            {
                cn.Open();
                tr = cn.BeginTransaction();
                cn.Execute("delete from weatherbyhours where weatherid = @wid;", new { wid = weatherid });

                foreach (var hw in weather.DetailedWeather.WeatherByHours.Values)
                {
                    cn.Execute("insert into weatherbyhours (weatherid, humidity, temperature, time, wind, iconsvg) values (@wid, @hm, @tmp, @tm, @wn, @svg)",
                        new { wid = weatherid, hm = hw.Humidity, tmp = hw.Temperature, tm = hw.Time, wn = hw.WindText, svg = hw.IconSvg });
                }

                tr.Commit();
                cn.Close();
            }
            catch
            {
                tr?.Rollback();
            }
        }
    }
}