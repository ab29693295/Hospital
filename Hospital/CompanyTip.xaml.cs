using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Hospital
{
    /// <summary>
    /// MessageTip.xaml 的交互逻辑
    /// </summary>
    public partial class CompanyTip : Window
    {
        public static CompanyTip _CompanyTip;
        public CompanyTip()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //System.Threading.Thread.Sleep(2000);//系统沉睡2000毫秒. 
            //this.Close();//关闭当前窗体
        }

        private static void StartTimer(int interval)
        {
            var t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, interval);
            t.Tick += Timer_Tick;
            t.IsEnabled = true;
            t.Start();
            
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
           
            //停止计时器
            ((DispatcherTimer)sender).IsEnabled = false;

            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
