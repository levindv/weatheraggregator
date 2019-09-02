using System;
using System.Collections.Generic;

namespace WA.Common.WeatherGrabber
{
    public interface IGrabber
    {
        ///// <summary>
        ///// Подсказка по городам с поиском
        ///// </summary>
        ///// <param name="searchString"></param>
        ///// <returns></returns>
        //List<CityInfo> GetCitiesList(string searchString);

        /// <summary>
        /// Подробная информация
        /// </summary>
        /// <param name="city"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        WeatherInfo GetOneWeatherInfo(CityInfo city, DateTime date);

        /// <summary>
        /// Получить список популярных городов с главной
        /// </summary>
        /// <returns></returns>
        List<CityInfo> GetFavoriteCities();
    }
}