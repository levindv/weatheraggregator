using System;

namespace WA.Common.WeatherGrabber
{
    public class WeatherInfo
    {
        //public List<ShortWeather> Tabs { get; set; }

        public DateTime CurrDate { get; set; }
        public DetailedWeather DetailedWeather { get; set; }
    }
}