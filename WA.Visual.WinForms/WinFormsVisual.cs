using WA.Common.Visual;

namespace WA.Visual.WinForms
{
    public class WinFormsVisual : IVisual
    {
        public IWpfCompatible GetVisualComponent()
        {
            return new WeatherPreviewControl();
        }
    }
}