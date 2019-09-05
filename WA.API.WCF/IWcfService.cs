using System;
using System.Collections.Generic;
using System.ServiceModel;
using WA.Common.WeatherGrabber;

namespace WA.API.WCF
{
    [ServiceContract]
    public interface IWcfService
    {
        [OperationContract]
        List<string> GetAvailableCitiesForTomorrow(DateTime today);

        [OperationContract]
        WeatherInfo GetWeatherInfo(string cityName, DateTime today);
    }
}
