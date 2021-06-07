using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using QCAP.NET;

namespace Hospital
{
    public partial class MySetupControl : Form
    {
        public Form1 m_pMainForm;

        public uint m_hCapDev = 0x00000000;                         // STREAM CAPTURE DEVICE

        public string m_strFormatChangedOutput = " INFO :  . . .";

        string m_strCurrentDir;                                                   // CURRENT WORKING DIRECTORY

        public bool m_bNoSignal = true;

        public bool m_bIsRecord1 = false;

        public bool m_bIsRecord2 = false;

        bool m_bSupportGPU1 = false;

        bool m_bSupportGPU2 = false;

        bool m_bAutoDeinterlace = true;

        bool m_bCheckedAVI_1 = false;

        bool m_bCheckedAVI_2 = false;

        bool m_bCheckedMP4_1 = true;

        bool m_bCheckedMP4_2 = true;

        string m_strBmpName;

        string m_strJpgName;

        string m_strAviName1;

        string m_strAviName2;

        public MyVideoInputDlg m_cVideoInputDlg;

        public MyAudioInputDlg m_cAudioInputDlg;

        public MyVideoPropertyDlg m_cVideoPropertytDlg;

        public MyOSDPropertyDlg m_cOSDPropertytDlg;

        public MySetupControl()
        {
            InitializeComponent();
        }        

        private void MySetupControl_Load(object sender, EventArgs e)
        {
            // GET CURRENT DIRECTORY
            //
            m_strCurrentDir = Directory.GetCurrentDirectory();

            m_cVideoInputDlg = new MyVideoInputDlg();

            m_cVideoInputDlg.m_pMainForm = this;

            m_cVideoInputDlg.Hide();

            m_cAudioInputDlg = new MyAudioInputDlg();

            m_cAudioInputDlg.m_pMainForm = this;

            m_cAudioInputDlg.Hide();

            m_cVideoPropertytDlg = new MyVideoPropertyDlg();

            m_cVideoPropertytDlg.m_pMainForm = this;

            m_cVideoPropertytDlg.Hide();

            m_cOSDPropertytDlg = new MyOSDPropertyDlg();

            m_cOSDPropertytDlg.m_pMainForm = this;

            m_cOSDPropertytDlg.Hide();

            m_staticDeviceFormatInformation.Width = this.Width - 30;

            m_strBmpName = m_strCurrentDir + "\\CH01.BMP";

            m_strJpgName = m_strCurrentDir + "\\CH01.JPG";

            m_strAviName1 = m_strCurrentDir + "\\CH01_1.MP4";

            m_strAviName2 = m_strCurrentDir + "\\CH01_2.MP4";

            textBoxSnapshotBMP.Text = m_strBmpName;

            textBoxSnapshotJPG.Text = m_strJpgName;

            textBoxRecordAVI1.Text = m_strAviName1;

            textBoxRecordAVI2.Text = m_strAviName2;

            m_btnRecordStart1.Enabled = true;

            m_btnRecordStop1.Enabled = false;

            m_btnRecordStart2.Enabled = true;

            m_btnRecordStop2.Enabled = false;

            timerCheckSignal.Enabled = true;

            comboDeinterlaceType.SelectedIndex = 0;
        }

        private void MySetupControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void timerShowInfo_Tick(object sender, EventArgs e)
        {
            m_staticDeviceFormatInformation.Text = m_strFormatChangedOutput;
        }

        private void m_btnVideoInput_Click(object sender, EventArgs e)
        {
            m_cVideoInputDlg.m_hCapDev = m_pMainForm.m_hCapDev;

            m_cVideoInputDlg.Show();
        }

        private void m_btnAudioInput_Click(object sender, EventArgs e)
        {
            m_cAudioInputDlg.m_hCapDev = m_pMainForm.m_hCapDev;

            m_cAudioInputDlg.Show();
        }

        private void m_btnSnapshotBMP_Click(object sender, EventArgs e)
        {
            m_hCapDev = m_pMainForm.m_hCapDev;

            if (m_hCapDev != 0)
            {
                string strBmpName = m_strCurrentDir + "\\CH01.BMP";       

                EXPORTS.QCAP_SNAPSHOT_BMP(m_hCapDev, ref strBmpName);
            }
        }

        private void m_btnSnapshotJPG_Click(object sender, EventArgs e)
        {
            m_hCapDev = m_pMainForm.m_hCapDev;

            if (m_hCapDev != 0)
            {
                string strJpgName = m_strCurrentDir + "\\CH01.JPG";

                EXPORTS.QCAP_SNAPSHOT_JPG(m_hCapDev, ref strJpgName, 80);
            }
        }

        private void m_btnVideoQuality_Click(object sender, EventArgs e)
        {
            m_cVideoPropertytDlg.m_hCapDev = m_pMainForm.m_hCapDev;

            m_cVideoPropertytDlg.Show();
        }

