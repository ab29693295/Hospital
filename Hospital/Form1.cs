using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using QCAP.NET;

namespace Hospital
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);
        public int LoadType = 0;
        public Form1(int type)
        {
            LoadType = type;
            InitializeComponent();
        }

        public bool m_bIsMaximizedForm = false;

        public bool m_bNoSignal = true;

        // CALLBACK FUNCTION
        //        
        public static EXPORTS.PF_FORMAT_CHANGED_CALLBACK m_pFormatChangedCB;

        public static EXPORTS.PF_NO_SIGNAL_DETECTED_CALLBACK m_pNoSignalDetectedCB;

        public static EXPORTS.PF_SIGNAL_REMOVED_CALLBACK m_pSignalRemovedCB;

        public static EXPORTS.PF_VIDEO_PREVIEW_CALLBACK m_pPreviewVideoCB;

        public static EXPORTS.PF_AUDIO_PREVIEW_CALLBACK m_pPreviewAudioCB;

        public MySetupControl m_cSetupControl = new MySetupControl();

        public MyChannelControl m_cChannelControl_LIVE = new MyChannelControl();

        public int iceNumber = 0;

        // string m_strChipName = "SA7160 PCI";

        string m_strChipName = XmlHelper.GetValue("CaptureStr");    // for UB530 and UB530G

       public string m_strFormatChangedOutput = "";

        // DEVICE PROPERTY
        //
        public uint m_hCapDev = 0x00000000;                         // STREAM CAPTURE DEVICE
      

        public uint m_hCloneCapDev = 0x00000000;                // CLONE STREAM CAPTURE DEVICE

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

            if (bVideoIsInterleaved == 1) { nVH = nVideoHeight / 2; } else { nVH = nVideoHeight; }

            if (bVideoIsInterleaved == 1) { strFrameType = " I "; } else { strFrameType = " P "; }

            m_strFormatChangedOutput = @" INFO : " + nVideoWidth.ToString() + " x " + nVH.ToString() + strFrameType + " @" + dVideoFrameRate.ToString() +

                " FPS , " + nAudioChannels.ToString() + " CH x " + nAudioBitsPerSample.ToString() + " BITS x " + nAudioSampleFrequency.ToString() + " HZ , " +

                " VIDEO INPUT : " + strVideoInput + " , " + " AUDIO INPUT : " + strAudioInput + " \n";

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

        private void Form1_Load(object sender, EventArgs e)
        {
     
            labelNoSignal.Parent = this;

            m_cChannelControl_LIVE = new MyChannelControl();

            m_cChannelControl_LIVE.Parent = this;
           
            m_cChannelControl_LIVE.Left = 0;

            m_cChannelControl_LIVE.Top = 0;
           

            m_cChannelControl_LIVE.Width = this.Width;

            m_cChannelControl_LIVE.Height = this.Height;

            m_cChannelControl_LIVE.Visible = true;

            m_cChannelControl_LIVE.Show();

            CloneChannelPanel.Left = this.Width - 320; 

            CloneChannelPanel.Top = this.Height - 240; 

            CloneChannelPanel.Width = 320;

            CloneChannelPanel.Height = 240;

            HwInitialize();

            
            // USER INTERFACE PROGRAMMING (SETUP CONTROL)
            //

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            HwUnInitialize();
        }

        private void SetupControlClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        public void ShowCloneVideo(bool bShow)
        {
            if (bShow)
            {
                CloneChannelPanel.Visible = true;

                if (m_hCapDev != 0)
                {
                    EXPORTS.QCAP_CREATE_CLONE(m_hCapDev, (uint)CloneChannelPanel.Handle.ToInt32(), ref m_hCloneCapDev);

                    if (m_hCloneCapDev != 0)
                    {
                        EXPORTS.QCAP_RUN(m_hCloneCapDev);
                        EXPORTS.QCAP_RUN_EX(m_hCloneCapDev, 1);
                        EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev, 0);
                    }
                }
            }
            else
            {
                CloneChannelPanel.Visible = false;

                if (m_hCloneCapDev != 0)
                {
                    EXPORTS.QCAP_STOP(m_hCloneCapDev);

                    EXPORTS.QCAP_DESTROY(m_hCloneCapDev);

                    m_hCloneCapDev = 0;
                }
            }
        }

        public void OnLButtonDown_ChannelControl(uint nChannelNumber)
        {
            // MAXIMIZED THE VIDEO WINDOW
            // 
            if (!m_bIsMaximizedForm)
            {
                this.WindowState = FormWindowState.Maximized;

                m_cChannelControl_LIVE.Size = new System.Drawing.Size(this.Width, this.Height);

                m_bIsMaximizedForm = true;

                m_cChannelControl_LIVE.Left = 0;

                m_cChannelControl_LIVE.Top = 0;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
              
                    m_cChannelControl_LIVE.Left = 0;

                    m_cChannelControl_LIVE.Top = 0;
               

                m_cChannelControl_LIVE.Size = new System.Drawing.Size(1222, 680);

                m_bIsMaximizedForm = false;

               
            }
           
            CloneChannelPanel.Left = this.Width - 320; ;

            CloneChannelPanel.Top = this.Height - 240; ;

            CloneChannelPanel.Width = 320;

            CloneChannelPanel.Height = 240;
        }

        public bool HwInitialize()
        {
            // CREATE CAPTURE DEVICE
            //
            string str_chip_name = m_strChipName;

            EXPORTS.QCAP_CREATE(ref str_chip_name, 0, (uint)m_cChannelControl_LIVE.Handle.ToInt32(), ref m_hCapDev);

            if (m_hCapDev == 0)
            {
                return false;
            }
            else
            {
                XmlHelper.SetValue("m_hCapDev", m_hCapDev.ToString());
            }

            EXPORTS.QCAP_CREATE_CLONE(m_hCapDev, (uint)CloneChannelPanel.Handle.ToInt32(), ref m_hCloneCapDev);

            // REGISTER FORMAT CHANGED CALLBACK FUNCTION
            // 
            m_pFormatChangedCB = new EXPORTS.PF_FORMAT_CHANGED_CALLBACK(on_process_format_changed);

            EXPORTS.QCAP_REGISTER_FORMAT_CHANGED_CALLBACK(m_hCapDev, m_pFormatChangedCB, (uint)this.Handle.ToInt32());

            // REGISTER PREVIEW VIDEO CALLBACK FUNCTION
            // 
            m_pPreviewVideoCB = new EXPORTS.PF_VIDEO_PREVIEW_CALLBACK(on_process_preview_video_buffer);

            EXPORTS.QCAP_REGISTER_VIDEO_PREVIEW_CALLBACK(m_hCapDev, m_pPreviewVideoCB, (uint)this.Handle.ToInt32());

            // REGISTER PREVIEW AUDIO CALLBACK FUNCTION
            //
            m_pPreviewAudioCB = new EXPORTS.PF_AUDIO_PREVIEW_CALLBACK(on_process_preview_audio_buffer);

            EXPORTS.QCAP_REGISTER_AUDIO_PREVIEW_CALLBACK(m_hCapDev, m_pPreviewAudioCB, (uint)this.Handle.ToInt32());

            // REGISTER NO SIGNAL DETECTED CALLBACK FUNCTION
            //
            m_pNoSignalDetectedCB = new EXPORTS.PF_NO_SIGNAL_DETECTED_CALLBACK(on_process_no_signal_detected);

            EXPORTS.QCAP_REGISTER_NO_SIGNAL_DETECTED_CALLBACK(m_hCapDev, m_pNoSignalDetectedCB, (uint)this.Handle.ToInt32());

            // REGISTER SIGNAL REMOVED CALLBACK FUNCTION
            //
            m_pSignalRemovedCB = new EXPORTS.PF_SIGNAL_REMOVED_CALLBACK(on_process_signal_removed);

            EXPORTS.QCAP_REGISTER_SIGNAL_REMOVED_CALLBACK(m_hCapDev, m_pSignalRemovedCB, (uint)this.Handle.ToInt32());

            // 
            //
            EXPORTS.QCAP_SET_VIDEO_DEINTERLACE(m_hCapDev, 0);

            EXPORTS.QCAP_RUN(m_hCapDev);
            EXPORTS.QCAP_RUN_EX(m_hCapDev, 1);
            timerCheckSignal.Enabled = true;

            return true;
        }

        public bool HwUnInitialize()
        {
            if (m_hCloneCapDev != 0)
            {
                EXPORTS.QCAP_STOP(m_hCloneCapDev);

                EXPORTS.QCAP_DESTROY(m_hCloneCapDev);
            }

            if (m_hCapDev != 0)
            {
                EXPORTS.QCAP_STOP(m_hCapDev);

                EXPORTS.QCAP_DESTROY(m_hCapDev);
            }

            return true;
        }

        private void timerCheckSignal_Tick(object sender, EventArgs e)
        {
            // DISPLAY FORMAT CHANGED MESSAGE
            //
            if (m_bNoSignal)
            {
                labelNoSignal.Visible = true;

                labelNoSignal.Left = (this.Width / 2) - (labelNoSignal.Width / 2);

                labelNoSignal.Top = (this.Height / 2) - (labelNoSignal.Height / 2);

                m_cChannelControl_LIVE.Visible = false;

                m_cSetupControl.m_strFormatChangedOutput = " INFO :  . . .";
                MainWindow._mainWindow.SHiPin_Label.Content = "视频信息：";
            }
            else
            {
                labelNoSignal.Visible = false;

                m_cChannelControl_LIVE.Visible = true;

                m_cSetupControl.m_strFormatChangedOutput = m_strFormatChangedOutput;
                MainWindow._mainWindow.SHiPin_Label.Content = "视频信息：" + m_cSetupControl.m_strFormatChangedOutput;
            }

            m_cSetupControl.m_bNoSignal = m_bNoSignal;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            OnLButtonDown_ChannelControl(0);
        }
    }
}