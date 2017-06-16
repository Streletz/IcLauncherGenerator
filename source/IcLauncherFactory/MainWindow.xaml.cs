using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace IcLauncherFactory
{
    /// <summary>
    /// Главное окно
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage sourceViewBitmap;
        private string sourceFileName;
        private bool imageLoaded = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void fileLoad_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog sourseFileOpenDialog = new Microsoft.Win32.OpenFileDialog();
            sourseFileOpenDialog.Filter = "Изображение в формате PNG|*.png";
            if (sourseFileOpenDialog.ShowDialog() == true)
            {
                //Загружаем изображение из файла
                sourceFileName = sourseFileOpenDialog.FileName;
                sourceViewBitmap = new BitmapImage();
                sourceViewBitmap.BeginInit();
                sourceViewBitmap.UriSource = new Uri(sourceFileName, UriKind.RelativeOrAbsolute);
                sourceViewBitmap.CacheOption = BitmapCacheOption.OnLoad;
                sourceViewBitmap.EndInit();
                imageOriginal.Source = sourceViewBitmap;
                imageLoaded = true;
            }
        }

        private void GenerateIcLauncherImages(ImageSource source)
        {
            /*Генерируем изображения предварительного просмотра ic_launcher для всех нужных разрешений*/
            if (sourceViewBitmap != null)
            {
                imageMdpi.Source = source;
                imageHdpi.Source = source;
                imageXdpi.Source = source;
                imageXxdpi.Source = source;
                imageXxxdpi.Source = source;
            }
            else
            {
                System.Windows.MessageBox.Show("Загрузите файл изображения!");
            }
        }

        private void GenerateItem_Click(object sender, RoutedEventArgs e)
        {
            GenerateIcLauncherImages(sourceViewBitmap);
        }

        private void ExportItem_Click(object sender, RoutedEventArgs e)
        {
            ExportImages();
        }

        private void ExportImages()
        {
            FolderBrowserDialog saveFolderDialog = new FolderBrowserDialog();
            if (imageLoaded&&(saveFolderDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK))
            {                
                for (int i = 0; i <= (int)FormatList.mdpi; i++)
                {
                    SaveImages((FormatList)i, saveFolderDialog.SelectedPath + "\\");
                }
            };
            if (!imageLoaded)
            {
                System.Windows.MessageBox.Show("Вначале необходимо сгенерировать значки!");
            }
        }

        private void SaveImages(FormatList format, String path)
        {
            Bitmap sourceBitmap = new Bitmap(sourceFileName);
            System.Drawing.Image saveImage;
            try
            {               
                // Физическое изменение исходного изображения и сохранение файлов значков
                switch (format)
                {
                    case FormatList.hdpi:
                        {
                            SetCurrentDirectory(path,format);
                            saveImage = sourceBitmap.GetThumbnailImage(72, 72, null, IntPtr.Zero);
                            saveImage.Save("ic_launcher.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                        break;
                    case FormatList.mdpi:
                        {
                            SetCurrentDirectory(path, format);
                            saveImage = sourceBitmap.GetThumbnailImage(48, 48, null, IntPtr.Zero);
                            saveImage.Save("mdpi.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                        break;
                    case FormatList.xdpi:
                        {
                            SetCurrentDirectory(path, format);
                            saveImage = sourceBitmap.GetThumbnailImage(96, 96, null, IntPtr.Zero);
                            saveImage.Save("xdpi.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                        break;
                    case FormatList.xxdpi:
                        {
                            SetCurrentDirectory(path, format);
                            saveImage = sourceBitmap.GetThumbnailImage(144, 144, null, IntPtr.Zero);
                            saveImage.Save("xxdpi.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                        break;
                    case FormatList.xxxdpi:
                        {
                            SetCurrentDirectory(path, format);
                            saveImage = sourceBitmap.GetThumbnailImage(192, 192, null, IntPtr.Zero);
                            saveImage.Save("xxxdpi.png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                        break;
                }
            }
            finally
            {
                // Возврат текущей папки по умолчанию
                Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            }
        }

       /// <summary>
       /// Устанавливает текущую папку для сохранения значка в том или ином конкретном формате
       /// </summary>
       /// <param name="path">Путь к корневой папке для сохранения значков</param>
       /// <param name="format">Формат</param>       
        private static void SetCurrentDirectory(string path,FormatList format)
        {
            string formatName;
            switch (format)
            {
                case FormatList.hdpi: formatName = "hdpi";break;
                case FormatList.mdpi: formatName = "mdpi"; break;
                case FormatList.xdpi: formatName = "xhdpi"; break;
                case FormatList.xxdpi: formatName = "xxhdpi"; break;
                case FormatList.xxxdpi: formatName = "xxxhdpi"; break;
                default: formatName = "hdpi"; break;
            }            
            Directory.CreateDirectory(path + "mipmap-"+formatName+"\\");
            Environment.CurrentDirectory = path +"mipmap-"+formatName+"\\";
        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            WindowAbout about = new WindowAbout();
            about.ShowDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