        private void m_btnOSDSettings_Click(object sender, EventArgs e)
        {
            m_cOSDPropertytDlg.m_hCapDev = m_pMainForm.m_hCapDev;

            m_cOSDPropertytDlg.Show();
        }

        private void m_btnRecordStart1_Click(object sender, EventArgs e)
        {
            m_hCapDev = m_pMainForm.m_hCapDev;

            m_btnRecordStart1.Enabled = false;

            m_btnRecordStop1.Enabled = true;

            m_bSupportGPU1 = m_checkGPU1.Checked;

            if (m_hCapDev != 0)
            {
                if (m_bCheckedAVI_1 == true)
                {
                    EXPORTS.QCAP_SET_AUDIO_RECORD_PROPERTY(m_hCapDev, 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_PCM);
                }

                if (m_bCheckedMP4_1 == true)
                {
                    EXPORTS.QCAP_SET_AUDIO_RECORD_PROPERTY(m_hCapDev, 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_AAC);
                }

                if ( m_bSupportGPU1 )
                {
                    EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev, 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_INTEL_MEDIA_SDK, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_OFF);

                    string str_avi_name1 = m_strAviName1;

                    string pszNULL = null;

                    EXPORTS.QCAP_START_RECORD(m_hCapDev, 0, ref str_avi_name1, (uint)EXPORTS.RecordFlagEnum.QCAP_RECORD_FLAG_FULL, 0.0, 0.0, 0.0, 0, ref pszNULL);
                }
                else
                {
                    EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev, 0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_OFF);

                    string str_avi_name1 = m_strAviName1;

                    string pszNULL = null;

                    EXPORTS.QCAP_START_RECORD(m_hCapDev, 0, ref str_avi_name1, (uint)EXPORTS.RecordFlagEnum.QCAP_RECORD_FLAG_FULL, 0.0, 0.0, 0.0, 0, ref pszNULL);
                }

