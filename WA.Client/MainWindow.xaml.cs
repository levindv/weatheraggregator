using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using WA.Common.Visual;
using WA.IOC;

namespace WA.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainVM MainVM { get; }

        private readonly UIElement _weatherControl;
        private readonly IWpfCompatible _wcInterface;

        public MainWindow()
        {
            InitializeComponent();

            MainVM = new MainVM();

            var wc = Ioc.Resolve<IVisual>().GetVisualComponent();
            _wcInterface = wc;
            if (wc.IsWpfCompatible)
            {
                _weatherControl = (UIElement)wc.Control;
            }
            else
            {
                _weatherControl = new WindowsFormsHost() { Child = (System.Windows.Forms.Control)wc.Control };
            }
            Body.Content = _weatherControl;
        }


        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                StartSearch();
            }
        }

        private void StartSearch()
        {
            Dispatcher.Invoke(() =>
            {
                var searchResult = MainVM.Search();
                _wcInterface.ShowWeather(searchResult);
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartSearch();
        }
    }
}
