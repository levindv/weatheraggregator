using System.Windows;
using System.Windows.Controls;
using WA.Common.Visual;
using WA.Common.WeatherGrabber;

namespace WA.Visual.WPF
{
    /// <summary>
    /// Interaction logic for DetailedWeatherPage.xaml
    /// </summary>
    public partial class DetailedWeatherPage : ContentControl, IVisualComponent
    {
        private readonly DetailedWeatherVM _detailedWeatherVM;

        public DetailedWeatherPage(DetailedWeatherVM detailedWeatherVM)
        {
            _detailedWeatherVM = detailedWeatherVM;
            DataContext = _detailedWeatherVM;

            InitializeComponent();
        }

        public bool IsWpfCompatible => true;

        public object Control => this;

        public void ShowWeather(WeatherInfo weather)
        {
            _detailedWeatherVM.LoadWeather(weather);
        }

        private void ContentControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!double.IsNaN(this.ActualWidth))
            {
                _detailedWeatherVM.ControlWidth = this.ActualWidth;
            }
        }
    }
}