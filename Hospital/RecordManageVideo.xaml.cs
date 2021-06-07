using QCAP.NET;
using Smart.Entity;
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
    public partial class RecordManageVideo : Window
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        List<VideoeModel> VideoList = new List<VideoeModel>();

        public static RecordManageVideo _RecordManageVideo;

        private int recordID = 0;


        public RecordManageVideo(int RID)
        {
            recordID = RID;
            InitializeComponent();

            var painet = unitOfWork.DOperationRecord.GetByID(RID);
            this.titleLabel.Content = "视频文件--"+painet.surgeon+"--"+painet.username;

            LoadVideoList(RID);

            _RecordManageVideo = this;
            //视频列表
            //var videoList = unitOfWork.DOperationVideo.Get(p=>p.operationID==RID).ToList();
            //this.RecordDG.ItemsSource = videoList;

        }

        public void LoadVideoList(int RID)
        {
            List<VideoeModel> ImList = new List<VideoeModel>();
            //获取图片地址
            var imageList = unitOfWork.DOperationVideo.Get(p => p.operationID == RID).ToList();
            if (imageList != null)
            {
                foreach (var vedio in imageList)
                {
                    VideoeModel iModel = new VideoeModel();
                    iModel.ID = vedio.ID;
                    iModel.videoPath = vedio.videoPath;
                    iModel.duration = vedio.duration;
                    iModel.name = vedio.name;
                    iModel.IsSelected = false;

                    ImList.Add(iModel);
                }
            }
            VideoList = ImList;

            RecordDG.ItemsSource = VideoList;
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
      
     


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {

            int count = 0;
            foreach (var item in VideoList)
            {
                bool isSelected = item.IsSelected; //要获取的状态
                if (isSelected == true)
                {
                    count++;
                    int ID = Convert.ToInt32(item.ID);
                    RecordDeleteSure rd = new RecordDeleteSure(2, ID);
                    rd.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    rd.Show();
                }
            }



        }

        public void DeleteSuccess()
        {
            LoadVideoList(recordID);
            //MessageBox.Show("删除成功！");

            MessageTip msg = new MessageTip("删除成功！");
            msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            msg.ShowDialog();

        }

        private void BtnAction_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
                int VID = Convert.ToInt32(btn.Tag);

                var video = unitOfWork.DOperationVideo.GetByID(VID);
                string videoPath = video.videoPath;


                System.Diagnostics.Process.Start(videoPath);
            }
            catch (Exception ex)
            {
                MessageTip msg = new MessageTip("暂无视频！");
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.ShowDialog();
            }
                //MediaPlay msg1 = new MediaPlay(VID);
                //msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                //msg1.Show();

        }


        private void OpenVideo_Click(object sender, RoutedEventArgs e)
        {
            var imageList = unitOfWork.DOperationVideo.Get(p => p.operationID == recordID).FirstOrDefault();
            if (imageList != null)
            {

                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
                //string file = @"c:/windows/notepad.exe"; 
                psi.Arguments = " /select," + imageList.videoPath;
                System.Diagnostics.Process.Start(psi);

            }
        }
        /// <summary>
        /// 修复视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRepaier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
                int VID = Convert.ToInt32(btn.Tag);

                var video = unitOfWork.DOperationVideo.GetByID(VID);
                string videoPath = video.videoPath;

                uint IsBad = 0;
                EXPORTS.QCAP_DIAGNOSE_FILE(ref videoPath, ref IsBad);
                if (IsBad !=1)
                {
                    string repairePath = videoPath.Replace(".MP4", "_R.MP4");
                    EXPORTS.QCAP_REPAIR_FILE(ref videoPath, ref repairePath);
                    video.videoPath = repairePath;
                    unitOfWork.DOperationVideo.Update(video);
                    unitOfWork.Save();

                    MessageTip msg = new MessageTip("视频修复成功！");
                    msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg.ShowDialog();
                }
                else
                {
                    MessageTip msg = new MessageTip("该视频不需要修复！");
                    msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageTip msg = new MessageTip("暂无视频！");
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.ShowDialog();
            }
        }


       
    }
}
