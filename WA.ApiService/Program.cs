using System;
using System.Configuration;
using WA.Common.API;
using WA.Common.DataLayer;
using WA.IOC;

namespace WA.ApiService
{
    public static class Program
    {
        private static IApi _api;

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
        }

        public static void StartService(string host, int port)
        {
            _api = Ioc.Resolve<IApi>();
            var storage = Ioc.Resolve<IStorage>();
            _api.Bind(host, port, storage);
        }
    }
}