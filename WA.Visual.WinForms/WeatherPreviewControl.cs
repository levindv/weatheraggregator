using System;
using System.Windows.Forms;
using WA.Common.Visual;
using WA.Common.WeatherGrabber;

namespace WA.Visual.WinForms
{
    public partial class WeatherPreviewControl: UserControl, IWpfCompatible
    {
        public bool IsWpfCompatible => false;

        public object Control => this;

        public WeatherPreviewControl()
        {
            InitializeComponent();
        }

        public void ShowWeather(WeatherInfo weather)
        {
            throw new NotImplementedException();
        }
    }
}