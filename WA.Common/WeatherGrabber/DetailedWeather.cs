using System;
using System.Collections.Generic;

namespace WA.Common.WeatherGrabber
{
    public class DetailedWeather
    {
        public SortedDictionary<double, HourDetails> WeatherByHours { get; set; }
    }

    public class HourDetails
    {
        public TimeSpan Time { get; set; }
        public double Temperature { get; set; }
        public double WindLow { get; set; }
        public double WindHigh { get; set; }
        public double Humidity { get; set; }
    }
}