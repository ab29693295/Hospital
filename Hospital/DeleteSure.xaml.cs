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
    /// MessageTip.xaml 的交互逻辑
    /// </summary>
    public partial class DeleteSure : Window
    {
        public int DType = 0;
        public string SetingStr = "";
        private static readonly string XmlPath = System.Windows.Forms.Application.StartupPath + "\\CheckAPP.config";//网站配置xml


        public DeleteSure( int Type,string SetStr)
        {
            InitializeComponent();
            DType = Type;
            SetingStr = SetStr;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Yes_Click(object sender, RoutedEventArgs e)
        {
            if (DType == 1)
            {
                XElement root = XElement.Load(XmlPath);
                var old = root.Elements("OperationParts").Elements("Part").Where(p => p.Attribute("OperationName").Value == SetingStr).FirstOrDefault();
                if (old != null)
                {
                    old.Remove();
                    root.Save(XmlPath);

                }
                MainWindow._mainWindow.operPart_Com.ItemsSource = XmlHelper.GetOperationParts();
                BuWeiManage._BuWeiManage.AddList();

            }
            else if (DType == 2)
            {
                XElement root = XElement.Load(XmlPath);
                var old = root.Elements("Users").Elements("User").Where(p => p.Attribute("UserName").Value == SetingStr).FirstOrDefault();
                if (old != null)
                {
                    old.Remove();
                    root.Save(XmlPath);

                }
                MainWindow._mainWindow.opeDoctor_Com.ItemsSource = XmlHelper.GetStoreUsers();
                DoctorManage._DoctorManage.AddList();
            }
            else if (DType == 3)
            {
                XElement root = XElement.Load(XmlPath);
                var old = root.Elements("Operations").Elements("Operation").Where(p => p.Attribute("OperationName").Value == SetingStr).FirstOrDefault();
                if (old != null)
                {
                    old.Remove();
                    root.Save(XmlPath);

                }
                MainWindow._mainWindow.operName_Com.ItemsSource = XmlHelper.GetOperations();
                ShousuManage._ShousuManage.AddList();
            }
            this.Close();
        }
        private void Button_NO_Click(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }
    }
}
