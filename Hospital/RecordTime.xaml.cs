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
    public partial class RecordTime : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();


        public string PauseTimeStr = "";

        public static RecordTime _recordTime;
        /// <summary>
        /// 状态
        /// </summary>
       public enum State
        {
            Start,
            Pause,
            End
        }

        /// <summary>
        /// 状态
        /// </summary>
        public State _state = State.Start;

        TimeSpan _timeSpan = new TimeSpan(0, 0, 0, 0, 0);
        public RecordTime(TimeSpan tp)
        {
            InitializeComponent();
          
            var t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, 1);
            t.Tick += OnTimer;
            t.IsEnabled = true;
            t.Start();
          

            this._timeSpan = tp;

            _recordTime = this;
        }
        /// <summary>
        /// 时钟回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnTimer(object sender, EventArgs e)
        {
            switch (_state)
            {
                case State.Start:
                    {
                        _timeSpan += new TimeSpan(0, 0, 0, 1);
                    }
                    break;
                case State.Pause:
                    {

                    }
                    break;
                case State.End:
                    {
                        _timeSpan = new TimeSpan();
                        //_timeSpan = new TimeSpan(0, 23, 12, 45, 54);
                    }
                    break;
            }

            PauseTimeStr = string.Format("{0:D2}:{1:D2}:{2:D2}", _timeSpan.Hours, _timeSpan.Minutes, _timeSpan.Seconds);
            this.Recordtime.Content = PauseTimeStr;
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            System.Windows.Rect r = new System.Windows.Rect(e.NewSize);
            RectangleGeometry gm = new RectangleGeometry(r, 7, 7); // 40 is radius here
            ((UIElement)sender).Clip = gm;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            this.Recordtime.Content ="录制中："+ DateTime.Now.ToString("hh:mm:ss");

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
            XmlHelper.SetValue("TimeX", width.ToString());

            XmlHelper.SetValue("TimeY", height.ToString());

            //MessageBox.Show(width.ToString());
            //MessageBox.Show(_recordTime.Left.ToString());
            //XmlHelper.SetValue("TimeX", _recordTime.Left.ToString());

            //XmlHelper.SetValue("TimeY", _recordTime.top.ToString());
        }
    }
}
