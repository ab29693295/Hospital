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
using System.Xml.Linq;

namespace Hospital
{
    /// <summary>
    /// RecordManage.xaml 的交互逻辑
    /// </summary>
    public partial class DoctorManage : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        private static readonly string XmlPath = System.Windows.Forms.Application.StartupPath + "\\CheckAPP.config";//网站配置xml

        public static DoctorManage _DoctorManage;


        public DoctorManage()
        {
            InitializeComponent();
            _DoctorManage = this;
            AddList();
           
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
      
        /// <summary>
        /// 删除单子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string userName = btn.Tag.ToString();

                DeleteSure msg = new DeleteSure(2, userName);
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.Show();

              
              
            }
            MainWindow._mainWindow.opeDoctor_Com.ItemsSource = XmlHelper.GetStoreUsers();
            AddList();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            //设置医生列表
            XmlHelper.SetUsers(this.UserName_Txt.Text.ToString());

            MainWindow._mainWindow.opeDoctor_Com.ItemsSource = XmlHelper.GetStoreUsers();
            if (Operation._Operation != null)
            {
                Operation._Operation.opeDoctor_Com.ItemsSource = XmlHelper.GetStoreUsers();
            }
            //MessageTip msg = new MessageTip("添加成功！");
            //msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //msg.Show();
            AddList();
        }

        public void AddList()
        {
            //加载手术医生
            List<string> OperaDoctors = XmlHelper.GetStoreUsers();
            List<OperationName> Doctors = new List<OperationName>();
            if (OperaDoctors != null)
            {
                foreach (var o in OperaDoctors)
                {
                    if (o != "")
                    {
                        OperationName doctor = new OperationName();
                        doctor.Name = o;
                        Doctors.Add(doctor);
                    }
                }

            }
            this.RecordDG.ItemsSource = Doctors;
        }
    }
}
