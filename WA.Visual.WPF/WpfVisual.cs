using WA.Common.Visual;

namespace WA.Visual.WPF
{
    public class WpfVisual : IVisual
    {
        public IVisualComponent GetVisualComponent()
        {
            return new DetailedWeatherPage(new DetailedWeatherVM());
        }
    }
}