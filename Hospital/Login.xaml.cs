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
    public partial class Login : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();
        public Login()
        {
            InitializeComponent();
            Pwd_Txt.TextDecorations = new TextDecorationCollection(new TextDecoration[] {
                new TextDecoration() {
                     Location= TextDecorationLocation.Strikethrough,
                      Pen= new Pen(Brushes.Black, 10f) {
                          DashCap =  PenLineCap.Round,
                           StartLineCap= PenLineCap.Round,
                            EndLineCap= PenLineCap.Round,
                             DashStyle= new DashStyle(new double[] {0.0,1.2 }, 0.6f)
                      }
                }

            });


        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = this.UserName_Txt.Text;
            string pwd = this.Pwd_Txt.Text;

            var user = unitOfWork.DUser.Get(p => p.UserName == userName && p.Password == pwd).FirstOrDefault();
            if (user != null)
            {
                Seting st = new Seting();
                st.Left = 360;
                st.Top = 80;
                st.Show();
                this.Close();
            }
            else
            {
                MessageTip msg1 = new MessageTip("用户名密码错误！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
            }
        }
    }
}
