
using QCAP.NET;
using Smart.Entity;
using Smart.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using System.Threading;

namespace Hospital
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);



        public string _DefultSizeCheck = XmlHelper.GetValue("DefultSizeCheck");

        public string _DefultSize = XmlHelper.GetValue("DefultSize");

        public string _DefultTimeCheck = XmlHelper.GetValue("DefultTimeCheck");

        public string _DefultTime = XmlHelper.GetValue("DefultTime");

        public string _DefultImage = XmlHelper.GetValue("DefultImage");

        public string _DefultFen = XmlHelper.GetValue("DefultFen");

        public string _DefultVideo = XmlHelper.GetValue("DefultVideo");
        public string _Malv = XmlHelper.GetValue("Malv");

        public DateTime CurrentDate = DateTime.Now;

        public string PauseDateStr = "";

        //public static extern void OutputDebugString(string message);
        protected UnitOfWork unitOfWork = new UnitOfWork();

        public int IsFullScreen = 0;
        private int CurrentID = 0;
        public int CurrentRecordID = 0;

        public int IsRecording = 0;

        public int currentTime = 0;
        //存储文件所用文件夹路径
        private string CurrentFolderPath = "";

        private int CurrentVideoID = 0;

        public bool _IsFanzhuan = true;//是否是翻转状态

        public bool _IsRecordStatus = false;

        public bool _IsPauseStatus = false;

        public RecordStatus rs;
        public RecordTime rt;
        public Control cn;

        private OperationRecord oRecord = new OperationRecord();
        public DateTime _RecordTime;

        public bool m_bNoSignal = true;

        public uint m_hCapDev = 0x00000000;                         // STREAM CAPTURE DEVICE

        public uint m_hCloneCapDev = 0x00000000;                // CLONE STREAM CAPTURE DEVICE

        public static MainWindow _mainWindow;

        public uint iceNumber = 0;

        public static EXPORTS.PF_FORMAT_CHANGED_CALLBACK m_pFormatChangedCB;

        public static EXPORTS.PF_NO_SIGNAL_DETECTED_CALLBACK m_pNoSignalDetectedCB;

        public static EXPORTS.PF_SIGNAL_REMOVED_CALLBACK m_pSignalRemovedCB;

        public static EXPORTS.PF_VIDEO_PREVIEW_CALLBACK m_pPreviewVideoCB;

        public static EXPORTS.PF_AUDIO_PREVIEW_CALLBACK m_pPreviewAudioCB;

        public MySetupControl m_cSetupControl = new MySetupControl();

        public MyChannelControl m_cChannelControl_LIVE = new MyChannelControl();

        // string m_strChipName = "SA7160 PCI";

        string m_strChipName = XmlHelper.GetValue("CaptureStr");    // for UB530 and UB530G

        public string m_strFormatChangedOutput = "";

        // DEVICE PROPERTY
        //
        //public uint m_hCapDev = 0x00000000;                         // STREAM CAPTURE DEVICE


        //public uint m_hCloneCapDev = 0x00000000;                // CLONE STREAM CAPTURE DEVICE

        //  FORMAT CHANGED CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_format_changed(uint pDevice, uint nVideoInput, uint nAudioInput, uint nVideoWidth, uint nVideoHeight, uint bVideoIsInterleaved, double dVideoFrameRate, uint nAudioChannels, uint nAudioBitsPerSample, uint nAudioSampleFrequency, uint pUserData)
        {

            // OUTPUT FORMAT CHANGED MESSAGE
            //
            string strOutput = @"FORMAT CHANGED : pDevice : " + pDevice.ToString() + " , " + "nVideoInput : " + nVideoInput.ToString() + " , " +

                                        "nAudioInput : " + nAudioInput.ToString() + " , " + "nVideoWidth : " + nVideoWidth.ToString() + " , " +

                                        "nVideoHeight : " + nVideoHeight.ToString() + " , " + "bVideoIsInterleaved : " + bVideoIsInterleaved.ToString() + " , " +

                                        "dVideoFrameRate : " + dVideoFrameRate.ToString() + " , " + "nAudioChannels : " + nAudioChannels.ToString() + " , " +

                                        "nAudioBitsPerSample : " + nAudioBitsPerSample.ToString() + " , " + "nAudioSampleFrequency : " + nAudioSampleFrequency.ToString() + " , " +

                                        "pUserData : " + pUserData.ToString() + " \n";

            OutputDebugString(strOutput);

            uint nVH = 0;

            string strFrameType = " P ";

            string strVideoInput = "", strAudioInput = "";

            if (nVideoInput == 0) { strVideoInput = "COMPOSITE"; }
            if (nVideoInput == 1) { strVideoInput = "SVIDEO"; }
            if (nVideoInput == 2) { strVideoInput = "HDMI"; }

            if (nVideoInput == 3) { strVideoInput = "DVI_D"; }
            if (nVideoInput == 4) { strVideoInput = "COMPONENTS (YCBCR)"; }
            if (nVideoInput == 5) { strVideoInput = "DVI_A (RGB / VGA)"; }

            if (nVideoInput == 6) { strVideoInput = "SDI"; }
            if (nVideoInput == 7) { strVideoInput = "AUTO"; }

            if (nAudioInput == 0) { strAudioInput = "EMBEDDED_AUDIO"; }
            if (nAudioInput == 1) { strAudioInput = "LINE_IN"; }

            if (bVideoIsInterleaved == 1) { nVH = nVideoHeight; } else { nVH = nVideoHeight; }

            if (bVideoIsInterleaved == 1) { strFrameType = " I "; } else { strFrameType = " P "; }

            m_strFormatChangedOutput = /*XmlHelper.GetValue("VideoInputStr") + @" INFO : "*/ nVideoWidth.ToString() + " x " + nVH.ToString() + strFrameType +

                ", 视频输入:" + strVideoInput + "," + " 音频输入: " + strAudioInput + " \n";
            //string DefultVideo = XmlHelper.GetValue("CurrentVideoInput");
            //if (DefultVideo == "1")
            //{
            //    m_strFormatChangedOutput = XmlHelper.GetValue("VideoSecondInputStr") + @" INFO : " + nVideoWidth.ToString() + " x " + nVH.ToString() + strFrameType + " @" + dVideoFrameRate.ToString() +

            // " FPS , " + nAudioChannels.ToString() + " CH x " + nAudioBitsPerSample.ToString() + " BITS x " + nAudioSampleFrequency.ToString() + " HZ , " +

            // " VIDEO INPUT : " + strVideoInput + " , " + " AUDIO INPUT : " + strAudioInput + " \n";
            //}
            // NO SIGNAL
            //       
            if (nVideoWidth == 0 && nVideoHeight == 0 && dVideoFrameRate == 0.0 && nAudioChannels == 0 && nAudioBitsPerSample == 0 && nAudioSampleFrequency == 0)
            {
                m_bNoSignal = true;
            }
            else
            {
                m_bNoSignal = false;
            }

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        // PREVIEW VIDEO CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_preview_video_buffer(uint pDevice, double dSampleTime, uint pFrameBuffer, uint nFrameBufferLen, uint pUserData)
        {
            //string strOutput = @"on_process_preview_video_buffer => pDevice : " + pDevice.ToString() + " , dSampleTime : " + dSampleTime.ToString() + " , pFrameBuffer : " + pFrameBuffer.ToString() + " , nFrameBufferLen : " + nFrameBufferLen.ToString() + " , pUserData : " + pUserData.ToString() + " \n";

            //OutputDebugString(strOutput);

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        // PREVIEW AUDIO CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_preview_audio_buffer(uint pDevice, double dSampleTime, uint pFrameBuffer, uint nFrameBufferLen, uint pUserData)
        {
            //string strOutput = @"on_process_preview_audio_buffer => pDevice : " + pDevice.ToString() + " , dSampleTime : " + dSampleTime.ToString() + " , pFrameBuffer : " + pFrameBuffer.ToString() + " , nFrameBufferLen : " + nFrameBufferLen.ToString() + " , pUserData : " + pUserData.ToString() + " \n";

            //OutputDebugString(strOutput);

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        // NO SIGNAL DETEACTED CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_no_signal_detected(uint pDevice, uint nVideoInput, uint nAudioInput, uint pUserData)
        {
            OutputDebugString("No Signal Detected  \n");

            m_bNoSignal = true;

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }

        // SIGNAL REMOVED CALLBACK FUNCTION
        //
        EXPORTS.ReturnOfCallbackEnum on_process_signal_removed(uint pDevice, uint nVideoInput, uint nAudioInput, uint pUserData)
        {
            OutputDebugString(" Signal Removed \n");

            m_bNoSignal = true;

            return EXPORTS.ReturnOfCallbackEnum.QCAP_RT_OK;
        }



        public MainWindow()
        {
            InitializeComponent();
            _mainWindow = this;

            this.StopRecord.IsEnabled = false;
            //加载手术列表名称
            List<String> OperaNames = XmlHelper.GetOperations();
            this.operName_Com.ItemsSource = OperaNames;
            //加载手术部位名称
            List<String> OperaParts = XmlHelper.GetOperationParts();
            this.operPart_Com.ItemsSource = OperaParts;

            //加载手术医生
            List<string> OperaDoctors = XmlHelper.GetStoreUsers();
            this.opeDoctor_Com.ItemsSource = OperaDoctors;

            #region 加载当天手术用户
            // 查询当天数据

            var paintList = unitOfWork.DOperationRecord.GetAll().OrderByDescending(p => p.ID).Take(3).ToList();

            if (paintList != null && paintList.Count() > 0)
            {

                if (paintList.FirstOrDefault() != null)
                {
                    //CurrentID = paintList.FirstOrDefault().ID;
                    //SetPainetUser(CurrentID);
                    // AddRecord(CurrentID);
                }


            }
            this.patintName_Com.ItemsSource = paintList;

            #endregion


            this.operName_Com.SelectedValue = "";
            this.operPart_Com.SelectedValue = "";
            this.opeDoctor_Com.SelectedValue = "";




            //var ImageList = unitOfWork.DOperationImage.Get(p => p.operationID == CurrentRecordID);
            //lb.ItemsSource = ImageList;


            //var listFile = unitOfWork.DOperationVideo.Get(p=>p.operationID== CurrentRecordID);

            //DG1.ItemsSource = listFile;
            this.WindowState = WindowState.Maximized;



            // _form1.Height =Convert.ToInt32( winForm.Height);
            //_form1.Width = Convert.ToInt32(winForm.Width);
            // _form1.WindowState =FormWindowState.Maximized;
            m_cChannelControl_LIVE = new MyChannelControl();

            m_cChannelControl_LIVE = new MyChannelControl();

            m_cChannelControl_LIVE.Left = 0;

            m_cChannelControl_LIVE.Top = 0;


            m_cChannelControl_LIVE.Width = Convert.ToInt32(this.winForm.Width);

            m_cChannelControl_LIVE.Height = Convert.ToInt32(this.winForm.Height);

            m_cChannelControl_LIVE.Visible = true;

            m_cChannelControl_LIVE.Show();
            winForm.Child = m_cChannelControl_LIVE;

            //CloneChannelPanel.Left = this.Width - 320;

            //CloneChannelPanel.Top = this.Height - 240;

            //CloneChannelPanel.Width = 320;

            //CloneChannelPanel.Height = 240;







            this.btnRecordStart.IsEnabled = false;//开始录像
            this.CameraShow.IsEnabled = false;//拍照
            //this.ZhiBoBtn.IsEnabled = false;//直播
            this.StopRecord.IsEnabled = false;//停止录制
            this.CameraBtn.IsEnabled = false;//拍照ICON

            this.stopBtn.IsEnabled = false;//停止录制ICON

            //软件启动候磁盘预警

            CurrentFolderPath = XmlHelper.GetValue("Selectpath");

            string ciPan = CurrentFolderPath.Substring(0, 1);

            long Free = GetHardDiskSpace(ciPan);
            if (Free < 10)
            {
                MessageTip msg1 = new MessageTip("您的电脑存储空间已经小于10G！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();

            }


        }

        public void WriteToScreen()
        {
            var paintList = unitOfWork.DPatient.Get(P => P.status == 0).OrderBy(p => p.ID).ToList();
            if (paintList != null && paintList.Count() > 0)
            {
                paintList = paintList.Where(P => P.operationDate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")).ToList();
                if (paintList.FirstOrDefault() != null)
                {
                    CurrentID = paintList.FirstOrDefault().ID;
                    // SetPainetUser(CurrentID);
                    // AddRecord(CurrentID);
                }


            }
            this.patintName_Com.ItemsSource = paintList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Operation op = new Operation
            {
                Left = 360,
                Top = 80
            };
            op.ShowDialog();
        }

        private void SetMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            Operation op = new Operation
            {
                Left = 360,
                Top = 80
            };
            op.ShowDialog();

        }

        private void Button_CaseManage(object sender, RoutedEventArgs e)
        {
            CaseManage op = new CaseManage
            {
                Left = 360,
                Top = 80
            };
            op.ShowDialog();
        }

        private void ButtonRecord_Click(object sender, RoutedEventArgs e)
        {
            RecordManage op = new RecordManage
            {
                Left = 360,
                Top = 80
            };
            op.ShowDialog();
        }
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {

            WindowState = WindowState.Minimized;

            //录制过程中允许最小化

            //if (_IsRecordStatus == false)
            //{
                
            //}
            //else
            //{

            //    MessageTip msg = new MessageTip("录制过程中禁止最小化！")
            //    {
            //        WindowStartupLocation = WindowStartupLocation.CenterScreen
            //    };
            //    msg.Show();

            //}


        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            XmlHelper.SetValue("CurrentVideoInput", "0");
            if (_IsRecordStatus == false)
            {
                //Application.Current.Shutdown();
                System.Environment.Exit(0);
            }
            else
            {

                MessageTip msg = new MessageTip("停止当前录制以后才可以退出！")
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                msg.Show();

            }

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// 设置页面默认病人
        /// </summary>
        /// <param name="ID"></param>
        public void SetRecordPainetUser(int ID)
        {
            CurrentRecordID = ID;
            var painetUser = unitOfWork.DOperationRecord.GetByID(ID);
            if (painetUser != null)
            {
                int sexIndex = painetUser.sex == "男" ? 0 : 1;

                this.painetSex_Com.SelectedIndex = sexIndex;

                this.painetAge_Txt.Text = painetUser.age.ToString();
                this.painetNum_Txt.Text = painetUser.hospNumber;

                this.operName_Com.Text = painetUser.operaName;
                this.operPart_Com.Text = painetUser.operativesite;
                this.opeDoctor_Com.Text = painetUser.surgeon;
                if (this.patintName_Com.Text != painetUser.username)
                {
                    var ImageList = unitOfWork.DOperationImage.Get(p => p.operationID == ID).FirstOrDefault();
                   


                    var listFile = unitOfWork.DOperationVideo.Get(p => p.operationID == ID).FirstOrDefault();

                    if (ImageList != null || listFile != null)
                    {

                        //是否继续手术判断
                        MessageSure msg = new MessageSure(ID)
                        {
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        };
                        msg.Show();
                    }

                    
                    //this.DG1.ItemsSource = listFile;
                }
                this.patintName_Com.Text = painetUser.username;
            }
            else
            {
                this.painetAge_Txt.Text = "";
                this.painetNum_Txt.Text = "";

                this.operName_Com.Text = "";
                this.operPart_Com.Text = "";
                this.opeDoctor_Com.Text = "";
                lb.ItemsSource = null;


                DG1.ItemsSource = null;
            }




        }
        /// <summary>
        /// 设置页面默认病人
        /// </summary>
        /// <param name="ID"></param>
        public void SetPainetUser(int ID)
        {
            CurrentID = ID;
            var painetUser = unitOfWork.DPatient.GetByID(ID);
            if (painetUser != null)
            {
                int sexIndex = painetUser.sex == "男" ? 0 : 1;
                this.patintName_Com.Text = painetUser.username;
                this.painetSex_Com.SelectedIndex = sexIndex;

                this.painetAge_Txt.Text = painetUser.age.ToString();
                this.painetNum_Txt.Text = painetUser.hospNumber;

                this.operName_Com.Text = painetUser.operaName;
                this.operPart_Com.Text = painetUser.operativesite;
                this.opeDoctor_Com.Text = painetUser.surgeon;
                var redcordPaint = unitOfWork.DOperationRecord.Get(p => p.ID == CurrentRecordID).FirstOrDefault();
                if (redcordPaint != null)
                {
                    var ImageList = unitOfWork.DOperationImage.Get(p => p.operationID == CurrentRecordID).FirstOrDefault();
                 


                    var listFile = unitOfWork.DOperationVideo.Get(p => p.operationID == CurrentRecordID).FirstOrDefault();


                    if (ImageList != null && listFile != null)
                    {
                        MessageSure msg = new MessageSure(CurrentRecordID)
                        {
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        };
                        msg.Show();

                    }
                   
                  


                }

            }
            else
            {
                this.painetAge_Txt.Text = "";
                this.painetNum_Txt.Text = "";

                this.operName_Com.Text = "";
                this.operPart_Com.Text = "";
                this.opeDoctor_Com.Text = "";
                lb.ItemsSource = null;


                DG1.ItemsSource = null;
            }


        }
        /// <summary>
        /// 病人改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatintName_Com_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SetRecordPainetUser(Convert.ToInt32(this.patintName_Com.SelectedValue));

            currentTime = currentTime + 1;
        }
        /// <summary>
        /// 新手术
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Button_Click_New(object sender, RoutedEventArgs e)
        {
            #region 加载当天手术用户
            // 查询当天数据

            var paintList = unitOfWork.DOperationRecord.GetAll().OrderByDescending(p => p.ID).Take(3).ToList();

            if (paintList != null && paintList.Count() > 0)
            {

                if (paintList.FirstOrDefault() != null)
                {
                    //CurrentID = paintList.FirstOrDefault().ID;
                    //SetPainetUser(CurrentID);
                    // AddRecord(CurrentID);
                }


            }
            this.patintName_Com.ItemsSource = paintList;

            #endregion

            if (_IsRecordStatus == false)
            {
                CurrentRecordID = 0;
                this.btnRecordStart.IsEnabled = false;//开始录像
                this.CameraShow.IsEnabled = false;//拍照
                //this.ZhiBoBtn.IsEnabled = false;//直播
                this.StopRecord.IsEnabled = false;//停止录制
                this.CameraBtn.IsEnabled = false;//拍照ICON
                                                 //this.zHiBtn.IsEnabled = false;//直播ICON
                this.stopBtn.IsEnabled = false;//停止录制ICON
                this.btnSave.IsEnabled = true;

                //this.Nosiganlr.Visibility = Visibility.Visible;
                //this.FullStack.Visibility = Visibility.Hidden;



                this.patintName_Com.Text = "";

                //Application._Application.
                this.painetAge_Txt.Text = "";
                this.painetNum_Txt.Text = "";
                this.operName_Com.Text = "";
                this.operPart_Com.Text = "";
                this.opeDoctor_Com.Text = "";
                //CurrentID = CurrentID + 1;f
                //SetPainetUser(CurrentID);
                //AddRecord(CurrentID);

                //var ImageList = unitOfWork.DOperationImage.Get(p => p.operationID == CurrentRecordID);
                lb.ItemsSource = null;


                //var listFile = unitOfWork.DOperationVideo.Get(p => p.operationID == CurrentRecordID);

                DG1.ItemsSource = null;
            }
            else
            {
                //System.Windows.MessageBox.Show("请您停止当前录制！");

                MessageTip msg = new MessageTip("请您停止当前录制！")
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                msg.Show();
            }
        }
        /// <summary>
        /// 添加一条手术记录
        /// </summary>
        /// <param name="rID"></param>
        /// <returns></returns>
        public void AddRecord(int rID,string currentPath)
        {
            var painetUser = unitOfWork.DPatient.GetByID(CurrentID);
            var record = unitOfWork.DOperationRecord.Get(p => p.username == painetUser.username).FirstOrDefault();
            if (record == null)
            {

                OperationRecord opRecord = new OperationRecord();


                opRecord.PatintUserID = painetUser.ID;
                opRecord.sex = painetUser.sex;
                opRecord.age = painetUser.age;
                opRecord.username = painetUser.username;
                opRecord.hospNumber = painetUser.hospNumber;
                opRecord.surgeon = this.opeDoctor_Com.Text;
                opRecord.createtime = DateTime.Now;
                opRecord.status = 0;
                opRecord.operaName = this.operName_Com.Text.ToString();
                opRecord.operativesite = this.operPart_Com.Text.ToString();
                opRecord.surgeon = this.opeDoctor_Com.Text.ToString();
                opRecord.des = currentPath;
                unitOfWork.DOperationRecord.Insert(opRecord);
                unitOfWork.Save();

                oRecord = opRecord;

                CurrentRecordID = opRecord.ID;
            }
            else
            {
                record.operaName = this.operName_Com.Text.ToString();
                record.operativesite = this.operPart_Com.Text.ToString();
                record.surgeon = this.opeDoctor_Com.Text.ToString();
                unitOfWork.DOperationRecord.Update(record);
                unitOfWork.Save();
                oRecord = record;
                CurrentRecordID = record.ID;
            }
            //设置医生列表
            //XmlHelper.SetUsers(this.opeDoctor_Com.Text.ToString());
            ////设置手术名称
            //XmlHelper.StoreOperations(this.operName_Com.Text.ToString());
            ////设置手术部位
            //XmlHelper.StoreOperaPart(this.operPart_Com.Text.ToString());

            //加载手术列表名称
            List<String> OperaNames = XmlHelper.GetOperations();
            this.operName_Com.ItemsSource = OperaNames;
            //加载手术部位名称
            List<String> OperaParts = XmlHelper.GetOperationParts();
            this.operPart_Com.ItemsSource = OperaParts;

            //加载手术医生
            List<string> OperaDoctors = XmlHelper.GetStoreUsers();
            this.opeDoctor_Com.ItemsSource = OperaDoctors;


        }

        public void CameraShow_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentRecordID == 0)
            {
                MessageTip msg = new MessageTip("请您先添加病人手术信息！");
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.Show();
                return;
            }
            string subPath = CurrentFolderPath + "\\Iamge\\";
            if (false == System.IO.Directory.Exists(subPath))
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory(subPath);
            }


            // string m_strCurrentDir = Directory.GetCurrentDirectory();
            //m_hCapDev = _form1.m_hCapDev;

            if (m_hCapDev != 0)
            {

                string ImageType = _DefultImage;
                int countOrder = 1;

                List<OperationImage> vList = unitOfWork.DOperationImage.Get(p => p.operationID == CurrentRecordID).ToList();
                if (vList != null)
                {
                    countOrder = vList.Count() + 1;
                }


                if (ImageType == "JPG")
                {
                    string vName = countOrder + ".JPG";
                    string fileName = subPath + vName;
                    string ImagePath = fileName;

                   


                    EXPORTS.QCAP_SNAPSHOT_JPG(m_hCapDev, ref fileName, 80, 1, 0);
                    Task task_2 = Task.Run(task_YS);
                    task_2.Wait();  //注释打开则等待task_2延时，注释掉则不等待
                    InsertImage(vName, ImagePath);
                    if (IsFullScreen == 1)
                    {
                        ImageTip iT = new ImageTip(vName);
                        iT.WindowStartupLocation = WindowStartupLocation.Manual;
                        iT.Left = System.Windows.SystemParameters.PrimaryScreenWidth * 48 / 100;
                        iT.Top = System.Windows.SystemParameters.PrimaryScreenHeight - 180;
                        iT.Topmost = true;
                        iT.Show();
                        Thread.Sleep(1000);  //form2窗体显示1秒

                        iT.Close();  //form2窗体关闭
                    }


                }
                else
                {
                    string vName = countOrder + ".BMP";
                    string fileName = subPath + vName;
                    string ImagePath = fileName;
                    EXPORTS.QCAP_SNAPSHOT_BMP(m_hCapDev, ref fileName);
                    Task task_2 = Task.Run(task_YS);
                    task_2.Wait();  //注释打开则等待task_2延时，注释掉则不等待
                    InsertImage(vName, ImagePath);
                }

            }


        }


        public static async Task task_YS()
        {
            await Task.Delay(1000);

        }
        #region 录像 拍照

        /// <summary>
        /// 开始录像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnRecordStart_Click(object sender, RoutedEventArgs e)
        {
            //验证磁盘空间
            CurrentFolderPath = XmlHelper.GetValue("Selectpath");

            string ciPan = CurrentFolderPath.Substring(0, 1);



            long Free = GetHardDiskSpace(ciPan);
            if (Free < 10)
            {
                MessageTip msg1 = new MessageTip("您的电脑存储空间已经小于10G！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();

            }



            if (CurrentRecordID == 0)
            {
                MessageTip msg = new MessageTip("请您先添加病人手术信息！");
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.Show();
                return;
            }

            this.btnSave.IsEnabled = false;//保存资料
            //this.btnNew.IsEnabled = false;//新手术
            //this.ExitButton.IsEnabled = false;//直播

            if (this.textRecordStart.Text.ToString() == "开始录像")
            {


                _IsRecordStatus = true;
                if (rt != null && rt._state == RecordTime.State.Pause)
                {
                    rt._state = RecordTime.State.Start;
                }
                else
                {
                    rs = new RecordStatus(CurrentRecordID);
                    double sx = Convert.ToDouble(XmlHelper.GetValue("StatusX"));
                    double sy = Convert.ToDouble(XmlHelper.GetValue("StatusY"));

                    if (IsFullScreen == 1)
                    {
                        rs.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 240;
                    }
                    else
                    {
                        rs.Left = System.Windows.SystemParameters.PrimaryScreenWidth * 86 / 100;
                    }
                    rs.Top = 60;
                    rs.Topmost = true;
                    rs.Show();
                    _RecordTime = DateTime.Now;
                    TimeSpan tp = new TimeSpan(0, 0, 0, 0, 0);
                    rt = new RecordTime(tp);

                    if (IsFullScreen == 1)
                    {
                        rt.Left = 100;
                    }
                    else
                    {
                        rt.Left = System.Windows.SystemParameters.PrimaryScreenWidth * 20 / 100;
                    }
                    rt.Top = 60;
                    rt.Topmost = true;
                    rt.Show();
                }

                // m_hCapDev = _form1.m_hCapDev;


                this.StopRecord.IsEnabled = true;

                this.textRecordStart.Text = "暂停录像";
                //m_btnRecordStop1.Enabled = true;


                string subPath = CurrentFolderPath + "\\Video\\";
                if (false == System.IO.Directory.Exists(subPath))
                {
                    //创建pic文件夹
                    System.IO.Directory.CreateDirectory(subPath);
                }
                int countOrder = 1;
                List<OperationVideo> vList = unitOfWork.DOperationVideo.Get(p => p.operationID == CurrentRecordID).ToList();
                if (vList != null)
                {
                    countOrder = vList.Count() + 1;
                }

                string ImageType = _DefultVideo;
                string vName = countOrder.ToString() + ".MP4";//oRecord.username +
                bool IsMp4 = true;
                if (ImageType != "MP4")
                {
                    IsMp4 = false;
                    vName = countOrder.ToString() + ".AVI";//oRecord.username +
                }
                //DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".mp4";
                string fileName = subPath + vName;

                // m_bSupportGPU1 = m_checkGPU1.Checked;
                if (IsRecording == 0)
                {
                    #region 开始录像
                    if (m_hCapDev != 0)
                    {
                        if (!string.IsNullOrEmpty(_Malv))
                        {
                            int maLv = Convert.ToInt32(_Malv);
                            EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev, iceNumber, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12* 1024 * 1024, (uint)maLv, 0, 0, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_OFF);

                        }

                        if (IsMp4 == true)
                        {
                            EXPORTS.QCAP_SET_AUDIO_RECORD_PROPERTY(m_hCapDev, iceNumber, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_AAC);
                        }
                        else
                        {
                            EXPORTS.QCAP_SET_AUDIO_RECORD_PROPERTY(m_hCapDev, iceNumber, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_PCM);
                        }

                        string videoFen = _DefultFen;

                        if (videoFen == "1280x720")
                        {
                            EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev, iceNumber, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 15, 0, 0, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_2_3);

                        }
                        else if (videoFen == "960x540")
                        {
                            EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev, iceNumber, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 15, 0, 0, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_1_2);

                        }
                        else if (videoFen == "720x480")
                        {
                            EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev, iceNumber, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 15, 0, 0, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_1_4);

                        }
                        else
                        {
                            EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev, iceNumber, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 15, 0, 0, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_OFF);

                        }
                       


                        string str_avi_name1 = fileName;

                        string pszNULL = null;
                        //string _DefultSizeCheck = XmlHelper.GetValue("DefultSizeCheck");
                        UInt32 Size = Convert.ToUInt32(_DefultSize) * 1024;
                        //string DefultTimeCheck = XmlHelper.GetValue("DefultTimeCheck");
                        Double time = Convert.ToDouble(_DefultTime) * 60;
                        if (_DefultSizeCheck == "1")
                        {

                            EXPORTS.QCAP_START_RECORD(m_hCapDev, iceNumber, ref str_avi_name1, (uint)EXPORTS.RecordFlagEnum.QCAP_RECORD_FLAG_FULL, 0.0, 0.0, 0.0, Size, ref pszNULL);

                        }
                        else
                        {
                            if (_DefultTimeCheck == "1")
                            {
                                EXPORTS.QCAP_START_RECORD(m_hCapDev, iceNumber, ref str_avi_name1, (uint)EXPORTS.RecordFlagEnum.QCAP_RECORD_FLAG_FULL, 0.0, 0.0, time, 0, ref pszNULL);
                            }
                            else
                            {
                                EXPORTS.QCAP_START_RECORD(m_hCapDev, iceNumber, ref str_avi_name1, (uint)EXPORTS.RecordFlagEnum.QCAP_RECORD_FLAG_FULL, 0.0, 0.0, 0.0, 0, ref pszNULL);
                            }

                        }

                        InsertVideo(vName, fileName);
                        //m_bIsRecord1 = true;
                    }

                    EXPORTS.QCAP_SET_VIDEO_DEINTERLACE_TYPE(m_hCapDev, 0);
                    EXPORTS.QCAP_SET_VIDEO_DEINTERLACE(m_hCapDev, 1);
                    #endregion

                }
                else
                {

                    EXPORTS.QCAP_RESUME_RECORD(m_hCapDev, iceNumber);
                }
                _IsPauseStatus = true;
                this.playBtn.Template = this.FindResource("RealPauseRecord") as ControlTemplate;
                RecordTime._recordTime.MessageBtn.Template = this.FindResource("TimePauseBtn") as ControlTemplate;
            }
            else
            {
                _IsPauseStatus = false;
                rt._state = RecordTime.State.Pause;
                CurrentDate = DateTime.Now;
                PauseDateStr = rt.PauseTimeStr;
                this.textRecordStart.Text = "开始录像";
                this.playBtn.Template = this.FindResource("StartRecord") as ControlTemplate;
                RecordTime._recordTime.MessageBtn.Template = this.FindResource("TimeStartBtn") as ControlTemplate;


                EXPORTS.QCAP_PAUSE_RECORD(m_hCapDev, iceNumber);

                this.playBtn.IsEnabled = true;
            }

            this.IsRecording = 1;

        }
        /// <summary>
        /// 停止录像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StopRecord_Click(object sender, RoutedEventArgs e)
        {
            this.IsRecording = 0;
            if (CurrentRecordID == 0)
            {
                MessageTip msg = new MessageTip("请您先添加病人手术信息！");
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.Show();
                return;
            }
            if (_IsRecordStatus == true)
            {

                this.btnNew.IsEnabled = true;//新手术
                this.ExitButton.IsEnabled = true;//直播
                this.btnSave.IsEnabled = false;//保存资料
                rt._state = RecordTime.State.Pause;
                //m_hCapDev = _form1.m_hCapDev;
                _IsPauseStatus = false;//录像状态
                _IsRecordStatus = false;
                rs.Close();
                rt.Close();
                rt = null;
                rs = null;
                this.textRecordStart.Text = "开始录像";

                //m_btnRecordStop1.Enabled = false;

                if (m_hCapDev != 0)
                {
                    EXPORTS.QCAP_STOP_RECORD(m_hCapDev, iceNumber);

                    UpdateVideo();
                    //m_bIsRecord1 = false;
                    //System.Windows.MessageBox.Show("保存文件成功！");
                    //MessageTip msg = new MessageTip("保存文件成功！");
                    //msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    //msg.Show();
                }
            }
            else
            {
                MessageTip msgpaint = new MessageTip("您尚未开始录像！");
                msgpaint.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msgpaint.Show();
            }

            this.playBtn.Template = this.FindResource("StartRecord") as ControlTemplate;



        }
        /// <summary>
        /// 插入录播视频
        /// </summary>
        public void InsertVideo(string VideoName, string FilePath)
        {
            var opeationRecord = unitOfWork.DOperationRecord.GetByID(CurrentRecordID);
            OperationVideo oVideo = new OperationVideo();
            oVideo.name = VideoName;
            oVideo.startTime = DateTime.Now;
            oVideo.operationID = CurrentRecordID;
            oVideo.videoPath = FilePath;
            oVideo.doctor = opeationRecord.surgeon;

            unitOfWork.DOperationVideo.Insert(oVideo);
            unitOfWork.Save();

            CurrentVideoID = oVideo.ID;
        }
        /// <summary>
        /// 插入图片
        /// </summary>
        public void InsertImage(string imageName, string FilePath)
        {
            var opeationRecord = unitOfWork.DOperationRecord.GetByID(CurrentRecordID);
            OperationImage oImage = new OperationImage();
            oImage.name = imageName;

            oImage.operationID = CurrentRecordID;
            oImage.imagePath = FilePath;
            oImage.doctor = opeationRecord.surgeon;

            unitOfWork.DOperationImage.Insert(oImage);
            unitOfWork.Save();
            var ImageList = unitOfWork.DOperationImage.Get(p => p.operationID == CurrentRecordID).ToList();

            lb.ItemsSource = ImageList;
            lb.ScrollIntoView(lb.Items[lb.Items.Count - 1]);

        }
        /// <summary>
        /// 结束录制功能保存视频
        /// </summary>
        public void UpdateVideo()
        {
            var video = unitOfWork.DOperationVideo.GetByID(CurrentVideoID);
            video.endTime = DateTime.Now;
            video.duration = (DateTime.Now - video.startTime).ToString();
            unitOfWork.DOperationVideo.Update(video);
            unitOfWork.Save();

            var listFile = unitOfWork.DOperationVideo.Get(p => p.operationID == CurrentRecordID);

            DG1.ItemsSource = listFile;
            this.btnRecordStart.IsEnabled = true;
            this.StopRecord.IsEnabled = false;


        }
        private void dataGridEquipment_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (_IsRecordStatus == false)
            {
                //Application.Current.Shutdown();
                System.Environment.Exit(0);
            }
            else
            {

                MessageTip msg = new MessageTip("停止当前录制以后才可以退出！");
                msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg.Show();

            }
        }

        public void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            CurrentFolderPath = XmlHelper.GetValue("Selectpath");

            if (!Directory.Exists(CurrentFolderPath))//判断是否存在
            {
                MessageTip msgpaint = new MessageTip("必须输入病人姓名和医生姓名！");
                msgpaint.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msgpaint.Show();
                return;
            }
          


            if (string.IsNullOrEmpty(this.patintName_Com.Text.ToString()) || string.IsNullOrEmpty(this.opeDoctor_Com.Text.ToString()))
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

            //设置磁盘信息
            CurrentFolderPath = XmlHelper.GetValue("Selectpath");
            if (string.IsNullOrEmpty(XmlHelper.GetValue("Selectpath")))
            {
                MessageTip msg1 = new MessageTip("请在设置中设置文件存储地址！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
                return;
            }
            else
            {
                string ciPan = CurrentFolderPath.Substring(0, 1);

                string Free = GetHardDiskSpace(ciPan).ToString();
                string Total = GetTotalDiskSpace(ciPan).ToString();

                string CiMessage = "磁盘信息:总容量" + Total + "G/可用容量" + Free + "G";
                this.CiPan_Label.Content = CiMessage;

            }
            string username = this.patintName_Com.Text;
            // 查询当天数据
            string time = DateTime.Now.ToShortDateString();


            DateTime time1 = Convert.ToDateTime(time + " 0:00:00");  // 数字前 记得 加空格


            DateTime time2 = Convert.ToDateTime(time + " 23:59:59");
            var painetUser = unitOfWork.DPatient.Get(p => p.username == username && p.createtime > time1 && p.createtime < time2).FirstOrDefault();
            if (painetUser == null)
            {
                var paineRecortUser = unitOfWork.DOperationRecord.GetByID(CurrentRecordID);
                if (paineRecortUser == null)
                {
                    AddpaintUser();
                }
                else
                {
                    CurrentID =Convert.ToInt32( paineRecortUser.PatintUserID);
                }

            }
            else {
                CurrentID = painetUser.ID;
            }
            var currentPaint = unitOfWork.DPatient.GetByID(CurrentID);
            // 创建文件夹
            CurrentFolderPath = CurrentFolderPath + "\\" + currentPaint.surgeon + "\\" + currentPaint.username + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
           




            var pRecortUser = unitOfWork.DOperationRecord.GetByID(CurrentRecordID);
            if (pRecortUser == null)
            {
                if (false == System.IO.Directory.Exists(CurrentFolderPath))
                {
                    //创建pic文件夹
                    System.IO.Directory.CreateDirectory(CurrentFolderPath);
                }
                //添加手术记录
                AddRecord(CurrentRecordID, CurrentFolderPath);

            }
            else
            {
                if (!string.IsNullOrEmpty(pRecortUser.des))
                {
                    CurrentFolderPath = pRecortUser.des;
                }
                else {
                    if (false == System.IO.Directory.Exists(CurrentFolderPath))
                    {
                        //创建pic文件夹
                        System.IO.Directory.CreateDirectory(CurrentFolderPath);
                    }
                }

            }
            oRecord = unitOfWork.DOperationRecord.GetByID(CurrentRecordID);

   

            string textString= string.Format("病人姓名：{0},病人性别：{1},病人年龄：{2},住院号：{3},手术名称：{4},手术医生：{5},手术部位：{6}", this.patintName_Com.Text, this.painetSex_Com.Text, this.painetAge_Txt.Text, this.painetNum_Txt.Text, this.operName_Com.Text, this.operPart_Com.Text, this.opeDoctor_Com.Text);



            string path = CurrentFolderPath + "\\"+ this.patintName_Com.Text + ".txt";

            if (!File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

                StreamWriter sw = new StreamWriter(fs);
                sw.Write(textString);
                sw.Flush();
                sw.Close();
            }
            else
            {
                FileStream fs = new FileStream(path, FileMode.Append);
                //文本写入
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(textString);
                sw.Flush();
                sw.Close();
            }

            this.btnRecordStart.IsEnabled = true;//开始录像
            this.CameraShow.IsEnabled = true;//拍照
            //this.ZhiBoBtn.IsEnabled = true;//直播
            this.StopRecord.IsEnabled = true;//停止录制
            this.CameraBtn.IsEnabled = true;//拍照ICON
            //this.zHiBtn.IsEnabled = true;//直播ICON
            this.stopBtn.IsEnabled = true;//停止录制ICON

            this.btnSave.IsEnabled = false;

            this.Nosiganlr.Visibility = Visibility.Hidden;
            this.FullStack.Visibility = Visibility.Visible;


            //MessageTip msg = new MessageTip("保存成功！");
            //msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //msg.Show();

            // System.Windows.MessageBox.Show("设置成功！");


        }

        /// <summary>
        /// 新增病人资料
        /// </summary>
        private void AddpaintUser()
        {
            Patient pInt = new Patient();
            pInt.username = this.patintName_Com.Text;
            pInt.hospNumber = this.painetNum_Txt.Text;
            if (!string.IsNullOrEmpty(this.painetAge_Txt.Text))
            {
                pInt.age = Convert.ToInt32(this.painetAge_Txt.Text.ToString());
            }
            else
            {
                pInt.age = 0;
            }

            pInt.sex = this.painetSex_Com.Text;
            pInt.operaName = this.operName_Com.Text.ToString();
            pInt.operativesite = this.operPart_Com.Text.ToString();
            pInt.surgeon = this.opeDoctor_Com.Text.ToString();
            pInt.createtime = DateTime.Now;
            pInt.operationDate = DateTime.Now;
            pInt.status = 0;
            pInt.des = "1";
            unitOfWork.DPatient.Insert(pInt);
            unitOfWork.Save();

            CurrentID = pInt.ID;
        }

        #endregion

        /// <summary>
        /// 旋转180
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TurnBtn_Click(object sender, RoutedEventArgs e)
        {
            EXPORTS.QCAP_SET_VIDEO_MIRROR(m_hCapDev, 1,0);
            if (_IsFanzhuan)
            {
                //EXPORTS.QCAP_SET_VIDEO_MIRROR(m_hCapDev, 1, 1);
              
                //EXPORTS.QCAP_SET_DEVICE_CUSTOM_PROPERTY(m_hCapDev, 245, 0);
                //EXPORTS.QCAP_SET_DEVICE_CUSTOM_PROPERTY(m_hCapDev, 244, 0);
                _IsFanzhuan = false;
            }
            else
            {
                // EXPORTS.QCAP_SET_VIDEO_MIRROR(m_hCapDev, 0, 0);
                //EXPORTS.QCAP_SET_DEVICE_CUSTOM_PROPERTY(m_hCapDev, 245, 1);
                //EXPORTS.QCAP_SET_DEVICE_CUSTOM_PROPERTY(m_hCapDev, 244, 1);

                _IsFanzhuan = true;
            }
            //if (QCAP_SET_DEVICE_CUSTOM_PROPERTY == 0)
            //{
            //    fmMainWin->Memo_Message->Lines->Add("无法调用翻转功能");
            //    return QCAP_RT_FAIL;
            //}
            //if (fmMainWin->m_nShareRecordState > 0x00000000)
            //    if (fmMainWin->m_iVertical)
            //    {
            //        QCAP_SET_DEVICE_CUSTOM_PROPERTY(fmMainWin->m_hVideoDevice, 244, 1);  // 垂直翻转
            //        OutputDebugString("开始垂直翻转");
            //    }
            //    else
            //    {
            //        QCAP_SET_DEVICE_CUSTOM_PROPERTY(fmMainWin->m_hVideoDevice, 244, 0);  // 垂直翻转
            //        OutputDebugString("停止垂直翻转");
            //    }
            //if (fmMainWin->m_iHorizontal)
            //{
            //    OutputDebugString("开始水平翻转");
            //     // 水平翻转
            //}
            //else
            //{
            //    OutputDebugString("停止水平翻转");
            //    QCAP_SET_DEVICE_CUSTOM_PROPERTY(fmMainWin->m_hVideoDevice, 245, 0);  // 水平翻转
            //}

            RotateTransform rotateTransform = new RotateTransform(180);   //其中180是旋转180度
                                                                          // transformGroup.Children.Add(rotateTransform);
            winForm.RenderTransform = rotateTransform;
        }
        /// <summary>
        /// 拍照图标功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IconCamareBtn_Click(object sender, RoutedEventArgs e)
        {
            oRecord = unitOfWork.DOperationRecord.GetByID(CurrentRecordID);
            var currentPaint = unitOfWork.DPatient.GetByID(CurrentID);
            string subPath = CurrentFolderPath + "\\" + currentPaint.surgeon + "\\" + currentPaint.username + "\\Iamge\\";
            if (false == System.IO.Directory.Exists(subPath))
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory(subPath);
            }
            string vName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".JPG";//
            string fileName = subPath + vName;
            string imagePath = fileName;
            string m_strCurrentDir = Directory.GetCurrentDirectory();
            //m_hCapDev = _form1.m_hCapDev;

            if (m_hCapDev != 0)
            {

                EXPORTS.QCAP_SNAPSHOT_JPG(m_hCapDev, ref fileName, 80);

                InsertImage(vName, imagePath);

            }
        }
        /// <summary>
        /// 开始直播图标按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;

            //  btn.Template = this.Resources["StartRecord"] as ControlTemplate;
            // _IsRecordStatus = true;

            this.btnSave.IsEnabled = false;//保存资料
                                           //this.btnNew.IsEnabled = false;//新手术
                                           //this.ExitButton.IsEnabled = false;//直播


            this.playBtn.IsEnabled = false;
            _IsRecordStatus = true;
            rs = new RecordStatus(CurrentRecordID);
            rs.Left = 340;
            rs.Top = 60;
            rs.Show();

            // m_hCapDev = _form1.m_hCapDev;


            this.StopRecord.IsEnabled = true;

            this.btnRecordStart.Content = "暂停录像";
            //m_btnRecordStop1.Enabled = true;
            oRecord = unitOfWork.DOperationRecord.GetByID(CurrentRecordID);
            var currentPaint = unitOfWork.DPatient.GetByID(CurrentID);
            string subPath = CurrentFolderPath + "\\" + currentPaint.surgeon + "\\" + currentPaint.username + "\\Video\\";
            if (false == System.IO.Directory.Exists(subPath))
            {
                //创建pic文件夹
                System.IO.Directory.CreateDirectory(subPath);
            }
            string vName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".mp4";
            string fileName = subPath + vName;

            // m_bSupportGPU1 = m_checkGPU1.Checked;

            if (m_hCapDev != 0)
            {
                EXPORTS.QCAP_SET_AUDIO_RECORD_PROPERTY(m_hCapDev, 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_AAC);

                if (!string.IsNullOrEmpty(_Malv))
                {
                    int maLv = Convert.ToInt32(_Malv);
                    EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev, 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, (uint)maLv * 1024 * 1024, 15, 0, 0, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_OFF);

                }

                string str_avi_name1 = fileName;

                string pszNULL = null;

                EXPORTS.QCAP_START_RECORD(m_hCapDev, 0, ref str_avi_name1, (uint)EXPORTS.RecordFlagEnum.QCAP_RECORD_FLAG_FULL, 0.0, 0.0, 0.0, 0, ref pszNULL);

                InsertVideo(vName, fileName);
                //m_bIsRecord1 = true;
            }



        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            Seting st = new Seting();
            st.Left = 360;
            st.Top = 80;
            st.Show();

        }

        public static long GetHardDiskSpace(string str_HardDiskName)
        {
            long totalSize = 0;
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalFreeSpace / (1024 * 1024 * 1024);

                }
            }

            return totalSize;
        }
        public static long GetTotalDiskSpace(string str_HardDiskName)
        {
            long totalSize = 0;
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize / (1024 * 1024 * 1024);

                }
            }

            return totalSize;
        }

        private void ZHiBtn_Click(object sender, RoutedEventArgs e)
        {
            Seting st = new Seting();
            st.Left = 360;
            st.Top = 80;
            st.Show();
            // System.Windows.MessageBox.Show("直播功能暂未开放");
        }

        private void ZhiBoBtn_Click(object sender, RoutedEventArgs e)
        {
            // System.Windows.MessageBox.Show("直播功能暂未开放");
            MessageTip msg1 = new MessageTip("直播功能暂未开放！");
            msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            msg1.Show();
        }
        /// <summary>
        /// 停止录像ICON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            this.btnNew.IsEnabled = true;//新手术
            this.ExitButton.IsEnabled = true;//直播
            this.btnSave.IsEnabled = false;//保存资料

            //m_hCapDev = _form1.m_hCapDev;
            _IsRecordStatus = false;
            rs.Close();
            btnRecordStart.Content = "开始录像";

            //m_btnRecordStop1.Enabled = false;

            if (m_hCapDev != 0)
            {
                EXPORTS.QCAP_STOP_RECORD(m_hCapDev, 0);

                UpdateVideo();
                //m_bIsRecord1 = false;
                //System.Windows.MessageBox.Show("保存文件成功！");

                MessageTip msg1 = new MessageTip("保存文件成功！");
                msg1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                msg1.Show();
            }
        }

        private void QuanBtn_Click(object sender, RoutedEventArgs e)
        {

            if (IsFullScreen == 0)
            {

                this.FirstColumn.Visibility = Visibility.Hidden;
                this.LeftColumn.Visibility = Visibility.Hidden;
                this.Fist_ColumnDefinition.Width = GridLength.Auto;
                this.ControlPanel.Visibility = Visibility.Hidden;
                this.ImageListPanel.Visibility = Visibility.Hidden;
                this.FootPanel.Visibility = Visibility.Hidden;
                this.VideoColumn.Width = 1500;
                this.winForm.Width = 1500;
                this.winForm.Height = 1005;
                winForm.Margin = new Thickness(-1, -45, 0, 0);
                IsFullScreen = 1;
                //this.QuanBtn.Content = "退出全屏";
                //_form2 = new Form1(1);
                //_form2.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                ////_form2.StartPosition = FormStartPosition.CenterScreen;
                //_form2.Show();

                if (_IsRecordStatus)
                {
                    if (rs != null && rt != null)
                    {
                        rs.Close();
                        rt.Close();
                    }


                    rs = new RecordStatus(CurrentRecordID);
                    //double sx = Convert.ToDouble(XmlHelper.GetValue("StatusX"));
                    //double sy = Convert.ToDouble(XmlHelper.GetValue("StatusY"));
                    //if (sx > 0 && sy > 0)
                    //{
                    //    rs.Left = sx;
                    //    rs.Top = sy;
                    //}
                    //else
                    //{

                    //}

                    rs.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 240;
                    rs.Top = 30;

                    rs.ShowInTaskbar = false;
                    rs.Topmost = true;
                    rs.Show();

                    //_RecordTime = DateTime.Now;

                    TimeSpan tp = DateTime.Now - _RecordTime;
                    if (_IsPauseStatus == false)
                    {
                        tp = CurrentDate - _RecordTime;


                        rt = new RecordTime(tp);
                        //double x = Convert.ToDouble(XmlHelper.GetValue("TimeX"));
                        //double y = Convert.ToDouble(XmlHelper.GetValue("TimeY"));
                        //if (x > 0 && y > 0)
                        //{
                        //    rt.Left = x;
                        //    rt.Top = y;
                        //}
                        //else
                        //{

                        //}
                        rt.Left = 60;
                        rt.Top = 30;

                        rt.ShowInTaskbar = false;
                        rt.Topmost = true;
                        rt.Show();

                        rt._state = RecordTime.State.Pause;
                    }
                    else
                    {
                        rt = new RecordTime(tp);


                        rt.Left = 60;
                        rt.Top = 30;

                        rt.ShowInTaskbar = false;
                        rt.Topmost = true;
                        rt.Show();
                    }





                }

                cn = new Control();
                cn.WindowStartupLocation = WindowStartupLocation.Manual;
                cn.Left = System.Windows.SystemParameters.PrimaryScreenWidth * 20 / 100;
                cn.Top = System.Windows.SystemParameters.PrimaryScreenHeight - 90;
                cn.Width = System.Windows.SystemParameters.PrimaryScreenWidth * 60 / 100;
                cn.Topmost = true;
                cn.Show();

            }
            else
            {

                //this.QuanBtn.Content = "全屏";
                IsFullScreen = 0;
                //_form2.Close();
            }

        }

        public void ExistFullScreen()
        {
            IsFullScreen = 0;
            //if (rs != null && rt != null)
            //{
            //    rs.Close();
            //    rt.Close();

            //}
            if (_IsRecordStatus)
            {

                rt.Left = System.Windows.SystemParameters.PrimaryScreenWidth * 20 / 100;
                rt.Top = 60;

                rs.Left = System.Windows.SystemParameters.PrimaryScreenWidth * 86 / 100;
                rs.Top = 60;


            }
            //Width="1211" Height="690"
            this.FirstColumn.Visibility = Visibility.Visible;
            this.LeftColumn.Visibility = Visibility.Visible;
            this.ControlPanel.Visibility = Visibility.Visible;
            this.ImageListPanel.Visibility = Visibility.Visible;
            //this.Fist_ColumnDefinition.Width = GridLength.Auto;
            this.FootPanel.Visibility = Visibility.Visible;
            this.winForm.Margin = new Thickness(5.2, 0, 4.6, 0);
            this.winForm.Width = 1211;
            this.winForm.Height = 690;
            this.VideoColumn.Width = 1222;
        }

        private void WinForm_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        public bool HwInitialize()
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            // CREATE CAPTURE DEVICE
            //
            string str_chip_name = m_strChipName;



            EXPORTS.QCAP_CREATE(ref str_chip_name, 0, (uint)m_cChannelControl_LIVE.Handle.ToInt32(), ref m_hCapDev, 1, 1);

            if (m_hCapDev == 0)
            {
                return false;
            }
            else
            {
                XmlHelper.SetValue("m_hCapDev", m_hCapDev.ToString());
            }

            LoadMainVideo();

            //EXPORTS.QCAP_CREATE_CLONE(m_hCapDev, (uint)CloneChannelPanel.Handle.ToInt32(), ref m_hCloneCapDev);

            // REGISTER FORMAT CHANGED CALLBACK FUNCTION
            // 
            m_pFormatChangedCB = new EXPORTS.PF_FORMAT_CHANGED_CALLBACK(on_process_format_changed);

            EXPORTS.QCAP_REGISTER_FORMAT_CHANGED_CALLBACK(m_hCapDev, m_pFormatChangedCB, (uint)hwnd);

            // REGISTER PREVIEW VIDEO CALLBACK FUNCTION
            // 
            m_pPreviewVideoCB = new EXPORTS.PF_VIDEO_PREVIEW_CALLBACK(on_process_preview_video_buffer);

            EXPORTS.QCAP_REGISTER_VIDEO_PREVIEW_CALLBACK(m_hCapDev, m_pPreviewVideoCB, (uint)hwnd);

            // REGISTER PREVIEW AUDIO CALLBACK FUNCTION
            //
            m_pPreviewAudioCB = new EXPORTS.PF_AUDIO_PREVIEW_CALLBACK(on_process_preview_audio_buffer);

            EXPORTS.QCAP_REGISTER_AUDIO_PREVIEW_CALLBACK(m_hCapDev, m_pPreviewAudioCB, (uint)hwnd);

            // REGISTER NO SIGNAL DETECTED CALLBACK FUNCTION
            //
            m_pNoSignalDetectedCB = new EXPORTS.PF_NO_SIGNAL_DETECTED_CALLBACK(on_process_no_signal_detected);

            EXPORTS.QCAP_REGISTER_NO_SIGNAL_DETECTED_CALLBACK(m_hCapDev, m_pNoSignalDetectedCB, (uint)hwnd);

            // REGISTER SIGNAL REMOVED CALLBACK FUNCTION
            //
            m_pSignalRemovedCB = new EXPORTS.PF_SIGNAL_REMOVED_CALLBACK(on_process_signal_removed);

            EXPORTS.QCAP_REGISTER_SIGNAL_REMOVED_CALLBACK(m_hCapDev, m_pSignalRemovedCB, (uint)hwnd);

            // 
            //
            EXPORTS.QCAP_SET_VIDEO_DEINTERLACE(m_hCapDev, 0);

            EXPORTS.QCAP_RUN(m_hCapDev);
            EXPORTS.QCAP_RUN_EX(m_hCapDev, 1);
            //timerCheckSignal.Enabled = true;

            return true;
        }

        private void HighSet_Click(object sender, RoutedEventArgs e)
        {
            ShousuManage lg = new ShousuManage();
            lg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            lg.Show();
        }

        public void OnTimer(object sender, EventArgs e)
        {
            if (m_bNoSignal)
            {
                this.Nosiganlr.Visibility = Visibility.Visible;
                this.FullStack.Visibility = Visibility.Hidden;
                m_cChannelControl_LIVE.Visible = false;

                m_cSetupControl.m_strFormatChangedOutput = " INFO :  . . .";
                this.SHiPin_Label.Content = "视频信息：";
                this.btnRecordStart.IsEnabled = false;//开始录像
                this.CameraShow.IsEnabled = false;//拍照
                //this.ZhiBoBtn.IsEnabled = false;//直播
                this.StopRecord.IsEnabled = false;//停止录制
                this.QuanBtn.IsEnabled = false;//全屏幕
            }
            else
            {

                m_cChannelControl_LIVE.Visible = true;
                this.FullStack.Visibility = Visibility.Visible;
                this.btnRecordStart.IsEnabled = true;//开始录像
                this.CameraShow.IsEnabled = true;//拍照
                //this.ZhiBoBtn.IsEnabled = true;//直播
                this.StopRecord.IsEnabled = true;//停止录制
                this.QuanBtn.IsEnabled = true;//全屏幕

                m_cSetupControl.m_strFormatChangedOutput = m_strFormatChangedOutput;
                this.SHiPin_Label.Content = "视频信息：" + m_cSetupControl.m_strFormatChangedOutput;
            }
            


            m_cSetupControl.m_bNoSignal = m_bNoSignal;
        }
        /// <summary>
        /// 检查是否有设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnCheckEquip(object sender, EventArgs e)
        {
            List<SimpleModel> cameraList = VideoHelper.GetAllCameras();
            if (cameraList != null && cameraList.Count > 0)
            {
                foreach (var cam in cameraList)
                {
                    if (cam.Value.Contains("CY3014 USB"))
                    {
                        m_bNoSignal = false;
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HwInitialize();


            var t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, 1);
            t.Tick += OnTimer;
            t.IsEnabled = true;
            t.Start();


            var check = new DispatcherTimer();
            check.Interval = new TimeSpan(0, 0, 0, 3);
            check.Tick += OnCheckEquip;
            check.IsEnabled = true;
            check.Start();

        }

        private void PainetAge_Txt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);
        }
        /// <summary>
        /// 预览图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Controls.Image Tb = (System.Windows.Controls.Image)sender;
            int VID = Convert.ToInt32(Tb.Tag);

            ShowImage md = new ShowImage(VID);
            md.ShowDialog();
        }

        private void DoctorSet_Click(object sender, RoutedEventArgs e)
        {
            DoctorManage lg = new DoctorManage();
            lg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            lg.Show();
        }

        private void PartSet_Click(object sender, RoutedEventArgs e)
        {
            BuWeiManage lg = new BuWeiManage();
            lg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            lg.Show();
        }
        public bool Islegal()
        {
            Regex regExp = new Regex("[~!@#$%^&*()=+[\\]{}''\";:/?.,><`|！·￥…—（）\\-、；：。，》《]");
            return regExp.IsMatch(patintName_Com.Text.Trim());
        }

        private void PatintName_Com_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            SetPainetUser(Convert.ToInt32(this.patintName_Com.SelectedValue));
        }

        public void ChangeVideoBtn_Click(object sender, RoutedEventArgs e)
        {
          

            string VideoInputStr = "";
            string DefultVideo = XmlHelper.GetValue("CurrentVideoInput");
            if (DefultVideo == "0")
            {
                XmlHelper.SetValue("CurrentVideoInput", "1");
                VideoInputStr = XmlHelper.GetValue("VideoSecondInputStr");

                this.ChangeVideoBtn2.Template = this.FindResource("VideoSecondBtn") as ControlTemplate;
                this.ChangeVideoBtn1.Template = this.FindResource("VideoSecondBtn") as ControlTemplate;

                //MessageTip msgMain = new MessageTip("当前切换为副视频源！")
                //{
                //    WindowStartupLocation = WindowStartupLocation.CenterScreen
                //};
                //msgMain.Show();

                iceNumber = 1;

                if (IsRecording == 1)
                {
                    EXPORTS.QCAP_PAUSE_RECORD(m_hCapDev, 0);
                    EXPORTS.QCAP_RESUME_RECORD(m_hCapDev, 1);
                    
                }

            }
            else
            {
                XmlHelper.SetValue("CurrentVideoInput", "0");

                VideoInputStr = XmlHelper.GetValue("VideoInputStr");

                this.ChangeVideoBtn2.Template = this.FindResource("VideoBtn") as ControlTemplate;
                this.ChangeVideoBtn1.Template = this.FindResource("VideoBtn") as ControlTemplate;
                iceNumber = 0;

                if (IsRecording == 1)
                {
                    EXPORTS.QCAP_PAUSE_RECORD(m_hCapDev, 1);
                    EXPORTS.QCAP_RESUME_RECORD(m_hCapDev, 0);

                }

                //MessageTip msgMain = new MessageTip("当前切换为主视频源！")
                //{
                //    WindowStartupLocation = WindowStartupLocation.CenterScreen
                //};
                //msgMain.Show();
            }

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

        public void LoadMainVideo()
        {
            string VideoInputStr = XmlHelper.GetValue("VideoInputStr");

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

        private void PainetAge_Txt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BingLi_Click(object sender, RoutedEventArgs e)
        {
            ShouYeOperation op = new ShouYeOperation
            {
                Left = 360,
                Top = 80
            };
            op.ShowDialog();
        }

        private void Message_Tip_Click(object sender, RoutedEventArgs e)
        {
            CompanyTip msgMain = new CompanyTip()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            msgMain.Show();
        }
    }




    public class Person
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

    }

    public class FileModel
    {
        public int ID { get; set; }
        public string FileName { get; set; }
    }
}
