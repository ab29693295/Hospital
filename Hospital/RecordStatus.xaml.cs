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
using System.Windows.Threading;

namespace Hospital
{
    /// <summary>
    /// RecordStatus.xaml 的交互逻辑
    /// </summary>
    public partial class RecordStatus : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();
        public static RecordStatus _RecordStatus;
        public RecordStatus(int rID)
        {
            InitializeComponent();

            var record = unitOfWork.DOperationRecord.GetByID(rID);

            this.userName.Content = "病人姓名：" + record.username;
            this.age.Content = "病人年龄：" + record.age;
            this.sex.Content = "病人性别：" + record.sex;
            this.doctor.Content = "手术医生：" + record.surgeon;

            _RecordStatus = this;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            System.Windows.Rect r = new System.Windows.Rect(e.NewSize);
            RectangleGeometry gm = new RectangleGeometry(r, 7, 7); // 40 is radius here
            ((UIElement)sender).Clip = gm;
        }

      

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point ptLeftUp = new Point(0, 0);
            ptLeftUp = this.PointToScreen(ptLeftUp);

            double width = ptLeftUp.X / 1.25;
            double height = ptLeftUp.Y / 1.25;
            XmlHelper.SetValue("StatusX", width.ToString());

            XmlHelper.SetValue("StatusY", height.ToString());

            //XmlHelper.SetValue("StatusX", _RecordStatus.Left.ToString());

            //XmlHelper.SetValue("StatusY", _RecordStatus.top.ToString());
        }
    }
}
