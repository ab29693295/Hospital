using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Smart.Entity;
using Smart.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Hospital
{
    /// <summary>
    /// 添加病例
    /// </summary>
    public partial class Operation : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        public int _patintID = 0;

        public static Operation _Operation;
        public Operation()
        {
            InitializeComponent();

            _Operation = this;
            //加载手术列表名称
            List<String> OperaNames = XmlHelper.GetOperations();
            this.operName_Com.ItemsSource = OperaNames;
            //加载手术部位名称
            List<String> OperaParts = XmlHelper.GetOperationParts();
            this.operPart_Com.ItemsSource = OperaParts;

            //加载手术医生
            List<string> OperaDoctors = XmlHelper.GetStoreUsers();
            this.opeDoctor_Com.ItemsSource = OperaDoctors;

            //加载科别
            List<string> departs = XmlHelper.GetDepartments();
            this.opeDepart_Com.ItemsSource = departs;


            this.operName_Com.SelectedValue = "";
            this.operPart_Com.SelectedValue = "";
            this.opeDoctor_Com.SelectedValue = "";
            this.opeDepart_Com.SelectedValue = "";


            // 查询当天数据
            string time = DateTime.Now.ToShortDateString();


            DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格


            DateTime time2 = Convert.ToDateTime(time + " 23:59:59");
           
            var paintList = unitOfWork.DPatient.Get(t => t.createtime > time1 & t.createtime < time2&&t.des!="1").OrderBy(p => p.status).ToList();
            this.RecordDG.ItemsSource = paintList;

           
        }
        /// <summary>
        /// 自动添加序号的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gdView_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow._mainWindow.WriteToScreen();
            //Application.Current.MainWindow.Show();
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow._mainWindow.WriteToScreen();
            //Application.Current.MainWindow.Show();
            this.Close();
            
        }
        /// <summary>
        /// 新增病例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(this.UserName_Txt.Text.ToString()) || string.IsNullOrEmpty(this.opeDoctor_Com.Text.ToString()))
            {
                MessageTip msgpaint = new MessageTip("必须输入病人姓名和医生姓名！");
                msgpaint.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msgpaint.Show();
                return;
            }
            if (Islegal())
            {
                MessageTip msgpaint = new MessageTip("病人姓名不能包含特殊字符！");
                msgpaint.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msgpaint.Show();
                return;
            }
            Patient pInt = new Patient();
            if (_patintID == 0)
            {
               
                pInt.username = this.UserName_Txt.Text;
                pInt.hospNumber = this.hostitalNum_Txt.Text;
                if (!string.IsNullOrEmpty(this.Age_Txt.Text))
                {
                    pInt.age = Convert.ToInt32(this.Age_Txt.Text);
                }
                pInt.Department = this.opeDepart_Com.Text;
                pInt.sex = this.Sex_Com.Text.ToString();
                pInt.operaName = this.operName_Com.Text;
                pInt.operativesite = this.operPart_Com.Text;
                pInt.surgeon = this.opeDoctor_Com.Text;
                pInt.createtime = DateTime.Now;
                pInt.operationDate = DateTime.Now;
                pInt.status = 0;
                unitOfWork.DPatient.Insert(pInt);
                unitOfWork.Save();
            }
            else
            {
                pInt = unitOfWork.DPatient.GetByID(_patintID);

                pInt.username = this.UserName_Txt.Text;
                pInt.hospNumber = this.hostitalNum_Txt.Text;
                if (!string.IsNullOrEmpty(this.Age_Txt.Text))
                {
                    pInt.age = Convert.ToInt32(this.Age_Txt.Text);
                }
                pInt.Department = this.opeDepart_Com.Text;
                pInt.sex = this.Sex_Com.Text.ToString();
                pInt.operaName = this.operName_Com.Text;
                pInt.operativesite = this.operPart_Com.Text;
                pInt.surgeon = this.opeDoctor_Com.Text;
                pInt.createtime = DateTime.Now;
                pInt.operationDate = DateTime.Now;
                pInt.status = 0;
                unitOfWork.DPatient.Update(pInt);
                unitOfWork.Save();
                _patintID = 0;
            }

          

            this.UserName_Txt.Text = "";
            this.Age_Txt.Text = "";
            this.hostitalNum_Txt.Text = "";          
            this.Sex_Com.Text = "";
            this.operName_Com.Text = "";
            this.operPart_Com.Text = "";
            this.opeDoctor_Com.Text = "";
            // 查询当天数据
            string time = DateTime.Now.ToShortDateString();


            DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格


            DateTime time2 = Convert.ToDateTime(time + " 23:59:59");

            var paintList = unitOfWork.DPatient.Get(t => t.createtime > time1 & t.createtime < time2 && t.des != "1").OrderBy(p => p.status).ToList();
            this.RecordDG.ItemsSource = paintList;
            //设置科别
            XmlHelper.StoreDepartments(this.opeDepart_Com.Text.ToString());

            //设置医生列表
            XmlHelper.SetUsers(this.opeDoctor_Com.Text.ToString());
            //设置手术名称
            XmlHelper.StoreOperations(this.operName_Com.Text.ToString());
            //设置手术部位
            XmlHelper.StoreOperaPart(this.operPart_Com.Text.ToString());

            //加载手术列表名称
            List<String> OperaNames = XmlHelper.GetOperations();
            this.operName_Com.ItemsSource = OperaNames;
            //加载手术部位名称
            List<String> OperaParts = XmlHelper.GetOperationParts();
            this.operPart_Com.ItemsSource = OperaParts;
            //加载科别
            List<string> departs = XmlHelper.GetDepartments();
            this.opeDepart_Com.ItemsSource = departs;
            //加载手术医生
            List<string> OperaDoctors = XmlHelper.GetStoreUsers();
            this.opeDoctor_Com.ItemsSource = OperaDoctors;

            //MessageBox.Show("新增成功！");
            //MessageTip msg = new MessageTip("保存成功！");
            //msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //msg.Show();
        }
        /// <summary>
        /// 删除病例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            if (btn != null)
            {
                int ID = Convert.ToInt32(btn.Tag);



                RecordDeleteSure rd = new RecordDeleteSure(4, ID);
                rd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                rd.Show();
            }


            //System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            //    if (btn != null)
            //    {
            //        int ID = Convert.ToInt32(btn.Tag);
                   
                  
            //    }

           

          
        }


        public  void DeleteSure()
        {
            // 查询当天数据
            string time = DateTime.Now.ToShortDateString();


            DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格


            DateTime time2 = Convert.ToDateTime(time + " 23:59:59");

            var paintList = unitOfWork.DPatient.Get(t => t.createtime > time1 & t.createtime < time2 && t.des != "1").OrderBy(p => p.status).ToList();
            this.RecordDG.ItemsSource = paintList;
            //MessageBox.Show("删除成功！");
            MessageTip msg = new MessageTip("删除成功！");
            msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            msg.ShowDialog();
        }
        /// <summary>
        /// 导入excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadExcel_Click(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "(excel文件)|*.xls;*.xlsx"//如果需要筛选txt文件（"Files (*.xlsx)|*.xls"）
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                path = openFileDialog.FileName;
                LoadExcelData(path);
            }
          
        }

        public void LoadExcelData(string FilePath)
        {
            using (ExcelHelper excelHelper = new ExcelHelper(FilePath))
            {
               DataTable  dt = excelHelper.ExcelToDataTable("MySheet", true);//读取数据  
                foreach (DataRow dr in dt.Rows)//DataTable转ObservableCollection  
                {
                    Patient pt = new Patient();
                    pt.username = dr[0].ToString();
                    pt.sex= dr[1].ToString();
                    if (IsInt(dr[2].ToString()))
                    {
                        pt.age = Convert.ToInt32(dr[2].ToString());
                    }
                  
                    pt.hospNumber = dr[3].ToString();
                    pt.operaName = dr[4].ToString();
                    pt.operativesite = dr[5].ToString();
                    pt.Department = dr[6].ToString();
                    pt.des = dr[7].ToString();
                    pt.createtime = DateTime.Now;
                    pt.operationDate = DateTime.Now;
                    pt.status = 0;

                    unitOfWork.DPatient.Insert(pt);
                 
                }
                unitOfWork.Save();
            }
            // 查询当天数据
            string time = DateTime.Now.ToShortDateString();


            DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格


            DateTime time2 = Convert.ToDateTime(time + " 23:59:59");

            var paintList = unitOfWork.DPatient.Get(t => t.createtime > time1 & t.createtime < time2 && t.des != "1").OrderBy(p => p.status).ToList();
            this.RecordDG.ItemsSource = paintList;
            //MessageBox.Show("导入成功！");

            MessageTip msg1 = new MessageTip("导入成功！");
            msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            msg1.Show();

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

        public bool Islegal()
        {
            Regex regExp = new Regex("[~!@#$%^&*()=+[\\]{}''\";:/?.,><`|！·￥…—（）\\-、；：。，》《]");
            return regExp.IsMatch(UserName_Txt.Text.Trim());
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            if (btn != null)
            {
                int ID = Convert.ToInt32(btn.Tag);

                var pInt = unitOfWork.DPatient.GetByID(ID);
                this.UserName_Txt.Text = pInt.username;
                this.hostitalNum_Txt.Text = pInt.hospNumber ;
                this.Age_Txt.Text = pInt.age.ToString();
                 this.Sex_Com.Text = pInt.sex;
                this.operName_Com.Text = pInt.operaName ;
                this.operPart_Com.Text = pInt.operativesite;
                this.opeDoctor_Com.Text = pInt.surgeon;


                _patintID = ID;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OutLoadExcel_Click(object sender, RoutedEventArgs e)
        {
            string time = DateTime.Now.ToShortDateString();


            DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格


            DateTime time2 = Convert.ToDateTime(time + " 23:59:59");

            var paintList = unitOfWork.DPatient.Get(t => t.createtime > time1 & t.createtime < time2 && t.des != "1").OrderBy(p => p.status).ToList();
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("病例记录");

            IRow rowTitle = sheet.CreateRow(0);
            ICell cellTitle0 = rowTitle.CreateCell(0);
            cellTitle0.SetCellValue("病人姓名");

            ICell cellTitle1 = rowTitle.CreateCell(1);
            cellTitle1.SetCellValue("性别");

            ICell cellTitle2 = rowTitle.CreateCell(2);
            cellTitle2.SetCellValue("年龄");

            ICell cellTitle3 = rowTitle.CreateCell(3);
            cellTitle3.SetCellValue("科别");

            ICell cellTitle4 = rowTitle.CreateCell(4);
            cellTitle4.SetCellValue("住院号");

            ICell cellTitle5 = rowTitle.CreateCell(5);
            cellTitle5.SetCellValue("手术部位");

            ICell cellTitle6 = rowTitle.CreateCell(5);
            cellTitle6.SetCellValue("手术医生");

            int rowIndex = 1;
            foreach (var ri in paintList)
            {
                IRow rowData = sheet.CreateRow(rowIndex++);
                ICell cellData0 = rowData.CreateCell(0);
                cellData0.SetCellValue(ri.username);

                ICell cellData1 = rowData.CreateCell(1);
                cellData1.SetCellValue(ri.sex);

                ICell cellData2 = rowData.CreateCell(2);
                cellData2.SetCellValue(ri.age.ToString());

                ICell cellData3 = rowData.CreateCell(3);
                cellData3.SetCellValue(ri.Department);

                ICell cellData4 = rowData.CreateCell(4);
                cellData4.SetCellValue(ri.hospNumber.ToString());

                ICell cellData5 = rowData.CreateCell(5);
                cellData5.SetCellValue(ri.operativesite);

                ICell cellData6 = rowData.CreateCell(5);
                cellData6.SetCellValue(ri.surgeon);
            }
            //写入文件 弹出文件保存
            //string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//桌面路径
            string filename =  DateTime.Now.ToString("yyyyMMdd")+ "病例资料";//文件名
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = filename;
            saveDialog.ShowDialog();
            filename = saveDialog.FileName;
            if (filename.IndexOf(":") < 0) return; //被点了取消
            FileStream xlsfile = new FileStream(saveDialog.FileName, FileMode.Create);
            workbook.Write(xlsfile);
            xlsfile.Close();
            System.Diagnostics.Process.Start(filename);

        }
    }
}
