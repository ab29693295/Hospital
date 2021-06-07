namespace Hospital
{
    partial class MyVideoInputDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RadioButtonSDI = new System.Windows.Forms.RadioButton();
            this.RadioButtonRGB = new System.Windows.Forms.RadioButton();
            this.RadioButtonYCBCR = new System.Windows.Forms.RadioButton();
            this.RadioButtonInputDVI = new System.Windows.Forms.RadioButton();
            this.RadioButtonInputHDMI = new System.Windows.Forms.RadioButton();
            this.RadioButtonCOMPOSITE = new System.Windows.Forms.RadioButton();
            this.RadioButtonSVIDEO = new System.Windows.Forms.RadioButton();
            this.RadioButtonAUTO = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RadioButtonSDI
            // 
            this.RadioButtonSDI.AutoSize = true;
            this.RadioButtonSDI.Location = new System.Drawing.Point(12, 119);
            this.RadioButtonSDI.Name = "RadioButtonSDI";
            this.RadioButtonSDI.Size = new System.Drawing.Size(41, 16);
            this.RadioButtonSDI.TabIndex = 80;
            this.RadioButtonSDI.Text = "SDI";
            this.RadioButtonSDI.UseVisualStyleBackColor = true;
            this.RadioButtonSDI.Click += new System.EventHandler(this.RadioButtonSDI_Click);
            // 
            // RadioButtonRGB
            // 
            this.RadioButtonRGB.AutoSize = true;
            this.RadioButtonRGB.Location = new System.Drawing.Point(12, 93);
            this.RadioButtonRGB.Name = "RadioButtonRGB";
            this.RadioButtonRGB.Size = new System.Drawing.Size(96, 16);
            this.RadioButtonRGB.TabIndex = 79;
            this.RadioButtonRGB.Text = "DVI-A ( RGB )";
            this.RadioButtonRGB.UseVisualStyleBackColor = true;
            this.RadioButtonRGB.Click += new System.EventHandler(this.RadioButtonRGB_Click);
            // 
            // RadioButtonYCBCR
            // 
            this.RadioButtonYCBCR.AutoSize = true;
            this.RadioButtonYCBCR.Location = new System.Drawing.Point(12, 67);
            this.RadioButtonYCBCR.Name = "RadioButtonYCBCR";
            this.RadioButtonYCBCR.Size = new System.Drawing.Size(156, 16);
            this.RadioButtonYCBCR.TabIndex = 78;
            this.RadioButtonYCBCR.Text = "COMPONENTS ( YCBCR )";
            this.RadioButtonYCBCR.UseVisualStyleBackColor = true;
            this.RadioButtonYCBCR.Click += new System.EventHandler(this.RadioButtonYCBCR_Click);
            // 
            // RadioButtonInputDVI
            // 
            this.RadioButtonInputDVI.AutoSize = true;
            this.RadioButtonInputDVI.Location = new System.Drawing.Point(12, 41);
            this.RadioButtonInputDVI.Name = "RadioButtonInputDVI";
            this.RadioButtonInputDVI.Size = new System.Drawing.Size(55, 16);
            this.RadioButtonInputDVI.TabIndex = 77;
            this.RadioButtonInputDVI.Text = "DVI-D";
            this.RadioButtonInputDVI.UseVisualStyleBackColor = true;
            this.RadioButtonInputDVI.Click += new System.EventHandler(this.RadioButtonInputDVI_Click);
            // 
            // RadioButtonInputHDMI
            // 
            this.RadioButtonInputHDMI.AutoSize = true;
            this.RadioButtonInputHDMI.Location = new System.Drawing.Point(12, 15);
            this.RadioButtonInputHDMI.Name = "RadioButtonInputHDMI";
            this.RadioButtonInputHDMI.Size = new System.Drawing.Size(53, 16);
            this.RadioButtonInputHDMI.TabIndex = 76;
            this.RadioButtonInputHDMI.Text = "HDMI";
            this.RadioButtonInputHDMI.UseVisualStyleBackColor = true;
            this.RadioButtonInputHDMI.Click += new System.EventHandler(this.RadioButtonInputHDMI_Click);
            // 
            // RadioButtonCOMPOSITE
            // 
            this.RadioButtonCOMPOSITE.AutoSize = true;
            this.RadioButtonCOMPOSITE.Location = new System.Drawing.Point(12, 145);
            this.RadioButtonCOMPOSITE.Name = "RadioButtonCOMPOSITE";
            this.RadioButtonCOMPOSITE.Size = new System.Drawing.Size(87, 16);
            this.RadioButtonCOMPOSITE.TabIndex = 82;
            this.RadioButtonCOMPOSITE.Text = "COMPOSITE";
            this.RadioButtonCOMPOSITE.UseVisualStyleBackColor = true;
            this.RadioButtonCOMPOSITE.Click += new System.EventHandler(this.RadioButtonCOMPOSITE_Click);
            // 
            // RadioButtonSVIDEO
            // 
            this.RadioButtonSVIDEO.AutoSize = true;
            this.RadioButtonSVIDEO.Location = new System.Drawing.Point(12, 173);
            this.RadioButtonSVIDEO.Name = "RadioButtonSVIDEO";
            this.RadioButtonSVIDEO.Size = new System.Drawing.Size(68, 16);
            this.RadioButtonSVIDEO.TabIndex = 83;
            this.RadioButtonSVIDEO.Text = "S-VIDEO";
            this.RadioButtonSVIDEO.UseVisualStyleBackColor = true;
            this.RadioButtonSVIDEO.Click += new System.EventHandler(this.RadioButtonSVIDEO_Click);
            // 
            // RadioButtonAUTO
            // 
            this.RadioButtonAUTO.AutoSize = true;
            this.RadioButtonAUTO.Location = new System.Drawing.Point(12, 199);
            this.RadioButtonAUTO.Name = "RadioButtonAUTO";
            this.RadioButtonAUTO.Size = new System.Drawing.Size(54, 16);
            this.RadioButtonAUTO.TabIndex = 84;
            this.RadioButtonAUTO.Text = "AUTO";
            this.RadioButtonAUTO.UseVisualStyleBackColor = true;
            this.RadioButtonAUTO.Click += new System.EventHandler(this.RadioButtonAUTO_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(201, 196);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 85;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // MyVideoInputDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 228);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.RadioButtonAUTO);
            this.Controls.Add(this.RadioButtonSVIDEO);
            this.Controls.Add(this.RadioButtonCOMPOSITE);
            this.Controls.Add(this.RadioButtonSDI);
            this.Controls.Add(this.RadioButtonRGB);
            this.Controls.Add(this.RadioButtonYCBCR);
            this.Controls.Add(this.RadioButtonInputDVI);
            this.Controls.Add(this.RadioButtonInputHDMI);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyVideoInputDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SET VIDEO INPUT";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MyVideoInputDlg_Load);
            this.Shown += new System.EventHandler(this.MyVideoInputDlg_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MyVideoInputDlg_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.RadioButton RadioButtonSDI;
        internal System.Windows.Forms.RadioButton RadioButtonRGB;
        internal System.Windows.Forms.RadioButton RadioButtonYCBCR;
        internal System.Windows.Forms.RadioButton RadioButtonInputDVI;
        internal System.Windows.Forms.RadioButton RadioButtonInputHDMI;
        internal System.Windows.Forms.RadioButton RadioButtonCOMPOSITE;
        internal System.Windows.Forms.RadioButton RadioButtonSVIDEO;
        internal System.Windows.Forms.RadioButton RadioButtonAUTO;
        internal System.Windows.Forms.Button buttonOK;

    }
}