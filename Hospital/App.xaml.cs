using Smart.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Hospital
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class Application : System.Windows.Application
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();
        protected override void OnStartup(StartupEventArgs e)
        {
            //hook on error before app really starts
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            base.OnStartup(e);
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //put your tracing or logging code here (I put a message box as an example)
            System.Windows.Forms.MessageBox.Show(e.ExceptionObject.ToString());
        }
        //public static Application _Application;

        //public Application()
        //{
        //    _Application = this;
        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
            string sFileFullName = btn.ToolTip.ToString();
            if (!System.IO.File.Exists(sFileFullName)) return;

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            //string file = @"c:/windows/notepad.exe"; 
            psi.Arguments = " /select," + sFileFullName;
            System.Diagnostics.Process.Start(psi);
        }
        /// <summary>
        /// 数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Controls.TextBlock btn = (System.Windows.Controls.TextBlock)sender;
            int VID = Convert.ToInt32(btn.ToolTip);

            var video = unitOfWork.DOperationVideo.GetByID(VID);
            string videoPath = video.videoPath;


            System.Diagnostics.Process.Start(videoPath);

            //System.Windows.Controls.TextBlock Tb = (System.Windows.Controls.TextBlock)sender;
            //int VID = Convert.ToInt32( Tb.ToolTip);

            //MediaPlay md = new MediaPlay(VID);
            //md.ShowDialog();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (process.MainModule.FileName == current.MainModule.FileName)
                    {
                        System.Windows.Forms.MessageBox.Show("程序已经运行", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        System.Environment.Exit(0);
                        return;
                    }
                }
            }

            //System.Diagnostics.Process.Start("cmd.exe", "regsvr32 Ry4SCom.dll");

            Home hm = new Home();
            hm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            hm.Show();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            int TempleteID = CaseManage._Casemange._templeteID;
            int count = 0;
            List<ImageModel> ImViewList = CaseManage._Casemange.ImList;
            if (ImViewList != null)
            {
                foreach (var item in ImViewList)
                {
                    bool isSelected = item.IsSelected; //要获取的状态
                    if (isSelected == true)
                    {
                        count++;
                    }
                }
            }
            if (TempleteID == 1)
            {
                if (count > 2)
                {
                    MessageTip msgMain = new MessageTip("当前模板最多只能放置两张图片！")
                    {
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };
                    msgMain.Show();

                    return;
                }
            }
            else if (TempleteID == 2)
            {
                if (count > 4)
                {
                    MessageTip msgMain = new MessageTip("当前模板最多只能放置四张图片！")
                    {
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };
                    msgMain.Show();

                    return;
                }
            }
            else if (TempleteID == 3)
            {
                if (count > 6)
                {
                    MessageTip msgMain = new MessageTip("当前模板最多只能放置六张图片！")
                    {
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };
                    msgMain.Show();

                    return;
                }
            }
        }
    }
}
