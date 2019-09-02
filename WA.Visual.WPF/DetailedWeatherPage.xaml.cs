using System.Windows.Controls;
using WA.Common.Visual;
using WA.Common.WeatherGrabber;

namespace WA.Visual.WPF
{
    /// <summary>
    /// Interaction logic for DetailedWeatherPage.xaml
    /// </summary>
    public partial class DetailedWeatherPage : ContentControl, IWpfCompatible
    {
        public DetailedWeatherPage()
        {
            InitializeComponent();
        }

        public bool IsWpfCompatible => true;

        public object Control => this;

        public void ShowWeather(WeatherInfo weather)
        {
            //todo: show weather
            HWTB.Text += weather.CurrDate;
        }
    }
}