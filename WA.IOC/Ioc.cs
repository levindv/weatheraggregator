using DryIoc;
using WA.API.WCF;
using WA.Common.API;
using WA.Common.ApiClient;
using WA.Common.DataLayer;
using WA.Common.Visual;
using WA.Common.WeatherGrabber;
using WA.DL.MySql;
using WA.Visual.WPF;

namespace WA.IOC
{
    public static class Ioc
    {
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private static Container _container;

        private static Container GetContainer()
        {
            return _container ?? (_container = new Container(rules => rules.WithoutThrowOnRegisteringDisposableTransient()));
        }

        static Ioc()
        {
            var cntr = GetContainer();
            cntr.Register<IVisual, WpfVisual>();
            cntr.Register<IApi, WcfApi>();
            cntr.Register<IStorage, WeatherStorage>();
            cntr.Register<IGrabber, WA.Grabber.PhantomJS.Grabber>();
            cntr.Register<IApiClient, WcfClient.WcfClient>();
        }
    }
}