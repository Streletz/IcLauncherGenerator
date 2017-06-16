using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace IcLauncherFactory
{
    /// <summary>
    /// Окно "О Программе"
    /// </summary>
    public partial class WindowAbout : Window
    {
        public WindowAbout()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SiteLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenUrl("http://streletzcoder.ru/");
        }
        private void OpenUrl(string url)
        {
            Process.Start(url);
        }

        private void picturesLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenUrl("https://icons8.com/");
        }
    }
}
