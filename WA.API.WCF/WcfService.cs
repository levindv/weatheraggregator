using System;
using System.Collections.Generic;
using System.Linq;
using WA.Common.DataLayer;
using WA.Common.WeatherGrabber;

namespace WA.API.WCF
{
    public class WcfService : IWcfService
    {
        private readonly IStorage _storage;

        public WcfService(IStorage storage)
        {
            _storage = storage;
        }

        public List<string> GetAvailableCitiesForTomorrow(DateTime today)
        {
            return _storage.GetCitiesForTomorrow(today).Select(c => c.Name).ToList();
        }

        public WeatherInfo GetWeatherInfo(string cityName)
        {
            return _storage.GetWeatherForTomorowByCityName(cityName);
        }
    }
}