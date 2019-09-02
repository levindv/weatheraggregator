using System;
using System.Collections.Generic;
using System.ServiceModel;
using WA.Common.ApiClient;
using WA.Common.WeatherGrabber;
using WA.WcfClient.WcfServiceReference;

namespace WA.WcfClient
{
    public class WcfClient : IApiClient
    {
        private WcfServiceClient client;

        public void Open(string host, int port)
        {
            var tmt = TimeSpan.FromSeconds(5);
            client = new WcfServiceClient(new BasicHttpBinding() { ReceiveTimeout = tmt, SendTimeout = tmt, OpenTimeout = tmt },
                                          new EndpointAddress($"http://{host}:{port}"));
            
            client.Open();
        }

        public List<string> GetAvailableCitiesForTomorrow()
        {
            // todo: вызывать нормальный метод
            client.GetData(123);
            return new List<string>() { "Краснодар", "Ростов", "Уфа", "Лимассол" };
        }

        public WeatherInfo GetWeatherInfo(string cityName)
        {
            // todo: вызывать нормальный метод
            client.GetDataUsingDataContractAsync(new CompositeType() { BoolValue = true, StringValue = "false" }).Wait();
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

        public void Dispose()
        {
            client?.Close();
        }
    }
}