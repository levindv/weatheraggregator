namespace WA.Common.WeatherGrabber
{
    public class CityInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string OuterId { get; set; }

        public static implicit operator CityInfo(string cityName)
        {
            return new CityInfo() { Name = cityName };
        }
    }
}