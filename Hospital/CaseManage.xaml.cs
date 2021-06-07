using Hospital.Models;
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
    /// CaseManage.xaml 的交互逻辑
    /// </summary>
    public partial class CaseManage : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();
        private int CurrentCount = 1;
        private int templeteIndex = -1;
        private int RID = 0;
        public static CaseManage _Casemange = null;
        public List<ImageModel> ImList = new List<ImageModel>();
        public  int _templeteID = 1;
        public CaseManage()
        {
            InitializeComponent();
            Person p1 = new Person { ID=1, Name = "/Hospital;component/Resources/templete.png", Age = 10 };
            Person p2 = new Person { ID = 2, Name = "/Hospital;component/Resources/templete.png", Age = 10 };
            Person p3 = new Person { ID = 3, Name = "/Hospital;component/Resources/templete.png", Age = 10 };
            _Casemange = this;
            List<Person> list = new List<Person>();
            list.Add(p1);
            list.Add(p2);
            list.Add(p3);
          
          
            lb.ItemsSource = list;
            //绑定手术记录表
            var ListRecord = unitOfWork.DOperationRecord.GetAll().ToList();

            RecordDG.ItemsSource = ListRecord;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FistBtn_Click(object sender, RoutedEventArgs e)
        {
            this.FirstModel.Visibility = Visibility.Visible;
            this.SecondModel.Visibility = Visibility.Hidden;
            this.ThirdModel.Visibility = Visibility.Hidden;
            this.ForthModel.Visibility = Visibility.Hidden;
            this.FiveModel.Visibility = Visibility.Hidden;
            this.BeforeBtn.Visibility = Visibility.Hidden;
            this.NextBtn.Visibility = Visibility.Visible;
            this.NextBtn.Content = "下一步";
        }

        private void SecondBtn_Click(object sender, RoutedEventArgs e)
        {
            if (templeteIndex < 0)
            {
                // MessageBox.Show("请选择模板！");
                MessageTip msg = new MessageTip("请选择模板！");
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.Show();
                return;
            }
            CurrentCount = 1;
            this.FirstModel.Visibility = Visibility.Hidden;
            this.SecondModel.Visibility = Visibility.Visible;
            this.ThirdModel.Visibility = Visibility.Hidden;
            this.ForthModel.Visibility = Visibility.Hidden;
            this.FiveModel.Visibility = Visibility.Hidden;
            this.BeforeBtn.Visibility = Visibility.Visible;
            this.NextBtn.Visibility = Visibility.Visible;
            this.NextBtn.Content = "下一步";
        }

        private void ThirdBtn_Click(object sender, RoutedEventArgs e)
        {
           
            if (templeteIndex < 0)
            {
               // MessageBox.Show("请选择模板！");
                MessageTip msg = new MessageTip("请选择模板！");
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.Show();
                return;
            }
            CurrentCount = 2;
            if (RID == 0)
            {
                //MessageBox.Show("请选择病例！");
                MessageTip msg1 = new MessageTip("请选择病例！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
                return;
            }
            LoadImageList(RID);
            this.FirstModel.Visibility = Visibility.Hidden;
            this.SecondModel.Visibility = Visibility.Hidden;
            this.ThirdModel.Visibility = Visibility.Visible;
            this.ForthModel.Visibility = Visibility.Hidden;
            this.FiveModel.Visibility = Visibility.Hidden;
            this.BeforeBtn.Visibility = Visibility.Visible;
            this.NextBtn.Visibility = Visibility.Visible;
            this.NextBtn.Content = "下一步";
        }

        private void FourthBtn_Click(object sender, RoutedEventArgs e)
        {
            if (templeteIndex < 0)
            {
                //MessageBox.Show("请选择模板！");
                MessageTip msg1 = new MessageTip("请选择模板！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
                return;
            }
            if (RID == 0)
            {
                //MessageBox.Show("请选择病例！");
                MessageTip msg1 = new MessageTip("请选择病例！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
                return;
            }

            if (CheckIamgeCount())
            {

                LoadPainet(RID);
                CurrentCount = 3;
                this.FirstModel.Visibility = Visibility.Hidden;
                this.SecondModel.Visibility = Visibility.Hidden;
                this.ThirdModel.Visibility = Visibility.Hidden;
                this.ForthModel.Visibility = Visibility.Visible;
                this.FiveModel.Visibility = Visibility.Hidden;
                this.BeforeBtn.Visibility = Visibility.Visible;
                this.NextBtn.Visibility = Visibility.Visible;
                this.NextBtn.Content = "打印预览";
            }
        }

        private void FiveBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadWPFTemplete();
            CurrentCount = 4;
            this.FirstModel.Visibility = Visibility.Hidden;
            this.SecondModel.Visibility = Visibility.Hidden;
            this.ThirdModel.Visibility = Visibility.Hidden;
            this.ForthModel.Visibility = Visibility.Hidden;
            this.FiveModel.Visibility = Visibility.Hidden;
            this.BeforeBtn.Visibility = Visibility.Visible;
           
            this.NextBtn.Content = "打印预览";
        }
      
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.NextBtn.Content.ToString() == "打印预览")
            {

                LoadWPFTemplete();

                //PrintPreview previewWnd = new PrintPreview("OrderDocument.xaml", m_orderExample, new OrderDocumentRenderer());
                //previewWnd.Owner = this;
                //previewWnd.ShowInTaskbar = false;
                //previewWnd.ShowDialog();
                //Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                //dlg.FileName =this.userName_La.Content+ "病例记录"; // Default file name
                //dlg.DefaultExt = ".pdf"; // Default file extension
                //dlg.Filter = "标签|*.pdf"; // Filter files by extension

                //// Show save file dialog box
                //Nullable<bool> result = dlg.ShowDialog();
                //string filename = string.Empty;

                //// Process save file dialog box results
                //if (result == true)
                //{
                //    // Save document
                //    filename = dlg.FileName;
                //    PdfXpsHelper.SaveViewContentToPdf(filename, this.PDFViewBox);
                //    //MessageBox.Show("导出成功");
                //    MessageTip msg1 = new MessageTip("导出成功！");
                //    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                //    msg1.Show();
                //    return;
                //}
                //else
                //{
                //    return;
                //}


            }
            else
            {
                switch (CurrentCount)
                {
                    case 1:
                        CurrentCount = 2;
                        this.FirstModel.Visibility = Visibility.Hidden;
                        this.SecondModel.Visibility = Visibility.Visible;
                        this.ThirdModel.Visibility = Visibility.Hidden;
                        this.ForthModel.Visibility = Visibility.Hidden;
                        this.FiveModel.Visibility = Visibility.Hidden;
                        this.BeforeBtn.Visibility = Visibility.Visible;
                        this.NextBtn.Visibility = Visibility.Visible;
                        break;
                    case 2:
                      
                        if (templeteIndex < 0)
                        {
                            //MessageBox.Show("请选择模板！");
                            MessageTip msg1 = new MessageTip("请选择模板！");
                            msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            msg1.Show();

                            return;
                        }
                        if (RID == 0)
                        {
                            //MessageBox.Show("请选择病例！");
                            MessageTip msg1 = new MessageTip("请选择病例！");
                            msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            msg1.Show();
                            return;
                        }
                        if (CheckIamgeCount())
                        {
                            LoadImageList(RID);
                            this.FirstModel.Visibility = Visibility.Hidden;
                            this.SecondModel.Visibility = Visibility.Hidden;
                            this.ThirdModel.Visibility = Visibility.Visible;
                            this.ForthModel.Visibility = Visibility.Hidden;
                            this.FiveModel.Visibility = Visibility.Hidden;
                            this.BeforeBtn.Visibility = Visibility.Visible;
                            this.NextBtn.Visibility = Visibility.Visible;

                            CurrentCount = 3;
                            break;
                        }
                        else
                        {
                            return;

                        }
                      
                    case 3:
                     
                        if (templeteIndex < 0)
                        {
                            //MessageBox.Show("请选择模板！");
                            MessageTip msg1 = new MessageTip("请选择模板！");
                            msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            msg1.Show();
                            return;
                        }
                        if (RID == 0)
                        {
                            //MessageBox.Show("请选择病例！");
                            MessageTip msg1 = new MessageTip("请选择病例！");
                            msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            msg1.Show();
                            return;
                        }
                        if (CheckIamgeCount())
                        {
                            CurrentCount = 4;
                            LoadPainet(RID);

                            this.FirstModel.Visibility = Visibility.Hidden;
                            this.SecondModel.Visibility = Visibility.Hidden;
                            this.ThirdModel.Visibility = Visibility.Hidden;
                            this.ForthModel.Visibility = Visibility.Visible;
                            this.FiveModel.Visibility = Visibility.Hidden;
                            this.BeforeBtn.Visibility = Visibility.Visible;
                            this.NextBtn.Visibility = Visibility.Visible;
                            this.NextBtn.Content = "打印预览";
                            break;
                        }
                        else
                        {
                            return;
                        }
                    case 4:

                        LoadWPFTemplete();
                        this.FirstModel.Visibility = Visibility.Hidden;
                        this.SecondModel.Visibility = Visibility.Hidden;
                        this.ThirdModel.Visibility = Visibility.Hidden;
                        this.ForthModel.Visibility = Visibility.Hidden;
                        this.FiveModel.Visibility = Visibility.Hidden;
                        this.BeforeBtn.Visibility = Visibility.Visible;
                       
                        break;
                 
                      

                }

            }
        }

        private void BeforeBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (CurrentCount)
            {
              
                case 1:
                    
                    this.FirstModel.Visibility = Visibility.Visible;
                    this.SecondModel.Visibility = Visibility.Hidden;
                    this.ThirdModel.Visibility = Visibility.Hidden;
                    this.ForthModel.Visibility = Visibility.Hidden;
                    this.FiveModel.Visibility = Visibility.Hidden;
                    this.BeforeBtn.Visibility = Visibility.Visible;
                    this.NextBtn.Visibility = Visibility.Visible;
                    this.NextBtn.Content = "下一步";
                    break;
                case 2:
                    CurrentCount = 1;
                    this.FirstModel.Visibility = Visibility.Visible;
                    this.SecondModel.Visibility = Visibility.Hidden;
                    this.ThirdModel.Visibility = Visibility.Hidden;
                    this.ForthModel.Visibility = Visibility.Hidden;
                    this.FiveModel.Visibility = Visibility.Hidden;
                    this.BeforeBtn.Visibility = Visibility.Visible;
                    this.NextBtn.Visibility = Visibility.Visible;
                    this.NextBtn.Content = "下一步";
                    break;
                case 3:
                    CurrentCount = 2;
                    this.FirstModel.Visibility = Visibility.Hidden;
                    this.SecondModel.Visibility = Visibility.Visible;
                    this.ThirdModel.Visibility = Visibility.Hidden;
                    this.ForthModel.Visibility = Visibility.Hidden;
                    this.FiveModel.Visibility = Visibility.Hidden;
                    this.BeforeBtn.Visibility = Visibility.Visible;
                    this.NextBtn.Visibility = Visibility.Visible;
                    this.NextBtn.Content = "下一步";
                    break;
                case 4:
                    CurrentCount = 3;
                    this.FirstModel.Visibility = Visibility.Hidden;
                    this.SecondModel.Visibility = Visibility.Hidden;
                    this.ThirdModel.Visibility = Visibility.Visible;
                    this.ForthModel.Visibility = Visibility.Hidden;
                    this.FiveModel.Visibility = Visibility.Hidden;
                    this.BeforeBtn.Visibility = Visibility.Visible;
                    this.NextBtn.Content = "下一步";
                    break;

            }
        }
        /// <summary>
        /// listbox选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //获取当前索引
           this.templeteIndex= lb.SelectedIndex;
        }

      

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
           
            RID = 0;
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

          
                RID = int.Parse(chk.Tag.ToString());
          
           
        }

        public void LoadPainet(int RID)
        {
            var record = unitOfWork.DOperationRecord.GetByID(RID);
           
            this.UserName_Txt.Text = record.username;
            

            this.Age_Txt.Text = record.age.ToString();

            this.Sex_Com.Text = record.sex.ToString();

            this.Hous_Nmum.Text = record.hospNumber;

            this.Categy_Txt.Text = record.categoryName;

            CheckIamgeCount();

        }

       


        //加载WPF模板数据
        public void LoadWPFTemplete()
        {
            if (templeteIndex < 0)
            {
                //MessageBox.Show("请选择模板！");
                MessageTip msg1 = new MessageTip("请选择模板！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
                return;
            }
            if (RID == 0)
            {
                //MessageBox.Show("请选择病例！");
                MessageTip msg1 = new MessageTip("请选择病例！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
                return;
            }

            List<ImageModel> ImViewList = new List<ImageModel>();

            foreach (var item in ImList)
            {
                bool isSelected = item.IsSelected; //要获取的状态
                if (isSelected == true)
                {
                    ImViewList.Add(item);
                }
            }

            CheckIamgeCount();

          
          

            PainetMaster m_orderExample = new PainetMaster
            {
                UserName = this.UserName_Txt.Text,
                Sex = this.Sex_Com.Text,
                Title = this.Title_Txt.Text,
                Age = this.Age_Txt.Text,
                Phone = this.Phone_Txt.Text,
                Nei = this.Nei_Num.Text,
                Hos = this.Hous_Nmum.Text,
                Des = this.Des.Text,
                Categy = this.Categy_Des.Text,
                Address = this.Address_Txt.Text,
                OrderDetails= ImViewList

            };

            PrintPreview previewWnd = new PrintPreview("OrderDocument.xaml", m_orderExample, new OrderDocumentRenderer());
            previewWnd.Owner = this;
            previewWnd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
            previewWnd.ShowDialog();

         
            //this.Title_Label.Content = this.Title_Txt.Text;

            //this.userName_La.Content = this.UserName_Txt.Text;

            //this.sex_La.Content = this.Sex_Com.Text;

            //this.age_La.Content = this.Age_Txt.Text;

            //this.Phone_La.Content = this.Phone_Txt.Text;

            //this.Nei_La.Content = this.Nei_Num.Text;
          

            //this.Hos_LA.Content = this.Hous_Nmum.Text;

            //this.Categy_La.Content = this.Categy_Des.Text;

            //this.Des_La.Content = this.Des.Text;

            //this.AddressLabel.Content = this.Address_Txt.Text;

            //this.DaoPricture.ItemsSource = ImViewList;

          
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

        //绑定图片
        public void LoadImageList(int RID)
        {
          
            //获取图片地址
            var imageList = unitOfWork.DOperationImage.Get(p => p.operationID == RID).ToList();
            if (imageList != null)
            {
                foreach (var img in imageList)
                {
                    ImageModel iModel = new ImageModel();
                    iModel.ID = img.ID;
                    iModel.imagePath = img.imagePath;
                    iModel.name = img.name;
                    iModel.IsSelected = false;

                    ImList.Add(iModel);
                }
            }
            

            lsPricture.ItemsSource = ImList;
        }

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            var docName = this.Doc_Txt.Text;

            var painetName = this.Painet_Txt.Text;
            //绑定手术记录表
            var ListRecord = unitOfWork.DOperationRecord.Get(p=>p.surgeon.Contains(docName)&&p.username.Contains(painetName)).ToList();

            RecordDG.ItemsSource = ListRecord;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image chk = (Image)sender;
            _templeteID = Convert.ToInt32(chk.Tag);
        }


        public bool CheckIamgeCount()
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
                    return false;
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
                    return false;
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
                    return false;
                }
            }

            return true; 
        }
    }
    public class Record
    {
        public int ID;
        public string Dorname;

        public string CaseName;

        public string Sex;

        public int Age;

        public string OprtionName;

        public string Number;

        public string Date;
       

    }
}
