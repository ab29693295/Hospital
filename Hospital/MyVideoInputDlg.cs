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
    public partial class MyVideoInputDlg : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);

        public MySetupControl m_pMainForm;

        public uint m_hCapDev = 0x00000000;                         // STREAM CAPTURE DEVICE

        public MyVideoInputDlg()
        {
            InitializeComponent();
        }

        private void MyVideoInputDlg_Load(object sender, EventArgs e)
        {

        }

        private void MyVideoInputDlg_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void MyVideoInputDlg_Shown(object sender, EventArgs e)
        {
            if (m_hCapDev != 0x00000000)
            {
                uint nInput = (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_SDI;

                EXPORTS.QCAP_GET_VIDEO_INPUT(m_hCapDev, ref nInput);

                if (nInput == (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_COMPOSITE)
                {
                    RadioButtonCOMPOSITE.Checked = true;
                }

                if (nInput == (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_SVIDEO)
                {
                    RadioButtonSVIDEO.Checked = true;
                }

                if (nInput == (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_HDMI)
                {
                    RadioButtonInputHDMI.Checked = true;
                }

                if (nInput == (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_DVI_D)
                {
                    RadioButtonInputDVI.Checked = true;
                }

                if (nInput == (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_COMPONENTS)
                {
                    RadioButtonYCBCR.Checked = true;
                }

                if (nInput == (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_DVI_A)
                {
                    RadioButtonRGB.Checked = true;
                }

                if (nInput == (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_SDI)
                {
                    RadioButtonSDI.Checked = true;
                }

                if (nInput == (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_AUTO)
                {
                    RadioButtonAUTO.Checked = true;
                }               
            }
        }      

        private void RadioButtonInputHDMI_Click(object sender, EventArgs e)
        {
            if (m_hCapDev != 0x00000000)
            {
                EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_HDMI);
            }
        }

        private void RadioButtonInputDVI_Click(object sender, EventArgs e)
        {
            if (m_hCapDev != 0x00000000)
            {
                EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_DVI_D);
            }
        }

        private void RadioButtonYCBCR_Click(object sender, EventArgs e)
        {
            if (m_hCapDev != 0x00000000)
            {
                EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_COMPONENTS);
            }
        }

        private void RadioButtonRGB_Click(object sender, EventArgs e)
        {
            if (m_hCapDev != 0x00000000)
            {
                EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_DVI_A);
            }
        }

        private void RadioButtonSDI_Click(object sender, EventArgs e)
        {
            if (m_hCapDev != 0x00000000)
            {
                EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_SDI);
            }
        }

        private void RadioButtonCOMPOSITE_Click(object sender, EventArgs e)
        {
            if (m_hCapDev != 0x00000000)
            {
                EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_COMPOSITE);
            }
        }

        private void RadioButtonSVIDEO_Click(object sender, EventArgs e)
        {
            if (m_hCapDev != 0x00000000)
            {
                EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_SVIDEO);
            }
        }

        private void RadioButtonAUTO_Click(object sender, EventArgs e)
        {
            if (m_hCapDev != 0x00000000)
            {
                EXPORTS.QCAP_SET_VIDEO_INPUT(m_hCapDev, (uint)EXPORTS.InputVideoSourceEnum.QCAP_INPUT_TYPE_AUTO);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}