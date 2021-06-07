using Smart.Entity;
using Smart.Models.Models;
using Smart.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// 添加病例
    /// </summary>
    public partial class ShouYeOperation : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        public int _patintID = 0;

        public List<PaientModel> _paientList = new List<PaientModel>();


        public static ShouYeOperation _Operation;
        public ShouYeOperation()
        {
            InitializeComponent();

            _Operation = this;



            // 查询当天数据
            GetPaientList();
            //string time = DateTime.Now.ToShortDateString();


            //DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格


            //DateTime time2 = Convert.ToDateTime(time + " 23:59:59");

            //var paintList = unitOfWork.DPatient.Get(t => t.createtime > time1 && t.createtime < time2 && t.des != "1").OrderBy(p => p.status).ToList();
            //this.RecordDG.ItemsSource = paintList;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_patintID == 0)
            {
                MessageTip msg1 = new MessageTip("请您选择病例！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
            }
            else
            {
                MainWindow._mainWindow.SetPainetUser(_patintID);
                //MainWindow._mainWindow.WriteToScreen();
                //Application.Current.MainWindow.Show();
                this.Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow._mainWindow.WriteToScreen();
            //Application.Current.MainWindow.Show();
            this.Close();

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            _patintID = 0;
        }
        /// <summary>
        /// 取消实例选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            for (int k = 0; k < this.RecordDG.Items.Count; k++)
            {
                //首先获取DataGridTemplateColumn所在列
                DataGridTemplateColumn tempColumn = this.RecordDG.Columns[0] as DataGridTemplateColumn;


                //然后获取DataGridTemplateColumn单元格元素
                FrameworkElement element = this.RecordDG.Columns[0].GetCellContent(this.RecordDG.Items[k]);


                if (element != null)
                {
                    //把单元格元素转换为相应的控件，再从该控件中取值
                    CheckBox ck = tempColumn.CellTemplate.FindName("cbbSelALL", element) as CheckBox;


                    if (ck.Tag != chk.Tag)
                    {
                        ck.IsChecked = false;
                    }
                }
            }



            //for (int i = 0; i < _paientList.Count(); i++)
            //{
            //    DataGridTemplateColumn templeColumn = RecordDG.Columns[i] as DataGridTemplateColumn;

            //    if (templeColumn == null) return;

            //    object item = RecordDG.CurrentCell.Item;
            //    FrameworkElement element = templeColumn.GetCellContent(item);
            //    CheckBox check =(CheckBox) templeColumn.CellTemplate.FindName("cbbSelALL", element);
            //    if(chk!=check)
            //    {
            //        check.IsChecked = false;
            //    }

            //}
            _patintID = int.Parse(chk.Tag.ToString());

        }
        /// <summary>
        /// 获取病例列表
        /// </summary>
        public void GetPaientList()
        {
            // 查询当天数据
            string time = DateTime.Now.ToShortDateString();


            DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格


            DateTime time2 = Convert.ToDateTime(time + " 23:59:59");

            var paintList = unitOfWork.DPatient.Get(t => t.createtime > time1 && t.createtime < time2 && t.des != "1").OrderBy(p => p.status).ToList();

            foreach (var pt in paintList)
            {
                PaientModel pm = new PaientModel();
                pm.ID = pt.ID;
                pm.username = pt.username;
                pm.sex = pt.sex;
                pm.age = pt.age;
                pm.hospNumber = pt.hospNumber;
                pm.createtime = pt.createtime;
                pm.operaName = pt.operaName;
                pm.operativesite = pt.operativesite;
                pm.surgeon = pt.surgeon;
                pm.Department = pt.Department;
                pm.operationDate = pt.operationDate;
                pm.status = pt.status;
                pm.des = pt.des;
                pm.IsCheck = false;
                var _record = unitOfWork.DOperationRecord.Get(p => p.PatintUserID == pt.ID).FirstOrDefault();
                if (_record == null)
                {

                    _paientList.Add(pm);

                }
                else
                {
                    var ImageList = unitOfWork.DOperationImage.Get(p => p.operationID == _record.ID).FirstOrDefault();



                    var listFile = unitOfWork.DOperationVideo.Get(p => p.operationID == _record.ID).FirstOrDefault();

                    if (ImageList == null && listFile == null)
                    {
                        _paientList.Add(pm);
                    }
                }


            }
            this.RecordDG.ItemsSource = _paientList;
        }

        /// <summary>
        /// 删除病例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {



            Button btn = sender as Button;
            if (btn != null)
            {
                int ID = Convert.ToInt32(btn.Tag);
                unitOfWork.DPatient.Delete(ID);
                unitOfWork.Save();
                // 查询当天数据
                GetPaientList();
                //string time = DateTime.Now.ToShortDateString();


                //DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格


                //DateTime time2 = Convert.ToDateTime(time + " 23:59:59");

                //var paintList = unitOfWork.DPatient.Get(t => t.createtime > time1 & t.createtime < time2).OrderBy(p => p.status).ToList();
                //this.RecordDG.ItemsSource = paintList;
                //MessageBox.Show("删除成功！");
                MessageTip msg = new MessageTip("删除成功！");
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.ShowDialog();
            }

        }

        /// <summary>
        /// 窗体可拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        /// <summary>
        /// 判断是不是int型
        /// </summary>
        /// <param name="str">接收的字符串</param>
        /// <returns></returns>
        public bool IsInt(string str)
        {
            try
            {
                //在这里将接收的字符串
                int a = Convert.ToInt32(str);
                //如果转换成功 返回的则是true  可以转换为int型
                return true;
            }
            catch
            {
                //如果转换int型失败会返回false 这个字符串中含有非数字的字符 所以不能转换为int型
                return false;
            }
            //在使用这个函数的时候   只需要判断返回的是true 还是false即可
        }

        private void Age_Txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);
        }




        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
