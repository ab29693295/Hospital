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
    /// RecordManage.xaml 的交互逻辑
    /// </summary>
    public partial class RecordImage : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        List<ImageModel> ImageList = new List<ImageModel>();

        private int recordID = 0;

        public static RecordImage _recordImage;


        public RecordImage(int RID)
        {
            InitializeComponent();
            recordID = RID;
            LoadImageList(RID);

            _recordImage = this;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadImageList(int RID)
        {
            List<ImageModel> ImList = new List<ImageModel>();
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

            ImageList = ImList;

            lsPricture.ItemsSource = ImageList;
        }

        /// <summary>
        /// 删除单子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {


            int count = 0;
            foreach (var item in ImageList)
            {
                bool isSelected = item.IsSelected; //要获取的状态
                if (isSelected == true)
                {
                    count++;
                    int ID = Convert.ToInt32(item.ID);

                    RecordDeleteSure rd = new RecordDeleteSure(1, ID);
                    rd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    rd.Show();


                }
            }

          
           



        }

        public void DeleteSuccess()
        {
            LoadImageList(recordID);
            //MessageBox.Show("删除成功！");

            MessageTip msg = new MessageTip("删除成功！");
            msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            msg.ShowDialog();
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnVideo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
           
            foreach (var item in ImageList)
            {
                bool isSelected = item.IsSelected; //要获取的状态
                if (isSelected == true)
                {
                  
                    int ID = Convert.ToInt32(item.ID);
                    ShowImage md = new ShowImage(ID);
                    md.ShowDialog();
                }
            }



        
          
            //var imageList = unitOfWork.DOperationImage.Get(p => p.operationID == recordID).FirstOrDefault();
            //if (imageList != null)
            //{

            //    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            //    //string file = @"c:/windows/notepad.exe"; 
            //    psi.Arguments = " /select," + imageList.imagePath;
            //    System.Diagnostics.Process.Start(psi);

            //}
        }
    }
}
