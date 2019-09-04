using System;
using System.Configuration;
using System.Threading;
using WA.Common.API;
using WA.Common.DataLayer;
using WA.Common.WeatherGrabber;
using WA.IOC;

namespace WA.Service
{
    public static class Program
    {
        private static IApi _api;
        private static Thread _bgWorker;

        static void Main(string[] args)
        {
            var host = ConfigurationManager.AppSettings["host"];
            if (!int.TryParse(ConfigurationManager.AppSettings["port"], out int port) || string.IsNullOrEmpty(host))
            {
                Console.WriteLine("InvalidConfig");
                Console.ReadLine();
            }
            else
            {
                StartService(host, port);
                Console.ReadLine();
                StopService();
            }
        }

        public static void StopService()
        {
            _api?.UnBind();
            if (_bgWorker?.ThreadState.HasFlag(ThreadState.Running) ?? false)
            {
                _bgWorker.Join(1000);
            }
        }

        public static void StartService(string host, int port)
        {
            _api = Ioc.Resolve<IApi>();
            var storage = Ioc.Resolve<IStorage>();
            _api.Bind(host, port, storage);

            var gr = Ioc.Resolve<IGrabber>();
            var stor = Ioc.Resolve<IStorage>();
            _bgWorker = new Thread(new ThreadStart(() =>
            {
                var cities = gr.GetFavoriteCities();
                foreach (var city in cities)
                {
                    var weatherInfoList = gr.GetWeatherInfoList(city);
                    foreach (var weatherInfo in weatherInfoList)
                    {
                        stor.SetCityWeather(city, weatherInfo.CurrDate, weatherInfo);
                    }
                }

                Thread.Sleep(600_000);
            }));
            _bgWorker.IsBackground = true;
            _bgWorker.Start();
        }
    }
}