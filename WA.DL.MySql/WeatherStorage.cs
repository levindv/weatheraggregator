using System;
using System.Collections.Generic;
using System.Linq;
using WA.Common.DataLayer;
using WA.Common.WeatherGrabber;

namespace WA.DL.MySql
{
    public class WeatherStorage : IStorage
    {
        public List<CityInfo> GetCitiesForTomorrow(DateTime today)
        {
            // todo: вызывать нормальный метод
            return new List<CityInfo>() { "Краснодар", "Ростов", "Уфа", "Лимассол" };
        }

        public WeatherInfo GetWeatherForTomorowByCityName(string cityName)
        {
            // todo: вызывать нормальный метод
            return new WeatherInfo()
            {
                CurrDate = DateTime.Today,
                DetailedWeather = new DetailedWeather()
                {
                    WeatherByHours = new SortedDictionary<TimeSpan, HourDetails>(new List<HourDetails>()
                    {
                        new HourDetails() { Humidity = 0.1, Temperature = -1, Time = TimeSpan.FromHours(00), WindText = "1-2", IconSvg = _svg },
                        new HourDetails() { Humidity = 0.2, Temperature = +6, Time = TimeSpan.FromHours(03), WindText = "3-4", IconSvg = _svg },
                        new HourDetails() { Humidity = 0.3, Temperature = +5, Time = TimeSpan.FromHours(06), WindText = "5",   IconSvg = _svg },
                        new HourDetails() { Humidity = 0.4, Temperature = +4, Time = TimeSpan.FromHours(09), WindText = "6",   IconSvg = _svg },
                        new HourDetails() { Humidity = 0.5, Temperature = +3, Time = TimeSpan.FromHours(12), WindText = "7-8", IconSvg = _svg },
                        new HourDetails() { Humidity = 0.6, Temperature = +2, Time = TimeSpan.FromHours(15), WindText = "9",   IconSvg = _svg },
                        new HourDetails() { Humidity = 0.7, Temperature = +1, Time = TimeSpan.FromHours(18), WindText = "10",  IconSvg = _svg },
                        new HourDetails() { Humidity = 0.8, Temperature = 0, Time = TimeSpan.FromHours(21), WindText = "112",  IconSvg = _svg },
                    }.ToDictionary(d => d.Time)),
                },
            };
        }

        private const string _svg = @"<svg height=""36"" viewBox=""0 0 55 36"" width=""55"" xmlns=""http://www.w3.org/2000/svg""><g fill=""#7694b4"" fill-rule=""evenodd"" transform=""translate(6 1)""><path d=""m11.2 25.6 1.4 2.9 3.2.5-2.2 2.3.6 3.2-2.9-1.5-2.9 1.5.6-3.2-2.3-2.3 3.2-.5z""></path><path d=""m38.8 2.8 2 .3-1.4 1.4.3 2-1.8-.9-1.8.9.3-2-1.4-1.4 2-.3.9-1.8z""></path><path d=""m2.9 9 .9 1.8 2 .3-1.4 1.4.3 2-1.8-.9-1.8.9.3-2-1.4-1.4 2-.3z""></path><path d=""m40.5 26 .6470588 1.4235294 1.5529412.2588235-1.1647059 1.1647059.2588235 1.5529412-1.4235294-.7764706-1.2941176.6470588.2588235-1.5529411-1.1647059-1.1647059 1.5529412-.2588236z""></path><path d=""m23.1910436 0c-4.7101353 1.21606429-8.1910436 5.51234094-8.1910436 10.6257448 0 6.0586029 4.886719 10.970071 10.9147923 10.970071 3.7897193 0 7.1283338-1.9411914 9.0852077-4.8882254-.8706559.2247863-1.7833126.3443261-2.7237487.3443261-6.0280734 0-10.9147924-4.911468-10.9147924-10.9700709 0-2.24969032.6737796-4.34121396 1.8295847-6.0818456z""></path></g></svg>";

        public void SetCityWeather(CityInfo city, DateTime date, WeatherInfo weather)
        {
            // todo: вызывать нормальный метод
            //throw new NotImplementedException();
        }
    }
}