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
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Control : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        public static Control _Control;
        public Control()
        {
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            InitializeComponent();

            if (MainWindow._mainWindow._IsPauseStatus)
            {
                this.playBtn.Template = this.FindResource("FullRealPauseRecord") as ControlTemplate;
            }
            _Control = this;

        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Opacity = 0.01;
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
           
            this.Opacity = 0.5;

        }

    

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._mainWindow.ExistFullScreen();
            this.Close();
        }
        /// <summary>
        /// 开始录像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._mainWindow.BtnRecordStart_Click(sender,e);

            if (MainWindow._mainWindow._IsPauseStatus)
            {
                this.playBtn.Template = this.FindResource("FullRealPauseRecord") as ControlTemplate;
            }
            else
            {
                this.playBtn.Template = this.FindResource("FullStartRecord") as ControlTemplate;
            }
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._mainWindow.StopRecord_Click(sender, e);
            this.playBtn.Template = this.FindResource("FullStartRecord") as ControlTemplate;
        }

        private void CameraBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._mainWindow.CameraShow_Click(sender, e);
            
        }

        private void ZHiBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageTip msg1 = new MessageTip("直播功能暂未开放！");
            msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            msg1.Show();
        }

        private void TurnBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._mainWindow.TurnBtn_Click(sender, e);
        }

        private void ChangeVideoBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._mainWindow.ChangeVideoBtn_Click(sender, e);
            string DefultVideo = XmlHelper.GetValue("CurrentVideoInput");
            if (DefultVideo == "0")
            {
                Control._Control.ChangeVideoBtn.Template = this.FindResource("FullSecondVideoBtn") as ControlTemplate;
            }
            else {
                Control._Control.ChangeVideoBtn.Template = this.FindResource("FullVideoBtn") as ControlTemplate;
            }
        }
    }
}
