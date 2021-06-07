using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Smart.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// RecordManage.xaml 的交互逻辑
    /// </summary>
    public partial class RecordManage : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        public static RecordManage _recordManage;

        private int pageNo = 1;
        private int pageSize = 8;
        private int PageCount=1;
        private List<RecordVideo> RecordVideoList = new List<RecordVideo>();

     
        public RecordManage()
        {
            InitializeComponent();

            //加载手术医生
            List<string> OperaDoctors = XmlHelper.GetStoreUsers();
            this.Doc_txt.ItemsSource = OperaDoctors;
            
             List<String> OperaNames = XmlHelper.GetOperations();
            this.operation_Com.ItemsSource = OperaNames;

            _recordManage = this;
            //加载记录
            GetList();
           
            this.Pre.IsEnabled = false;//上一页设置为不可用
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string doctorName = this.Doc_txt.Text;//医生姓名
            string userName = this.painet_Txt.Text;//患者姓名
            string optionName = this.operation_Com.Text.ToString();//手术名称
            string Sex = this.Sex_Com.Text.ToString();//性别
            string hosNum = this.hosNum_Txt.Text;//住院号
            string minAge = this.MinAge.Text;//最小年龄
            string MaxAge = this.MaxAge.Text;//最大年龄
            string startDate = this.StartDate.Text;//开始时间
            string endDate = this.EndDate.Text;//结束时间

            GetList(doctorName, userName, optionName, Sex, hosNum, minAge, MaxAge, startDate, endDate,1);


        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void First_Click(object sender, RoutedEventArgs e)
        {
            string doctorName = this.Doc_txt.Text;//医生姓名
            string userName = this.painet_Txt.Text;//患者姓名
            string optionName = this.operation_Com.Text.ToString();//手术名称
            string Sex = this.Sex_Com.Text.ToString();//性别
            string hosNum = this.hosNum_Txt.Text;//住院号
            string minAge = this.MinAge.Text;//最小年龄
            string MaxAge = this.MaxAge.Text;//最大年龄
            string startDate = this.StartDate.Text;//开始时间
            string endDate = this.EndDate.Text;//结束时间

            GetList(doctorName, userName, optionName, Sex, hosNum, minAge, MaxAge, startDate, endDate, 1);
            this.Last.IsEnabled = true;
            this.Next.IsEnabled = true;
            this.Pre.IsEnabled = false;
        }

        private void Pre_Click(object sender, RoutedEventArgs e)
        {
            this.Next.IsEnabled = true;
            if (pageNo >0)
            {
                string doctorName = this.Doc_txt.Text;//医生姓名
                string userName = this.painet_Txt.Text;//患者姓名
                string optionName = this.operation_Com.Text.ToString();//手术名称
                string Sex = this.Sex_Com.Text.ToString();//性别
                string hosNum = this.hosNum_Txt.Text;//住院号
                string minAge = this.MinAge.Text;//最小年龄
                string MaxAge = this.MaxAge.Text;//最大年龄
                string startDate = this.StartDate.Text;//开始时间
                string endDate = this.EndDate.Text;//结束时间
                GetList(doctorName, userName, optionName, Sex, hosNum, minAge, MaxAge, startDate, endDate, pageNo - 1);
            }
            else
            {
                this.Pre.IsEnabled = false;
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.Pre.IsEnabled = true;
            if (pageNo < PageCount)
            {
                string doctorName = this.Doc_txt.Text;//医生姓名
                string userName = this.painet_Txt.Text;//患者姓名
                string optionName = this.operation_Com.Text.ToString();//手术名称
                string Sex = this.Sex_Com.Text.ToString();//性别
                string hosNum = this.hosNum_Txt.Text;//住院号
                string minAge = this.MinAge.Text;//最小年龄
                string MaxAge = this.MaxAge.Text;//最大年龄
                string startDate = this.StartDate.Text;//开始时间
                string endDate = this.EndDate.Text;//结束时间
                GetList(doctorName, userName, optionName, Sex, hosNum, minAge, MaxAge, startDate, endDate, pageNo + 1);
            }
            else
            {
                this.Pre.IsEnabled = false;
            }
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            this.Last.IsEnabled = false;
            this.Pre.IsEnabled = true;
            this.Next.IsEnabled = false;
            string doctorName = this.Doc_txt.Text;//医生姓名
            string userName = this.painet_Txt.Text;//患者姓名
            string optionName = this.operation_Com.Text.ToString();//手术名称
            string Sex = this.Sex_Com.Text.ToString();//性别
            string hosNum = this.hosNum_Txt.Text;//住院号
            string minAge = this.MinAge.Text;//最小年龄
            string MaxAge = this.MaxAge.Text;//最大年龄
            string startDate = this.StartDate.Text;//开始时间
            string endDate = this.EndDate.Text;//结束时间
            GetList(doctorName, userName, optionName, Sex, hosNum, minAge, MaxAge, startDate, endDate, PageCount);
        }
        //获取录像记录列表
        public void GetList(string doctorName="",string userName="",string optionName="",string Sex="",string hosNum="",string minAge="",string maxAge="",string startDate="",string endDate="",int pageNo=1)
        {
            RecordVideoList = new List<RecordVideo>();
            try
            {
                var query = unitOfWork.DOperationRecord.GetAll().OrderByDescending(p => p.ID).ToList();

                if (query != null && query.Count() > 0)
                {
                    //医生名称
                    if (doctorName != "")
                    {
                        query = query.Where(p => p.surgeon.Contains(doctorName)).ToList();
                    }
                    //病人名称
                    if (userName != "")
                    {
                        query = query.Where(p => p.username.Contains(userName)).ToList();
                    }
                    //手术名称
                    if (optionName != "")
                    {
                        query = query.Where(p => p.operaName.Contains(optionName)).ToList();
                    }
                    //性别
                    if (Sex != "")
                    {
                        query = query.Where(p => p.sex == Sex).ToList();
                    }
                    //住院号
                    if (hosNum != "")
                    {
                        query = query.Where(p => p.hospNumber.Contains(hosNum)).ToList();
                    }
                    //最小年龄
                    if (minAge != "")
                    {
                        int mAge = Convert.ToInt32(minAge);
                        query = query.Where(p => p.age > mAge).ToList();
                    }
                    //最大年龄
                    if (maxAge != "")
                    {
                        int maAge = Convert.ToInt32(maxAge);
                        query = query.Where(p => p.age < maAge).ToList();
                    }
                    //开始时间
                    if (startDate != "")
                    {
                        DateTime sDate = Convert.ToDateTime(startDate);
                        query = query.Where(p => p.createtime >= sDate).ToList();
                    }
                    //结束时间
                    if (endDate != "")
                    {
                        DateTime eDate = Convert.ToDateTime(endDate);
                        query = query.Where(p => p.createtime <= eDate).ToList();
                    }
                    PageCount = (query.Count + pageSize - 1) / pageSize;
                    this.Total.Content = "共"+PageCount.ToString()+"页";
                    query = query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

                    foreach (var q in query)
                    {
                        var image = unitOfWork.DOperationImage.Get(p => p.operationID == q.ID);
                        var video = unitOfWork.DOperationVideo.Get(p => p.operationID == q.ID);
                        RecordVideo rv = new RecordVideo();
                        rv.PatintUserID = q.PatintUserID;
                        int oID = Convert.ToInt32(q.ID);
                        rv.ID = q.ID;
                        rv.hospNumber = q.hospNumber;
                        rv.operaName = q.operaName;
                        rv.operativesite = q.operativesite;
                        rv.sex = q.sex;
                        rv.surgeon = q.surgeon;
                        rv.status = q.status;
                        rv.createtime = q.createtime;
                        rv.CreateDate = Convert.ToDateTime(q.createtime).ToString("yyyy-MM-dd");
                        rv.age = q.age;
                        rv.username = q.username;
                        rv.VideoCount = video.Count();
                        rv.ImageCount = image.Count();

                        RecordVideoList.Add(rv);
                    }
                }

              
                RecordDG.ItemsSource = RecordVideoList;
            }
            catch (Exception ex)
            {
                RecordDG.ItemsSource = RecordVideoList;
            }
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
                    int ID = Convert.ToInt32(btn.Tag);
                    RecordDeleteSure rd = new RecordDeleteSure(3, ID);
                    rd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    rd.Show();
                }
            
        }

        public void DeleteSuccess()
        {
            GetList();
            // MessageBox.Show("删除成功！");

            MessageTip msg1 = new MessageTip("删除成功！");
            msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
          
            msg1.ShowDialog();

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

          
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




        }


        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int RID = Convert.ToInt32(btn.Tag);
            RecordImage op = new RecordImage(RID);
            op.Left = 460;
            op.Top = 120;
            op.ShowDialog();

          
        }

        private void BtnVideo_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int RID =Convert.ToInt32(btn.Tag);
            RecordManageVideo op = new RecordManageVideo(RID);
            op.Left = 460;
            op.Top = 120;
            op.ShowDialog();
        }

        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
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

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = DateTime.Now.ToString("yyyyMMdd")+"手术资料"; // Default file name
            dlg.DefaultExt = ".xlsx"; // Default file extension
            dlg.Filter = "Excel 工作薄 (.xlsx)|*.xlsx"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            string filename = string.Empty;

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dlg.FileName;
                LoadExcel(filename);
                //MessageBox.Show("导出成功");

                MessageTip msg1 = new MessageTip("导出成功！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
            }
            else
            {
                return;
            }
            
        }

        public void LoadExcel(string FileName)
        {
            //获取list数据
            var query = RecordVideoList;
          
         
           

            XSSFWorkbook book = new XSSFWorkbook();

            //创建Excel文件的对象
            //NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();


            ICellStyle cellstyle = book.CreateCellStyle();
            cellstyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            cellstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            IFont font = book.CreateFont();
            font.FontHeightInPoints = 26;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            font.FontName = "標楷體";
            cellstyle.SetFont(font);//HEAD 样式

            ICellStyle cellA = book.CreateCellStyle();
            cellA.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            cellA.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            IFont fontA = book.CreateFont();
            fontA.FontHeightInPoints = 16;
            fontA.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            fontA.FontName = "標楷體";
            cellA.SetFont(fontA);//HEAD 样式

            ICellStyle cellB = book.CreateCellStyle();
            cellB.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            cellB.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            IFont fontB = book.CreateFont();
            fontB.FontHeightInPoints = 16;
            fontB.FontName = "標楷體";
            cellB.SetFont(fontB);//HEAD 样式

            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            sheet1.SetColumnWidth(0, 25 * 256);
            sheet1.SetColumnWidth(1, 25 * 256);
            sheet1.SetColumnWidth(2, 25 * 256);
            sheet1.SetColumnWidth(3, 25 * 256);
            sheet1.SetColumnWidth(4, 25 * 256);
            sheet1.SetColumnWidth(5, 25 * 256);
            sheet1.SetColumnWidth(6, 27 * 256);
            sheet1.SetColumnWidth(7, 27 * 256);


            XSSFRow row1 = (XSSFRow)sheet1.CreateRow(0);
            //给sheet1添加第一行的头部标题
            //NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.Height = 50 * 15;
            ICell r10 = row1.CreateCell(0);
            r10.SetCellValue("序号");
            r10.CellStyle = cellA;
            ICell r11 = row1.CreateCell(1);
            r11.SetCellValue("医生姓名");
            r11.CellStyle = cellA;
            ICell r12 = row1.CreateCell(2);
            r12.SetCellValue("患者姓名");
            r12.CellStyle = cellA;
            ICell r13 = row1.CreateCell(3);
            r13.SetCellValue("性别");
            r13.CellStyle = cellA;
            ICell r14 = row1.CreateCell(4);
            r14.SetCellValue("年龄");
            r14.CellStyle = cellA;
            ICell r15 = row1.CreateCell(5);
            r15.SetCellValue("手术名称");
            r15.CellStyle = cellA;
            ICell r16 = row1.CreateCell(6);
            r16.SetCellValue("住院号");
            r16.CellStyle = cellA;
            ICell r17 = row1.CreateCell(7);
            r17.SetCellValue("住院日期");
            r17.CellStyle = cellA;

            int i = 0; int a = 1;

            foreach (var item in query)
            {

                i++;
                XSSFRow rowtemp = (XSSFRow)sheet1.CreateRow(i);
                //NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i);
                rowtemp.Height = 50 * 15;
                ICell rt0 = rowtemp.CreateCell(0);
                rt0.SetCellValue(item.ID);
                rt0.CellStyle = cellB;
                ICell rt1 = rowtemp.CreateCell(1);
                rt1.SetCellValue(item.surgeon);
                rt1.CellStyle = cellB;
                ICell rt2 = rowtemp.CreateCell(2);
                rt2.SetCellValue(item.username);
                rt2.CellStyle = cellB;
                ICell rt3 = rowtemp.CreateCell(3);
                rt3.SetCellValue(item.sex);
                rt3.CellStyle = cellB;
                ICell rt4 = rowtemp.CreateCell(4);
                rt4.SetCellValue(item.age.ToString());
                rt4.CellStyle = cellB;
                ICell rt5 = rowtemp.CreateCell(5);
                rt5.SetCellValue(item.operaName);
                rt5.CellStyle = cellB;
                ICell rt6 = rowtemp.CreateCell(6);
                rt6.SetCellValue(item.hospNumber.ToString());
                rt6.CellStyle = cellB;
                ICell rt7 = rowtemp.CreateCell(7);
                rt7.SetCellValue(item.CreateDate.ToString());
                rt7.CellStyle = cellB;
                a++;

            }


            //string path = "/File/" + Edu.Tools.CombHelper.GenerateLong() + ".xlsx";

            using (FileStream sw = System.IO.File.Create(FileName))
            {
                book.Write(sw);
            }
      

          
        }

        private void BtnOpenName_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
            string sFileFullName = btn.Tag.ToString();
          
           string fullName= XmlHelper.GetValue("Selectpath") + "\\" + sFileFullName;
            //System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            //string file = @"c:/windows/notepad.exe"; 
           // psi.Arguments = " /select," + fullName;
            //System.Diagnostics.Process.Start(psi);
            System.Diagnostics.Process.Start("Explorer.exe", fullName);
        }
    }
}
