using System;
using System.Collections.Generic;
using WA.Common.WeatherGrabber;

namespace WA.Grabber.DotnetSpider
{
    public class Grabber : IGrabber
    {
        public List<CityInfo> GetFavoriteCities()
        {
            throw new NotImplementedException();
        }

        public WeatherInfo GetOneWeatherInfo(CityInfo city, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}