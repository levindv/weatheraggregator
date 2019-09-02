using System;
using System.Collections.Generic;
using WA.Common.WeatherGrabber;

namespace WA.Common.ApiClient
{
    public interface IApiClient : IDisposable
    {
        /// <summary>
        /// Открыть соединение
        /// </summary>
        /// <param name="host">хост сервера</param>
        /// <param name="port">порт сервера</param>
        void Open(string host, int port);

        /// <summary>
        ///  Получить список городов с погодой на завтра
        /// </summary>
        /// <returns></returns>
        List<string> GetAvailableCitiesForTomorrow();
        
        /// <summary>
        /// Получить погоду по выбранному городу
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        WeatherInfo GetWeatherInfo(string cityName);
    }
}