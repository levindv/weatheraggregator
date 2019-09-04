using System;
using System.Collections.Generic;

namespace WA.Common.WeatherGrabber
{
    public class DetailedWeather
    {
        public SortedDictionary<TimeSpan, HourDetails> WeatherByHours { get; set; }
    }

    public class HourDetails
    {
        public TimeSpan Time { get; set; }
        public double Temperature { get; set; }
        public string WindText { get; set; }
        public double Humidity { get; set; }
        public string IconSvg { get; set; }
    }
}