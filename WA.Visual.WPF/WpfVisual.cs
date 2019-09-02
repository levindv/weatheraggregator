using System;
using WA.Common.Visual;

namespace WA.Visual.WPF
{
    public class WpfVisual : IVisual
    {
        public IWpfCompatible GetVisualComponent()
        {
            return new DetailedWeatherPage();
        }
    }
}