using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows;

namespace Hospital
{
    public partial class MyChannelControl : UserControl
    {
        public uint m_nChannelNumber = 0;
        int x, y;
        DateTime start;
        bool ff = true;
        private Timer timer;

        public string PauseTimeStr = "";



        public static MyChannelControl _MyChannelControl;

        TimeSpan _timeSpan = new TimeSpan(0, 0, 0, 0, 0);
        public MyChannelControl()
        {
            InitializeComponent();

            timer = new Timer();

         
        }

        private void MyChannelControl_Click(object sender, EventArgs e)
        {

            //if (MainWindow._mainWindow.IsFullScreen == 1)
            //{
            //    MainWindow._mainWindow.cn.Close();
            //    MainWindow._mainWindow.cn = new Control();
            //    MainWindow._mainWindow.cn.WindowStartupLocation = WindowStartupLocation.Manual;
            //    MainWindow._mainWindow.cn.Left = 200;
            //    MainWindow._mainWindow.cn.Top = 810;
            //    MainWindow._mainWindow.cn.Show();
            //}
            if (MainWindow._mainWindow.rs != null && MainWindow._mainWindow.rt != null)
            {
                MainWindow._mainWindow.rs.Topmost = true;
                MainWindow._mainWindow.rt.Topmost = true;
                //MainWindow._mainWindow.rs.Close();
                //MainWindow._mainWindow.rt.Close();

                //MainWindow._mainWindow.rs = new RecordStatus(MainWindow._mainWindow.CurrentRecordID);
                //MainWindow._mainWindow.rs.Left = 1340;
                //MainWindow._mainWindow.rs.Top = 30;
                //MainWindow._mainWindow.rs.Show();

                //MainWindow._mainWindow.rt = new RecordTime();
                //MainWindow._mainWindow.rt.Left = 60;
                //MainWindow._mainWindow.rt.Top = 30;

                //MainWindow._mainWindow.rt.Show();


            }
        }
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void MyChannelControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (MainWindow._mainWindow.IsFullScreen == 1)
            {
                MainWindow._mainWindow.cn.Opacity = 0.5;
            }
        }

        private void MyChannelControl_Load(object sender, EventArgs e)
        {
            x = MyChannelControl.MousePosition.X;
            y = MyChannelControl.MousePosition.Y;

            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (MainWindow._mainWindow.IsFullScreen == 1)
            {
                int x1 = MyChannelControl.MousePosition.X;
                int y1 = MyChannelControl.MousePosition.Y;

                if ((x == x1) && (y == y1) && ff)
                {
                    start = DateTime.Now;
                    ff = false;
                }
                if (x != x1 || y != y1)
                {
                    x = x1;
                    y = y1;
                    start = DateTime.Now;
                    ff = true;
                }
                TimeSpan ts = DateTime.Now.Subtract(start);
                if (ts.Seconds >1.5)
                {
                    MainWindow._mainWindow.cn.Opacity = 0.01;
                }
            }
            //把5改成30，就是30秒


           
        }
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0201) // WM_LBUTTONDOWN
        //    {
        //        Form1 myForm = (Form1)this.Parent;

        //        myForm.OnLButtonDown_ChannelControl(m_nChannelNumber);
        //    }

        //    base.WndProc(ref m);
        //}
    }
}
