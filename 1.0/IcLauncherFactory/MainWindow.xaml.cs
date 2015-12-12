using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;

namespace IcLauncherFactory
{
    /// <summary>
    /// Главное окно
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage bm1;
        private string fn;
        private bool imageLoaded = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void filelOad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Изображение в формате PNG|*.png";
            if (ofd.ShowDialog() == true)
            {
                //Загружаем изображение из файла
                fn = ofd.FileName;
                bm1 = new BitmapImage();
                bm1.BeginInit();
                bm1.UriSource = new Uri(fn, UriKind.RelativeOrAbsolute);
                bm1.CacheOption = BitmapCacheOption.OnLoad;
                bm1.EndInit();
                imageOriginal.Source = bm1;
                imageLoaded = true;
            }
        }

        private void GenerateIcLauncherImages(ImageSource source)
        {
            /*Генерируем изображения ic_launcher для всех нужных разрешений*/
            if (bm1 != null)
            {
                imageMdpi.Source = source;
                imageHdpi.Source = source;
                imageXdpi.Source = source;
                imageXxdpi.Source = source;
                imageXxxdpi.Source = source;
            }
            else
            {
                MessageBox.Show("Загрузите файл изображения!");
            }
        }

        private void GenerateItem_Click(object sender, RoutedEventArgs e)
        {
            GenerateIcLauncherImages(bm1);
        }

        private void ExportItem_Click(object sender, RoutedEventArgs e)
        {
            ExportImages();

        }

        private void ExportImages()
        {
            if (imageLoaded)
            {
                for (int i = 0; i <= (int)FormatList.mdpi; i++)
                {
                    SaveImages((FormatList)i);
                }
            }
            else
            {
                MessageBox.Show("Вначале необходимо сгенерировать значки!");
            }
        }

        private void SaveImages(FormatList format)
        {
            Bitmap bm = new Bitmap(fn);
            System.Drawing.Image bm2;
            //Физическое изменение исходного изображени яи сохранение файлов значков
            switch (format)
            {
                case FormatList.hdpi:
                    {
                        bm2 = bm.GetThumbnailImage(72, 72, null, IntPtr.Zero);
                        bm2.Save("hdpi.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                    break;
                case FormatList.mdpi:
                    {
                        bm2 = bm.GetThumbnailImage(48, 48, null, IntPtr.Zero);
                        bm2.Save("mdpi.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                    break;
                case FormatList.xdpi:
                    {
                        bm2 = bm.GetThumbnailImage(96, 96, null, IntPtr.Zero);
                        bm2.Save("xdpi.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                    break;
                case FormatList.xxdpi:
                    {
                        bm2 = bm.GetThumbnailImage(144, 144, null, IntPtr.Zero);
                        bm2.Save("xxdpi.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                    break;
                case FormatList.xxxdpi:
                    {
                        bm2 = bm.GetThumbnailImage(192, 192, null, IntPtr.Zero);
                        bm2.Save("xxxdpi.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                    break;
            }

        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            WindowAbout about = new WindowAbout();
            about.ShowDialog();
        }
    }
}
