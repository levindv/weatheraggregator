using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;
using WA.Common.WeatherGrabber;

namespace WA.Visual.WPF
{
    public class WeatherColumnVM : INotifyPropertyChanged
    {
        private HourDetails _details;

        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherColumnVM(HourDetails details)
        {
            _details = details;
            LoadSvg(_details.IconSvg);
            Hour = _details.Time.Hours;
            Minute = _details.Time.Minutes;
            Temperature = (int)_details.Temperature;
            WindText = _details.WindText;
            Humidity = _details.Humidity;
        }

        private void LoadSvg(string svg)
        {
            // 1. Create conversion options
            var settings = new WpfDrawingSettings
            {
                IncludeRuntime = true,
                TextAsGeometry = false
            };
            // 2. Select a file to be converted
            byte[] stringBytes = Encoding.UTF8.GetBytes(svg);
            using (var sourceStream = new MemoryStream(stringBytes))
            // 3. Create a file reader
            using (var converter = new StreamSvgConverter(settings))
            // 4. convert the SVG file
            using (var resultStream = new MemoryStream())
            {
                if (converter.Convert(sourceStream, resultStream))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = resultStream;
                    bitmap.EndInit();
                    // Set the image source.
                    BitmapIcon = bitmap;
                }
            }
        }

        #region BitmapImage BitmapIcon
        public BitmapImage BitmapIcon
        {
            get { return _bitmapIcon; }
            set
            {
                if (_bitmapIcon != value)
                {
                    _bitmapIcon = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BitmapIcon)));
                }
            }
        }
        private BitmapImage _bitmapIcon;
        #endregion

        #region int Hour
        public int Hour
        {
            get { return _hour; }
            set
            {
                if (_hour != value)
                {
                    _hour = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hour)));
                }
            }
        }
        private int _hour;
        #endregion

        #region int Minute
        public int Minute
        {
            get { return _minute; }
            set
            {
                if (_minute != value)
                {
                    _minute = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Minute)));
                }
            }
        }
        private int _minute;
        #endregion

        #region int Temperature
        public int Temperature
        {
            get { return _temperature; }
            set
            {
                if (_temperature != value)
                {
                    _temperature = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Temperature)));
                }
            }
        }

        private int _temperature;
        #endregion

        #region int Temperature
        public double AvailableWidth
        {
            get { return _availableWidth; }
            set
            {
                if (_availableWidth != value)
                {
                    _availableWidth = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableWidth)));
                }
            }
        }

        private double _availableWidth;
        #endregion

        #region string WindText
        public string WindText
        {
            get { return _windText; }
            set
            {
                if (_windText != value)
                {
                    _windText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindText)));
                }
            }
        }

        private string _windText;
        #endregion

        #region double Humidity
        public double Humidity
        {
            get { return _humidity; }
            set
            {
                if (_humidity != value)
                {
                    _humidity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Humidity)));
                }
            }
        }

        private double _humidity;
        #endregion
    }
}