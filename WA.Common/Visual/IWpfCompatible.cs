using WA.Common.WeatherGrabber;

namespace WA.Common.Visual
{
    public interface IWpfCompatible
    {
        bool IsWpfCompatible { get; }
        object Control { get; }

        void ShowWeather(WeatherInfo weather);
    }
}