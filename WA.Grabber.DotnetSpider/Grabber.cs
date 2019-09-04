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

        public List<WeatherInfo> GetWeatherInfoList(CityInfo city)
        {
            throw new NotImplementedException();
        }
    }
}