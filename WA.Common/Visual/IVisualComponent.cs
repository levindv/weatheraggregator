using WA.Common.WeatherGrabber;

namespace WA.Common.Visual
{
    public interface IVisualComponent
    {
        bool IsWpfCompatible { get; }
        object Control { get; }

        void ShowWeather(WeatherInfo weather);
    }
}