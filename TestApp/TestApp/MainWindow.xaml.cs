using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartNavigation_Click(object sender, RoutedEventArgs e)
        {
            NavigateToUrl();
        }

        private void NavigateToUrl()
        {
            if (string.IsNullOrEmpty(Path.Text))
                return;

            string path = Path.Text;
            if (!path.StartsWith("https://") && !path.StartsWith("http://"))
                path = $"http://{path}";

            Browser.Source = new Uri(path);
        }

        private void StopNavigation_Click(object sender, RoutedEventArgs e)
        {
            Browser.Stop();
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            Browser.Refresh();
        }

        private void Browser_NavigationCompleted(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlNavigationCompletedEventArgs e)
        {
            Path.Text = e.Uri.AbsoluteUri;       
        }

        private void Path_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NavigateToUrl();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoBack)
                Browser.GoBack();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoForward)
                Browser.GoForward();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (Browser != null)
            {
                Browser.Stop();
                Browser.Dispose();
            }
        }
    }
}
