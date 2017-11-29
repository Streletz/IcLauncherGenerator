using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace IcLauncherFactory
{
    /// <summary>
    /// Главное окно
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage _sourceViewBitmap;
        private string _sourceFileName;       
        private bool _imagesGenerated = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Закрытие программы с помощью. меню.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Загрузка исходного изображения (обработчик меню и панели инструментов).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileLoad_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog sourseFileOpenDialog = new Microsoft.Win32.OpenFileDialog();
            sourseFileOpenDialog.Filter = "Изображение в формате PNG|*.png";
            if (sourseFileOpenDialog.ShowDialog() == true)
            {
                //Загружаем изображение из файла
                try
                {
                    FileLoad(sourseFileOpenDialog.FileName);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Загрузка исходного изображения.
        /// </summary>
        /// <param name="fileName">Путь к файлу изображения.</param>
        private void FileLoad(string fileName)
        {
            // Проверка по расширению.
            string extensionPattern = @".*\.png";
            Regex extensionValidator = new Regex(extensionPattern);
            if (!extensionValidator.IsMatch(fileName.ToLower()))
            {
                throw new UnsupportedFormatExeption(Properties.Resources.UnsupportedFormatExceptionMessage);
            }
            // Собственно загрузка.
            try
            {
                _sourceFileName = fileName;
                _sourceViewBitmap = new BitmapImage();
                _sourceViewBitmap.BeginInit();
                _sourceViewBitmap.UriSource = new Uri(_sourceFileName, UriKind.RelativeOrAbsolute);
                _sourceViewBitmap.CacheOption = BitmapCacheOption.OnLoad;
                _sourceViewBitmap.EndInit();
                imageOriginal.Source = _sourceViewBitmap;               
            }
            catch
            {
                throw new UnsupportedFormatExeption(Properties.Resources.UnsupportedFormatExceptionMessage);
            }
        }

        /// <summary>
        /// Генерация изображений для предварительного просмотра.
        /// </summary>
        /// <param name="source">Исходное изображение.</param>
        private void GenerateIcLauncherImages(ImageSource source)
        {
            /*Генерируем изображения предварительного просмотра ic_launcher для всех нужных разрешений*/
            if (_sourceViewBitmap != null)
            {
                imageMdpi.Source = source;
                imageHdpi.Source = source;
                imageXdpi.Source = source;
                imageXxdpi.Source = source;
                imageXxxdpi.Source = source;
                _imagesGenerated = true;
            }
            else
            {
                System.Windows.MessageBox.Show(Properties.Resources.LoadImageMessage);
            }
        }

        private void GenerateItem_Click(object sender, RoutedEventArgs e)
        {
            GenerateIcLauncherImages(_sourceViewBitmap);
        }

        /// <summary>
        /// Сохранение гтовых изображений (обработчик меню и панели инструментов).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportItem_Click(object sender, RoutedEventArgs e)
        {
            ExportImages();
        }

        /// <summary>
        /// Выбор папки для сохранения готовых изображений и собственно их сохранение.
        /// </summary>
        private void ExportImages()
        {
            if (!_imagesGenerated)
            {
                System.Windows.MessageBox.Show(Properties.Resources.ImageGenerationExpextedMessage);
            }
            else
            {
                FolderBrowserDialog saveFolderDialog = new FolderBrowserDialog();
                if (saveFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    for (int i = 0; i <= (int)FormatList.mdpi; i++)
                    {
                        SaveImages((FormatList)i, saveFolderDialog.SelectedPath + "\\");
                    }
                };
            }


        }

        /// <summary>
        /// Сохранение готовых изображений.
        /// </summary>
        /// <param name="format">Перечень форматов.</param>
        /// <param name="path">Путь к папке для солхранения.</param>
        private void SaveImages(FormatList format, String path)
        {
            Bitmap sourceBitmap = new Bitmap(_sourceFileName);
            System.Drawing.Image saveImage;
            try
            {
                // Физическое изменение исходного изображения и сохранение файлов значков
                switch (format)
                {
                    case FormatList.hdpi:
                        {
                            SetCurrentDirectory(path, format);
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
        private static void SetCurrentDirectory(string path, FormatList format)
        {
            string formatName;
            switch (format)
            {
                case FormatList.hdpi: formatName = "hdpi"; break;
                case FormatList.mdpi: formatName = "mdpi"; break;
                case FormatList.xdpi: formatName = "xhdpi"; break;
                case FormatList.xxdpi: formatName = "xxhdpi"; break;
                case FormatList.xxxdpi: formatName = "xxxhdpi"; break;
                default: formatName = "hdpi"; break;
            }
            Directory.CreateDirectory(path + "mipmap-" + formatName + "\\");
            Environment.CurrentDirectory = path + "mipmap-" + formatName + "\\";
        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            WindowAbout about = new WindowAbout();
            about.ShowDialog();
        }        

        /// <summary>
        /// Загрузка исходного изображения с помощью Drag&Drop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageOriginal_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] filePaths = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                try
                {
                    FileLoad(filePaths[0]);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Запрос на подтверждение закрытия программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseDialog dialog = new CloseDialog();
            dialog.ShowDialog();
            e.Cancel = !dialog.DialogResult ?? false;
        }
    }
}
