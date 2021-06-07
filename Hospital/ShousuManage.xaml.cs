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
    public partial class ShousuManage : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        private static readonly string XmlPath = System.Windows.Forms.Application.StartupPath + "\\CheckAPP.config";//网站配置xml

        public static ShousuManage _ShousuManage;


        public ShousuManage()
        {
            InitializeComponent();
            _ShousuManage = this;
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

                DeleteSure msg = new DeleteSure(3, userName);
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.Show();
               
              
            }
            MainWindow._mainWindow.operName_Com.ItemsSource = XmlHelper.GetOperations();
            AddList();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
         
            //设置手术名称
            XmlHelper.StoreOperations(this.UserName_Txt.Text.ToString());
    
            MainWindow._mainWindow.operName_Com.ItemsSource = XmlHelper.GetOperations();
            if (Operation._Operation != null)
            {
                Operation._Operation.operName_Com.ItemsSource = XmlHelper.GetOperations();
            }
            //MessageTip msg = new MessageTip("添加成功！");
            //msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //msg.Show();
            AddList();
        }

        public void AddList()
        {
            //加载手术医生
            List<String> OperaNames = XmlHelper.GetOperations();
            List<OperationName> Doctors = new List<OperationName>();
            if (OperaNames != null)
            {
                foreach (var o in OperaNames)
                {
                    OperationName doctor = new OperationName();
                    doctor.Name = o;
                    Doctors.Add(doctor);
                }

            }
            this.RecordDG.ItemsSource = Doctors;
        }
    }
}
