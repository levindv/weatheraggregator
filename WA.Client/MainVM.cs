using WA.Common.ApiClient;
using WA.Common.WeatherGrabber;

namespace WA.Client
{
    public class MainVM
    {
        private readonly IApiClient _apiClient;

        public MainVM(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public WeatherInfo Search()
        {
            return _apiClient.GetWeatherInfo(SearchText);
        }

        public string SearchText { get; set; }
    }
}