                m_bIsRecord1 = true;
            }
        }

        private void m_btnRecordStop1_Click(object sender, EventArgs e)
        {
            m_hCapDev = m_pMainForm.m_hCapDev;

            m_btnRecordStart1.Enabled = true;

            m_btnRecordStop1.Enabled = false;

            if (m_hCapDev != 0)
            {
                EXPORTS.QCAP_STOP_RECORD(m_hCapDev, 0);

                m_bIsRecord1 = false;
            }
        }

        private void m_btnRecordStart2_Click(object sender, EventArgs e)
        {
            m_hCapDev = m_pMainForm.m_hCapDev;

            m_btnRecordStart2.Enabled = false;

            m_btnRecordStop2.Enabled = true;

            m_bSupportGPU2 = m_checkGPU2.Checked;

            if (m_hCapDev != 0)
            {
                if (m_bCheckedAVI_2 == true)
                {
                    EXPORTS.QCAP_SET_AUDIO_RECORD_PROPERTY(m_hCapDev, 1, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_PCM);
                }

                if (m_bCheckedMP4_2 == true)
                {
                    EXPORTS.QCAP_SET_AUDIO_RECORD_PROPERTY(m_hCapDev, 1, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_AAC);
                }

                if (m_bSupportGPU2)
                {
                    EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev, 1, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_INTEL_MEDIA_SDK, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_1_4);

                    string str_avi_name2 = m_strAviName2;

                    string pszNULL = null;

                    EXPORTS.QCAP_START_RECORD(m_hCapDev, 1, ref str_avi_name2, (uint)EXPORTS.RecordFlagEnum.QCAP_RECORD_FLAG_FULL, 0.0, 0.0, 0.0, 0, ref pszNULL);
                }
                else
                {
                    EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev, 1, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12582912, 30, 0, 0, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_1_4);

                    string str_avi_name2 = m_strAviName2;

                    string pszNULL = null;

                    EXPORTS.QCAP_START_RECORD(m_hCapDev, 1, ref str_avi_name2, (uint)EXPORTS.RecordFlagEnum.QCAP_RECORD_FLAG_FULL, 0.0, 0.0, 0.0, 0, ref pszNULL);
                }

                m_bIsRecord2 = true;
            }
        }

        private void m_btnRecordStop2_Click(object sender, EventArgs e)
        {
            m_hCapDev = m_pMainForm.m_hCapDev;

            m_btnRecordStart2.Enabled = true;

            m_btnRecordStop2.Enabled = false;

            if (m_hCapDev != 0)
            {
                EXPORTS.QCAP_STOP_RECORD(m_hCapDev, 1);

                m_bIsRecord2 = false;
            }
        }

        private void m_checkGPU1_Click(object sender, EventArgs e)
        {
            m_btnRecordStop1_Click(sender, e);
        }

        private void m_checkGPU2_Click(object sender, EventArgs e)
        {
            m_btnRecordStop2_Click(sender, e);
        }

        private void m_checkAutoDeinterlace_Click(object sender, EventArgs e)
        {
            m_bAutoDeinterlace = m_checkAutoDeinterlace.Checked;

            m_hCapDev = m_pMainForm.m_hCapDev;

            if (m_hCapDev != 0)
            {
                if (m_bAutoDeinterlace)
                {
                    if (comboDeinterlaceType.SelectedIndex == 0) { EXPORTS.QCAP_SET_VIDEO_DEINTERLACE_TYPE(m_hCapDev, (uint)EXPORTS.SoftwareDeinterlaceTypeEnum.QCAP_SOFTWARE_DEINTERLACE_TYPE_BLENDING); }

                    if (comboDeinterlaceType.SelectedIndex == 1) { EXPORTS.QCAP_SET_VIDEO_DEINTERLACE_TYPE(m_hCapDev, (uint)EXPORTS.SoftwareDeinterlaceTypeEnum.QCAP_SOFTWARE_DEINTERLACE_TYPE_MOTION_ADAPTIVE); }

                    EXPORTS.QCAP_SET_VIDEO_DEINTERLACE(m_hCapDev, 1);
                }
                else
                {
                    EXPORTS.QCAP_SET_VIDEO_DEINTERLACE(m_hCapDev, 0);
                }
            }
        }

        private void m_checkShowCloneVideo_Click(object sender, EventArgs e)
        {
            bool bShowCloneVideo = m_checkShowCloneVideo.Checked;

            if (bShowCloneVideo)
            {
                m_pMainForm.ShowCloneVideo( true );
            }
            else
            {
                m_pMainForm.ShowCloneVideo( false );
            }
        }

        private void timerCheckSignal_Tick(object sender, EventArgs e)
        {
            m_hCapDev = m_pMainForm.m_hCapDev;

            if (m_hCapDev != 0)
            {
                if (m_bIsRecord1 && m_bNoSignal) 
                { 
                    EXPORTS.QCAP_STOP_RECORD(m_hCapDev, 0);

                    m_bIsRecord1 = false;
                }

                if (m_bIsRecord2 && m_bNoSignal)
                {
                    EXPORTS.QCAP_STOP_RECORD(m_hCapDev, 1);

                    m_bIsRecord2 = false;
                }
            }
        }

        private void m_checkAVI_1_Click(object sender, EventArgs e)
        {
            m_checkAVI_1.Checked = true;

            m_checkMP4_1.Checked = false;

            m_bCheckedAVI_1 = true;

            m_bCheckedMP4_1 = false;

            m_strAviName1 = m_strAviName1.Replace(".MP4", ".AVI");

            textBoxRecordAVI1.Text = m_strAviName1;
        }

        private void m_checkMP4_1_Click(object sender, EventArgs e)
        {
            m_checkAVI_1.Checked = false;

            m_checkMP4_1.Checked = true;

            m_bCheckedAVI_1 = false;

            m_bCheckedMP4_1 = true;

            m_strAviName1 = m_strAviName1.Replace(".AVI", ".MP4");

            textBoxRecordAVI1.Text = m_strAviName1;
        }

        private void m_checkAVI_2_Click(object sender, EventArgs e)
        {
            m_checkAVI_2.Checked = true;

            m_checkMP4_2.Checked = false;

            m_bCheckedAVI_2 = true;

            m_bCheckedMP4_2 = false;

            m_strAviName2 = m_strAviName2.Replace(".MP4", ".AVI");

            textBoxRecordAVI2.Text = m_strAviName2;
        }

        private void m_checkMP4_2_Click(object sender, EventArgs e)
        {
            m_checkAVI_2.Checked = false;

            m_checkMP4_2.Checked = true;

            m_bCheckedAVI_2 = false;

            m_bCheckedMP4_2 = true;

            m_strAviName2 = m_strAviName2.Replace(".AVI", ".MP4");

            textBoxRecordAVI2.Text = m_strAviName2;
        }

        private void comboDeinterlaceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_bAutoDeinterlace = m_checkAutoDeinterlace.Checked;

            m_hCapDev = m_pMainForm.m_hCapDev;         

            if (m_hCapDev != 0)
            {
                if (comboDeinterlaceType.SelectedIndex == 0) { EXPORTS.QCAP_SET_VIDEO_DEINTERLACE_TYPE(m_hCapDev, (uint)EXPORTS.SoftwareDeinterlaceTypeEnum.QCAP_SOFTWARE_DEINTERLACE_TYPE_BLENDING); }

                if (comboDeinterlaceType.SelectedIndex == 1) { EXPORTS.QCAP_SET_VIDEO_DEINTERLACE_TYPE(m_hCapDev, (uint)EXPORTS.SoftwareDeinterlaceTypeEnum.QCAP_SOFTWARE_DEINTERLACE_TYPE_MOTION_ADAPTIVE); }

                if (m_bAutoDeinterlace) { EXPORTS.QCAP_SET_VIDEO_DEINTERLACE(m_hCapDev, 1); }
            }
        }

        private void m_checkAVI_1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}