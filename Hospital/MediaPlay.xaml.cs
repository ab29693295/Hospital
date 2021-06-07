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
    public partial class MediaPlay : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        private string _videoPath;
        private int _VID;
        public MediaPlay(int VID)
        {
            InitializeComponent();
            _VID = VID;
         
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
   
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Source = new Uri(_videoPath);

            MediaPlayer.Play();
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Position = MediaPlayer.Position + TimeSpan.FromSeconds(20);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Position = MediaPlayer.Position - TimeSpan.FromSeconds(20);
        }

        private void MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            // Get the lenght of the video
            int duration = MediaPlayer.NaturalDuration.TimeSpan.Seconds;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var video = unitOfWork.DOperationVideo.GetByID(_VID);
            _videoPath = video.videoPath;
            MediaPlayer.Source = new Uri(_videoPath);
            MediaPlayer.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rect rc = SystemParameters.WorkArea;//获取当前工作区大小  
            //wpf最大化 全屏显示任务栏处理
            if (this.WindowState == WindowState.Normal)
            {

                this.WindowState = WindowState.Maximized;
                titleLabel.Width = rc.Width - 45;
                MediaPlayer.Width = rc.Width - 80;
                MediaPlayer.Height = rc.Height - 90;
            }
            else
            {
                titleLabel.Width = 700 - 45;
                MediaPlayer.Width = 620;
                MediaPlayer.Height = 550 - 90;
                this.WindowState = WindowState.Normal;


            }
        }
    }
}
