using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WA.Common.WeatherGrabber;

namespace WA.Grabber.PhantomJS
{
    public class Grabber : IGrabber
    {
        public List<CityInfo> GetFavoriteCities()
        {
            using (var driver = new PhantomJSDriver())
            {
                var baseUri = new Uri("https://www.gismeteo.ru/");
                driver.Navigate().GoToUrl(baseUri);

                var cities_list = driver.GetInnerElement("div", "class", "main")
                                        .GetInnerElement("div", "class", "column-wrap")
                                        .GetInnerElement("section", "class", "cities cities_frame __frame clearfix")
                                        .GetInnerElement("div", "class", "js_cities_pcities cities_section")
                                        .GetInnerElement("div", "class", "cities_list")
                                        .GetInnerElements("div", "class", "cities_item");

                List<CityInfo> result = new List<CityInfo>();
                if (cities_list != null)
                {
                    foreach (var city_div in cities_list.Where(cd => cd != null))
                    {
                        var city_url = city_div.GetInnerElementAtributeValue("a", "href");
                        if (!string.IsNullOrWhiteSpace(city_url))
                        {
                            var city_name = city_div.GetInnerElementText("span", "class", "cities_name");
                            result.Add(new CityInfo() { Name = city_name, Url = city_url, CityId = ExtractFromUrl(city_url) });
                        }
                    }
                }
                driver.Quit();
                return result;
            }
        }

        private string ExtractFromUrl(string city_url)
        {
            return city_url.Trim('/').Split('-').LastOrDefault();
        }

        private static string[] _days = { "tomorrow", "3-day", "4-day", "5-day", "6-day", "7-day", "8-day", "9-day", "10-day" };

        public List<WeatherInfo> GetWeatherInfoList(CityInfo city)
        {
            List<WeatherInfo> result = new List<WeatherInfo>();
            using (var driver = new PhantomJSDriver())
            {
                foreach (var day in _days)
                {
                    var baseUri = new Uri(city.Url);
                    driver.Navigate().GoToUrl(new Uri(baseUri, day));

                    var culture = driver.GetInnerElementAtributeValue("body", "class").Split(' ').Skip(2).FirstOrDefault();
                    var baseNode = driver.GetInnerElement("div", "class", "main")
                                         .GetInnerElement("div", "class", "column-wrap")
                                         .GetInnerElement("div", "class", "__frame_sm")
                                         .GetInnerElement("div", "class", "forecast_frame hw_wrap");

                    var baseDate = baseNode.GetInnerElement("div", "class", "tab  tooltip")
                                       .GetInnerElement("div", "class", "tab_wrap")
                                       .GetInnerElement("div", "class", "tab-content");

                    var date = (baseDate.GetInnerElementText("div", "class", "date ") ??
                                baseDate.GetInnerElementText("div", "class", "date weekend"))
                                       ?.Split(',')
                                        .LastOrDefault()
                                       ?.Trim();

                    if (string.IsNullOrWhiteSpace(date))
                    {
                        continue;
                    }

                    var detailsNode = baseNode.GetInnerElement("div", "class", "widget js_widget")
                                              .GetInnerElement("div", "class", "widget__body")
                                              .GetInnerElement("div", "class", "widget__container");

                    var hours = detailsNode.GetInnerElement("div", "class", "widget__row widget__row_time")
                                           .GetInnerElements("div", "class", "widget__item");

                    var icons = detailsNode.GetInnerElement("div", "class", "widget__row widget__row_table widget__row_icon")
                                           .GetInnerElements("div", "class", "widget__item");

                    var temps = detailsNode.GetInnerElement("div", "class", "widget__row widget__row_table widget__row_temperature")
                                           .GetInnerElement("div", "class", "templine w_temperature")
                                           .GetInnerElement("div", "class", "chart chart__temperature")
                                           .GetInnerElement("div", "class", "values")
                                           .GetInnerElements("div", "class", "value");

                    var winds = detailsNode.GetInnerElement("div", "class", "widget__row widget__row_table widget__row_wind-or-gust")
                                           .GetInnerElements("div", "class", "widget__item");

                    var precs = detailsNode.GetInnerElement("div", "class", "widget__row widget__row_table widget__row_precipitation")
                                           .GetInnerElements("div", "class", "widget__item");

                    var maxlen = new[] { hours.Count, icons.Count, temps.Count, winds.Count, precs.Count }.Max();

                    var wlist = new List<HourDetails>(Enumerable.Range(0, maxlen).Select(r => new HourDetails()));

                    for (int i = 0; i < maxlen; i++)
                    {
                        var hr = hours.ByInd(i);
                        if (hr != null)
                        {
                            var h = hr.GetInnerElement("div", "class", "w_time").FindElement(By.TagName("span")).Text;
                            var m = hr.GetInnerElement("div", "class", "w_time").FindElement(By.TagName("sup")).Text;
                            if (int.TryParse(h, out var hi) && int.TryParse(m, out var mi))
                            {
                                wlist[i].Time = TimeSpan.FromMinutes(hi * 60 + mi);
                            }
                        }

                        var ic = icons.ByInd(i);
                        if (ic != null)
                        {
                            var svg = ic.GetInnerElement("div", "class", "widget__value w_icon")
                                        .GetInnerElement("span", "class", "tooltip")
                                        .GetAttribute("innerHTML"); // Сакральные знания...
                            wlist[i].IconSvg = svg;
                        }

                        var tm = temps.ByInd(i);
                        if (tm != null)
                        {
                            var t = tm.GetInnerElementText("span", "class", "unit unit_temperature_c");
                            if (double.TryParse(t, out var td))
                            {
                                wlist[i].Temperature = td;
                            }
                        }

                        var wn = winds.ByInd(i);
                        if (wn != null)
                        {
                            var w = wn.GetInnerElement("div", "class", "w_wind")
                                      .GetInnerElement("div", "class", "w_wind__warning w_wind__warning_ ")
                                      .GetInnerElementText("span", "class", "unit unit_wind_m_s");
                            wlist[i].WindText = w;
                        }

                        var pc = precs.ByInd(i);
                        if (pc != null)
                        {
                            var t = pc.GetInnerElement("div", "class", "w_prec")
                                      .GetInnerElementText("div", "class", "w_prec__value");
                            if (double.TryParse(t, out var td))
                            {
                                wlist[i].Humidity = td;
                            }
                        }
                    }

                    result.Add(new WeatherInfo()
                    {
                        CurrDate =  DateTime.ParseExact(date, new[] { "d MMM", "d MMM y", "d MMM yyyy" }, CultureInfo.GetCultureInfo(culture), DateTimeStyles.None),
                        DetailedWeather = new DetailedWeather() { WeatherByHours = new SortedDictionary<TimeSpan, HourDetails>(wlist.ToDictionary(x => x.Time)) },
                    }) ;
                }
                driver.Quit();
                return result;
            }
        }
    }
}