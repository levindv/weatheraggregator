using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WA.Common.API;

namespace WA.API.WCF
{
    public class WcfApi : IApi
    {
        public ServiceHost config;
        public void Bind(string host, int port)
        {
            config = new ServiceHost(typeof(WcfService), new Uri($"http://{host}:{port}"));

            // Enable "Add Service Reference" support   
            config.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            //// set up support for http, https, net.tcp, net.pipe   
            //config.EnableProtocol(new BasicHttpBinding());
            //// add an extra BasicHttpBinding endpoint at http:///basic   
            //config.AddServiceEndpoint(typeof(IWcfService), new BasicHttpBinding(), "basic");
            // concurrencyMode
            var behavior = config.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            behavior.ConcurrencyMode = ConcurrencyMode.Multiple;
            behavior.UseSynchronizationContext = false;

            config.Open();
        }

        public void UnBind()
        {
            config?.Close();
        }
    }
}