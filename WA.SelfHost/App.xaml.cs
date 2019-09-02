using System.Configuration;
using System.Windows;
using WA.Client;
using WA.Service;

namespace WA.SelfHost
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            // Read config
            var host = ConfigurationManager.AppSettings["host"];
            if (!int.TryParse(ConfigurationManager.AppSettings["port"], out int port) || string.IsNullOrEmpty(host))
            {
                MessageBox.Show("Invalid Config");
                Shutdown();
            }

            // Start API
            Program.StartService(host, port);

            // Start Client
            MainWindow = new MainWindow();
            base.OnStartup(e);
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Program.StopService();
            base.OnExit(e);
        }
    }
}