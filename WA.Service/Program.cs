using System;
using System.Threading;
using WA.Common.DataLayer;
using WA.Common.WeatherGrabber;
using WA.IOC;

namespace WA.Service
{
    public static class Program
    {
        private static Thread _bgWorker;

        static void Main(string[] args)
        {
            StartService();
            Console.ReadLine();
            StopService();
        }
        public static void StopService()
        {
            if (_bgWorker?.ThreadState.HasFlag(ThreadState.Running) ?? false)
            {
                _bgWorker.Join(1000);
            }
        }

        public static void StartService()
        {
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