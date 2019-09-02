using System;
using System.Collections.Generic;
using System.ComponentModel;
using WA.Common.WeatherGrabber;

namespace WA.Client
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherInfo Search()
        {
            //todo: implement
            return new WeatherInfo()
            {
                CurrDate = DateTime.Today,
                Tabs = null,
                DetailedWeather = new DetailedWeather()
                {
                    WeatherByHours = new SortedDictionary<double, HourDetails>()
                    {
                        { 03, new HourDetails() { Humidity = 5, Temperature = 6, Time = TimeSpan.FromHours(03), WindLow = 3, WindHigh = 7 } },
                        { 06, new HourDetails() { Humidity = 5, Temperature = 6, Time = TimeSpan.FromHours(06), WindLow = 3, WindHigh = 7 } },
                        { 09, new HourDetails() { Humidity = 5, Temperature = 6, Time = TimeSpan.FromHours(09), WindLow = 3, WindHigh = 7 } },
                        { 12, new HourDetails() { Humidity = 5, Temperature = 6, Time = TimeSpan.FromHours(12), WindLow = 3, WindHigh = 7 } },
                        { 15, new HourDetails() { Humidity = 5, Temperature = 6, Time = TimeSpan.FromHours(15), WindLow = 3, WindHigh = 7 } },
                        { 18, new HourDetails() { Humidity = 5, Temperature = 6, Time = TimeSpan.FromHours(18), WindLow = 3, WindHigh = 7 } },
                        { 21, new HourDetails() { Humidity = 5, Temperature = 6, Time = TimeSpan.FromHours(21), WindLow = 3, WindHigh = 7 } },
                        { 00, new HourDetails() { Humidity = 5, Temperature = 6, Time = TimeSpan.FromHours(00), WindLow = 3, WindHigh = 7 } },
                    },
                },
            };
        }

        public string SearchText { get; set; }
    }
}