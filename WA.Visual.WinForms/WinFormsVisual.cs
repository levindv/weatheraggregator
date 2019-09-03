using WA.Common.Visual;

namespace WA.Visual.WinForms
{
    public class WinFormsVisual : IVisual
    {
        public IVisualComponent GetVisualComponent()
        {
            return new WeatherPreviewControl();
        }
    }
}