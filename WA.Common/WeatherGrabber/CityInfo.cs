using System;
using System.Collections.Generic;
using System.Text;

namespace WA.Common.WeatherGrabber
{
    public interface CityInfo
    {
        string Name { get; set; }
        string Url { get; set; }
        string CityId { get; set; }
    }
}
