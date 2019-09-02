using System;
using System.Collections.Generic;
using WA.Common.WeatherGrabber;

namespace WA.Common.DataLayer
{
    public interface IStorage
    {
        /// <summary>
        /// Сохранить погоду для города за указанную дату
        /// </summary>
        /// <param name="city"></param>
        /// <param name="weather"></param>
        void SetCityWeather(CityInfo city, DateTime date, WeatherInfo weather);
        /// <summary>
        /// Получить информацию по указаному городу на завтра
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        WeatherInfo GetWeatherForTomorowByCityName(string cityName);
        /// <summary>
        /// Получить список городов с доступной погодой на завтра
        /// </summary>
        /// <returns></returns>
        List<CityInfo> GetCitiesForTomorrow(DateTime today);
    }
}