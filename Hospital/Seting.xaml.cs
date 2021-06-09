using QCAP.NET;
using System;
using System.Collections.Generic;
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
    /// Seting.xaml 的交互逻辑
    /// </summary>
    public partial class Seting : Window
    {

        public uint m_hCapDev = 0x00000000;
        private List<SimpleModel> cameraList = VideoHelper.GetAllCameras();
        public Seting()
        {
            InitializeComponent();
            cameraList = VideoHelper.GetAllCameras();
            if (cameraList.Count == 0)
            {
                return;
            }
            XCameraList.ItemsSource = cameraList;
            XCameraList.Text = XmlHelper.GetValue("CaptureStr");
            m_hCapDev = uint.Parse(XmlHelper.GetValue("m_hCapDev"));
            Audio_Com.Text = XmlHelper.GetValue("AudioInputStr");
            FolderPath.Text= XmlHelper.GetValue("Selectpath");//设置默认文件路径
            //m_hCapDev= uint.TryParse(XmlHelper.GetValue("m_hCapDev"),out uint x); 
            // string a = XmlHelper.GetValue("VideoInputStr");
            Video_Com.SelectedValue = XmlHelper.GetValue("VideoInputStr");
            Video_SecondCom.SelectedValue = XmlHelper.GetValue("VideoSecondInputStr");

            Malv_com.SelectedValue= XmlHelper.GetValue("Malv"); 
            Video_type.Text= XmlHelper.GetValue("DefultVideo");
            Image_com.Text = XmlHelper.GetValue("DefultImage");
         


            this.Fen_com.Text = XmlHelper.GetValue("DefultFen"); ;
            this.FileSize_Txt.Text=XmlHelper.GetValue("DefultSize");
             this.Time_Text.Text=XmlHelper.GetValue("DefultTime");

            if (XmlHelper.GetValue("DefultTimeCheck") == "1")
            {
                this.Time.IsChecked = true;
            }
            if (XmlHelper.GetValue("DefultSizeCheck") == "1")
            {
                this.FileCheck.IsChecked = true;
            }

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 音频输入总线设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var AudioInputStr = this.Audio_Com.Text.ToString();
            if (m_hCapDev != 0x00000000)
            {

                switch (AudioInputStr)
                {
                    case "EMBEDDED AUDIO":
                        EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev, (uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_EMBEDDED_AUDIO);
                        break;
                    case "LINE-IN":
                        EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev, (uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_LINE_IN);
                        break;


                }
            }

        }
        /// <summary>
        /// 视频输入总线设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Video_Com_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var VideoInputStr = this.Video_Com.SelectedValue;

                //XmlHelper.SetValue("VideoInputStr", VideoInputStr.ToString());

                if (m_hCapDev != 0x00000000)
                {
                    switch (VideoInputStr)
                    {
                        case "HDMI":
                            EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_HDMI);
                            break;
                        case "DVI-D":
                            EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_DVI_D);
                            break;
                        case "COMPONENTS(YCBCR)":
                            EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_COMPONENTS);
                            break;
                        case "DVI-A ( RGB )":
                            EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_DVI_A);
                            break;
                        case "SDI":
                            EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_SDI);
                            break;
                        case "COMPOSITE":
                            EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_COMPOSITE);
                            break;
                        case "S-VIDEO":
                            EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_SVIDEO);
                            break;
                        case "AUTO":
                            EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_AUTO);
                            break;

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        private void XCameraList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var CaPtureInputStr = this.XCameraList.SelectedValue;
            //XmlHelper.SetValue("CaptureStr", CaPtureInputStr.ToString());
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请设置默认文件存放路径";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    //System.Windows.MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    MessageTip msg1 = new MessageTip("文件夹路径不能为空！");
                    msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    msg1.Show();
                    return;
                }
                this.FolderPath.Text = dialog.SelectedPath;
                XmlHelper.SetValue("Selectpath", dialog.SelectedPath);
            }
        }
        /// <summary>
        /// 设置默认图片类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_com_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Iamge = this.Image_com.SelectedValue;
            XmlHelper.SetValue("DefultImage", Iamge.ToString());
        }
        /// <summary>
        /// 设置默认视频类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Video_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var CaPtureInputStr = this.Video_type.SelectedValue;
            XmlHelper.SetValue("DefultVideo", CaPtureInputStr.ToString());
        }

        private void Fen_com_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var CaPtureInputStr = this.Fen_com.SelectedValue.ToString();
          
            XmlHelper.SetValue("DefultFen", CaPtureInputStr);



            MainWindow._mainWindow._DefultFen = CaPtureInputStr;

        }

        private void FileSize_Txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);
        }

        private void FileSize_Txt_MouseLeave(object sender, MouseEventArgs e)
        {
            var CaPtureInputStr = this.FileSize_Txt.Text.ToString();

            XmlHelper.SetValue("DefultSize", CaPtureInputStr);
        }

        private void TextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            var CaPtureInputStr = this.Time_Text.Text.ToString();

            XmlHelper.SetValue("DefultTime", CaPtureInputStr);
        }

        private void FileCheck_Click(object sender, RoutedEventArgs e)
        {

            if (this.FileCheck.IsChecked == true)
            {
                XmlHelper.SetValue("DefultSizeCheck", "1");
                this.Time.IsChecked = false;
                XmlHelper.SetValue("DefultTimeCheck", "0");
            }
        }

        private void Time_Click(object sender, RoutedEventArgs e)
        {
            if (this.Time.IsChecked == true)
            {
                XmlHelper.SetValue("DefultTimeCheck", "1");
                this.FileCheck.IsChecked = false;
                XmlHelper.SetValue("DefultSizeCheck", "0");
            }
        }
        /// <summary>
        /// 保存视频设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow._mainWindow._IsRecordStatus == true)
            {
                MessageTip mssg = new MessageTip("视频录制中禁止更改设置！");
                mssg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                mssg.Show();
                return;
            }
          
            //保存采集卡类型
            var CaPtureInputStr = this.XCameraList.SelectedValue;
            XmlHelper.SetValue("CaptureStr", CaPtureInputStr.ToString());

            var audioInputStr = this.Audio_Com.SelectedValue;
            XmlHelper.SetValue("AudioInputStr", audioInputStr.ToString());
            //设置默认图片类型
            var Iamge = this.Image_com.SelectedValue;
            XmlHelper.SetValue("DefultImage", Iamge.ToString());

            MainWindow._mainWindow._DefultImage = Iamge.ToString();

            //设置视频输入总线
            string VideoInputStr = this.Video_Com.SelectedValue.ToString();
            string VideoSecondInputStr = this.Video_SecondCom.SelectedValue.ToString();


            //副设置视频输入总线


            if (VideoInputStr==VideoSecondInputStr)
            {
                MessageTip msgTip = new MessageTip("主副视频源不能一致！");
                msgTip.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msgTip.Show();
                return;
            }
            else
            {
                XmlHelper.SetValue("VideoInputStr", VideoInputStr.ToString());
                XmlHelper.SetValue("VideoSecondInputStr", VideoSecondInputStr.ToString());
            }
           
            //设置默认视频类型
            var VideoType = this.Video_type.SelectedValue;
            XmlHelper.SetValue("DefultVideo", VideoType.ToString());

            MainWindow._mainWindow._DefultVideo = VideoType.ToString();

           //设置分辨率
           var FenInputStr = this.Fen_com.SelectedValue.ToString();

            XmlHelper.SetValue("DefultFen", FenInputStr);

            MainWindow._mainWindow._DefultFen = FenInputStr;
            //自动分段根据大小
            if (this.FileCheck.IsChecked == true)
            {
                XmlHelper.SetValue("DefultSizeCheck", "1");
                this.Time.IsChecked = false;
                XmlHelper.SetValue("DefultTimeCheck", "0");

                MainWindow._mainWindow._DefultSizeCheck = "1";
                MainWindow._mainWindow._DefultTimeCheck = "0";
            }
            else
            {
                XmlHelper.SetValue("DefultSizeCheck", "0");
                MainWindow._mainWindow._DefultSizeCheck = "0";
            }
            //自动分段根据时间
            if (this.Time.IsChecked == true)
            {
                XmlHelper.SetValue("DefultTimeCheck", "1");
                this.FileCheck.IsChecked = false;
                XmlHelper.SetValue("DefultSizeCheck", "0");

                MainWindow._mainWindow._DefultSizeCheck = "0";
                MainWindow._mainWindow._DefultTimeCheck = "1";
            }
            else
            {
                XmlHelper.SetValue("DefultTimeCheck", "0");
                MainWindow._mainWindow._DefultTimeCheck = "0";
            }
            var SizePtureInputStr = this.FileSize_Txt.Text.ToString();

            XmlHelper.SetValue("DefultSize", SizePtureInputStr);
            MainWindow._mainWindow._DefultSize = SizePtureInputStr;

            var TimePtureInputStr = this.Time_Text.Text.ToString();

            XmlHelper.SetValue("DefultTime", TimePtureInputStr);


            var _Malv_com = this.Malv_com.Text.ToString();

            XmlHelper.SetValue("Malv", _Malv_com);
            
            

            MainWindow._mainWindow._DefultTime = TimePtureInputStr;
            //保存文件存储位置
            XmlHelper.SetValue("Selectpath", this.FolderPath.Text);

            MessageTip msg = new MessageTip("保存成功！");
            msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            msg.Show();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Video_SecondCom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var VideoInputStr = this.Video_SecondCom.SelectedValue;

           // XmlHelper.SetValue("VideoSecondInputStr", VideoInputStr.ToString());
        }

        private void Malv_com_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string malv = this.Malv_com.SelectedValue.ToString();

            MainWindow._mainWindow._Malv = malv; 
        }

        private void Fen_com_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            string fen = this.Fen_com.SelectedValue.ToString();
            MainWindow._mainWindow._DefultFen = fen;
        }
    }
}
