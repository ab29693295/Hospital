using QCAP.NET;
using Smart.Service;
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
using System.Windows.Shapes;

namespace Hospital
{
    /// <summary>
    /// Seting.xaml 的交互逻辑
    /// </summary>
    public partial class ShowImage : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        private string _videoPath;
        private int _VID;
        public ShowImage(int VID)
        {
            InitializeComponent();
            _VID = VID;
            var video = unitOfWork.DOperationImage.GetByID(_VID);
            _videoPath = video.imagePath;
            BitmapImage imagetemp = new BitmapImage(new Uri(_videoPath, UriKind.Absolute));

            MainImage.Source = imagetemp;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
   
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rect rc = SystemParameters.WorkArea;//获取当前工作区大小  
            //wpf最大化 全屏显示任务栏处理
            if (this.WindowState == WindowState.Normal)
            {

                this.WindowState = WindowState.Maximized;

                titleLabel.Width = rc.Width - 45;
                MainImage.Width = rc.Width - 80;
                MainImage.Height = rc.Height - 90;
            }
            else
            {
                titleLabel.Width = 700 - 45;
                MainImage.Width = 620;
                MainImage.Height = 550 - 90;
                this.WindowState = WindowState.Normal;


            }
        }
    }
}
