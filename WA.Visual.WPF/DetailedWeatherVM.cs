using System.ComponentModel;
using System.Linq;
using System.Windows;
using WA.Common.WeatherGrabber;

namespace WA.Visual.WPF
{
    public class DetailedWeatherVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadWeather(WeatherInfo weatherInfo)
        {
            if (weatherInfo != null)
            {
                var vmCollectionEnum = weatherInfo.DetailedWeather.WeatherByHours
                                                                  .OrderBy(w => w.Key)
                                                                  .Select(w => new WeatherColumnVM(w.Value));
                WeatherByHours = vmCollectionEnum.ToArray();
                UpdateColumnsWidth();
                var anyWater = weatherInfo.DetailedWeather.WeatherByHours.Any(w => w.Value.Humidity > 0);
                WaterVisibility = anyWater ? Visibility.Visible : Visibility.Collapsed;
                NoWaterVisibility = anyWater ? Visibility.Collapsed : Visibility.Visible;
                DataVisibility = Visibility.Visible;
            }
        }

        #region WeatherColumnVM[] WeatherByHours
        public WeatherColumnVM[] WeatherByHours
        {
            get { return _weatherByHours; }
            set
            {
                if (_weatherByHours != value)
                {
                    _weatherByHours = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WeatherByHours)));
                }
            }
        }
        private WeatherColumnVM[] _weatherByHours;
        #endregion

        #region double ControlWidth
        public double ControlWidth
        {
            get { return _controlWidth; }
            set
            {
                if (_controlWidth != value)
                {
                    _controlWidth = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ControlWidth)));
                    UpdateColumnsWidth();
                }
            }
        }

        private double _controlWidth;
        #endregion

        private void UpdateColumnsWidth()
        {
            var awidth = WeatherByHours == null || double.IsNaN(ControlWidth)
                ? 50
                : ControlWidth / WeatherByHours.Length - 0.1;

            if (WeatherByHours != null)
            {
                foreach (var col in WeatherByHours)
                {
                    col.AvailableWidth = awidth;
                }
            }
        }

        #region Visibility WaterVisibility
        public Visibility WaterVisibility
        {
            get { return _waterVisibility; }
            set
            {
                if (_waterVisibility != value)
                {
                    _waterVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WaterVisibility)));
                }
            }
        }

        private Visibility _waterVisibility;
        #endregion

        #region Visibility NoWaterVisibility
        public Visibility NoWaterVisibility
        {
            get { return _noWaterVisibility; }
            set
            {
                if (_noWaterVisibility != value)
                {
                    _noWaterVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoWaterVisibility)));
                }
            }
        }

        private Visibility _noWaterVisibility;
        #endregion

        #region Visibility DataVisibility
        public Visibility DataVisibility
        {
            get { return _dataVisibility; }
            set
            {
                if (_dataVisibility != value)
                {
                    _dataVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DataVisibility)));
                }
            }
        }

        private Visibility _dataVisibility = Visibility.Collapsed;
        #endregion
    }
}