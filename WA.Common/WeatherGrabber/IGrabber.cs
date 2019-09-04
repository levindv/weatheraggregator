using System;
using System.Collections.Generic;

namespace WA.Common.WeatherGrabber
{
    public interface IGrabber
    {
        /// <summary>
        /// Подробная информация
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        List<WeatherInfo> GetWeatherInfoList(CityInfo city);

        /// <summary>
        /// Получить список популярных городов с главной
        /// </summary>
        /// <returns></returns>
        List<CityInfo> GetFavoriteCities();
    }
}