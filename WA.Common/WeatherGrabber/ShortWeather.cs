namespace WA.Common.WeatherGrabber
{
    public class ShortWeather
    {
        public string DayName { get; set; }
        public double? DayHighTemp { get; set; }
        public double? NightLowTemp { get; set; }
        public string WeatherKind { get; set; }
        public string WeatherKindSvg { get; set; }
        public bool IsSelected { get; set; }

        public bool IsNowBlock { get; set; }
        public double IsNowTemp { get; set; }
        public double IsNowFeelTemp { get; set; }
    }
}