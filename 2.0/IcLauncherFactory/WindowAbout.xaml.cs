using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

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
            OpenUrl("http://streletzcoder.cf/");
        }
        private void OpenUrl(string url)
        {
            Process.Start(url);
        }

        private void buttonDonate_Click(object sender, RoutedEventArgs e)
        {
            OpenUrl("http://streletzcoder.cf/about/donate/");
        }
    }
}
