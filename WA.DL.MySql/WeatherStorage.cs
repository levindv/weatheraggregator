using System;
using System.Collections.Generic;
using WA.Common.DataLayer;
using WA.Common.WeatherGrabber;

namespace WA.DL.MySql
{
    public class WeatherStorage : IStorage
    {
        public List<CityInfo> GetCitiesForTomorrow()
        {
            throw new NotImplementedException();
        }

        public WeatherInfo GetWeatherForTomorowByCityName(string cityName)
        {
            throw new NotImplementedException();
        }

        public void SetCityWeather(CityInfo city, DateTime date, WeatherInfo weather)
        {
            throw new NotImplementedException();
        }
    }
}