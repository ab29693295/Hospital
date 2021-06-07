namespace Hospital
{
    partial class MySetupControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MySetupControl));
            this.m_staticDeviceFormatInformation = new System.Windows.Forms.TextBox();
            this.timerShowInfo = new System.Windows.Forms.Timer(this.components);
            this.m_btnVideoInput = new System.Windows.Forms.Button();
            this.m_btnAudioInput = new System.Windows.Forms.Button();
            this.m_btnSnapshotBMP = new System.Windows.Forms.Button();
            this.m_btnSnapshotJPG = new System.Windows.Forms.Button();
            this.m_btnVideoQuality = new System.Windows.Forms.Button();
            this.textBoxSnapshotBMP = new System.Windows.Forms.TextBox();
            this.textBoxSnapshotJPG = new System.Windows.Forms.TextBox();
            this.m_btnRecordStart1 = new System.Windows.Forms.Button();
            this.m_btnRecordStop1 = new System.Windows.Forms.Button();
            this.textBoxRecordAVI1 = new System.Windows.Forms.TextBox();
            this.textBoxRecordAVI2 = new System.Windows.Forms.TextBox();
            this.m_btnRecordStart2 = new System.Windows.Forms.Button();
            this.m_btnRecordStop2 = new System.Windows.Forms.Button();
            this.m_checkGPU1 = new System.Windows.Forms.CheckBox();
            this.m_checkGPU2 = new System.Windows.Forms.CheckBox();
            this.m_checkAutoDeinterlace = new System.Windows.Forms.CheckBox();
            this.timerCheckSignal = new System.Windows.Forms.Timer(this.components);
            this.m_checkMP4_2 = new System.Windows.Forms.RadioButton();
            this.m_checkAVI_2 = new System.Windows.Forms.RadioButton();
            this.m_checkMP4_1 = new System.Windows.Forms.RadioButton();
            this.m_checkAVI_1 = new System.Windows.Forms.RadioButton();
            this.m_checkShowCloneVideo = new System.Windows.Forms.CheckBox();
            this.m_btnOSDSettings = new System.Windows.Forms.Button();
            this.comboDeinterlaceType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_staticDeviceFormatInformation
            // 
            this.m_staticDeviceFormatInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_staticDeviceFormatInformation.Location = new System.Drawing.Point(7, 7);
            this.m_staticDeviceFormatInformation.Name = "m_staticDeviceFormatInformation";
            this.m_staticDeviceFormatInformation.ReadOnly = true;
            this.m_staticDeviceFormatInformation.Size = new System.Drawing.Size(649, 26);
            this.m_staticDeviceFormatInformation.TabIndex = 0;
            // 
            // timerShowInfo
            // 
            this.timerShowInfo.Enabled = true;
            this.timerShowInfo.Interval = 500;
            this.timerShowInfo.Tick += new System.EventHandler(this.timerShowInfo_Tick);
            // 
            // m_btnVideoInput
            // 
            this.m_btnVideoInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnVideoInput.Location = new System.Drawing.Point(7, 35);
            this.m_btnVideoInput.Name = "m_btnVideoInput";
            this.m_btnVideoInput.Size = new System.Drawing.Size(154, 43);
            this.m_btnVideoInput.TabIndex = 1;
            this.m_btnVideoInput.Text = " VIDEO INPUT +";
            this.m_btnVideoInput.UseVisualStyleBackColor = true;
            this.m_btnVideoInput.Click += new System.EventHandler(this.m_btnVideoInput_Click);
            // 
            // m_btnAudioInput
            // 
            this.m_btnAudioInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnAudioInput.Location = new System.Drawing.Point(167, 35);
            this.m_btnAudioInput.Name = "m_btnAudioInput";
            this.m_btnAudioInput.Size = new System.Drawing.Size(154, 43);
            this.m_btnAudioInput.TabIndex = 2;
            this.m_btnAudioInput.Text = " AUDIO INPUT +";
            this.m_btnAudioInput.UseVisualStyleBackColor = true;
            this.m_btnAudioInput.Click += new System.EventHandler(this.m_btnAudioInput_Click);
            // 
            // m_btnSnapshotBMP
            // 
            this.m_btnSnapshotBMP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSnapshotBMP.Location = new System.Drawing.Point(7, 84);
            this.m_btnSnapshotBMP.Name = "m_btnSnapshotBMP";
            this.m_btnSnapshotBMP.Size = new System.Drawing.Size(154, 43);
            this.m_btnSnapshotBMP.TabIndex = 3;
            this.m_btnSnapshotBMP.Text = "SNAPSHOP BMP";
            this.m_btnSnapshotBMP.UseVisualStyleBackColor = true;
            this.m_btnSnapshotBMP.Click += new System.EventHandler(this.m_btnSnapshotBMP_Click);
            // 
            // m_btnSnapshotJPG
            // 
            this.m_btnSnapshotJPG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnSnapshotJPG.Location = new System.Drawing.Point(167, 84);
            this.m_btnSnapshotJPG.Name = "m_btnSnapshotJPG";
            this.m_btnSnapshotJPG.Size = new System.Drawing.Size(154, 43);
            this.m_btnSnapshotJPG.TabIndex = 4;
            this.m_btnSnapshotJPG.Text = "SNAPSHOP JPG";
            this.m_btnSnapshotJPG.UseVisualStyleBackColor = true;
            this.m_btnSnapshotJPG.Click += new System.EventHandler(this.m_btnSnapshotJPG_Click);
            // 
            // m_btnVideoQuality
            // 
            this.m_btnVideoQuality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnVideoQuality.Location = new System.Drawing.Point(327, 35);
            this.m_btnVideoQuality.Name = "m_btnVideoQuality";
            this.m_btnVideoQuality.Size = new System.Drawing.Size(154, 43);
            this.m_btnVideoQuality.TabIndex = 5;
            this.m_btnVideoQuality.Text = " VIDEO QUALITY +";
            this.m_btnVideoQuality.UseVisualStyleBackColor = true;
            this.m_btnVideoQuality.Click += new System.EventHandler(this.m_btnVideoQuality_Click);
            // 
            // textBoxSnapshotBMP
            // 
            this.textBoxSnapshotBMP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSnapshotBMP.Location = new System.Drawing.Point(327, 82);
            this.textBoxSnapshotBMP.Name = "textBoxSnapshotBMP";
            this.textBoxSnapshotBMP.ReadOnly = true;
            this.textBoxSnapshotBMP.Size = new System.Drawing.Size(606, 26);
            this.textBoxSnapshotBMP.TabIndex = 6;
            // 
            // textBoxSnapshotJPG
            // 
            this.textBoxSnapshotJPG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSnapshotJPG.Location = new System.Drawing.Point(327, 106);
            this.textBoxSnapshotJPG.Name = "textBoxSnapshotJPG";
            this.textBoxSnapshotJPG.ReadOnly = true;
            this.textBoxSnapshotJPG.Size = new System.Drawing.Size(606, 26);
            this.textBoxSnapshotJPG.TabIndex = 7;
            // 
            // m_btnRecordStart1
            // 
            this.m_btnRecordStart1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnRecordStart1.Location = new System.Drawing.Point(7, 132);
            this.m_btnRecordStart1.Name = "m_btnRecordStart1";
            this.m_btnRecordStart1.Size = new System.Drawing.Size(154, 43);
            this.m_btnRecordStart1.TabIndex = 8;
            this.m_btnRecordStart1.Text = " START RECORD 1-1";
            this.m_btnRecordStart1.UseVisualStyleBackColor = true;
            this.m_btnRecordStart1.Click += new System.EventHandler(this.m_btnRecordStart1_Click);
            // 
            // m_btnRecordStop1
            // 
            this.m_btnRecordStop1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnRecordStop1.Location = new System.Drawing.Point(167, 132);
            this.m_btnRecordStop1.Name = "m_btnRecordStop1";
            this.m_btnRecordStop1.Size = new System.Drawing.Size(154, 43);
            this.m_btnRecordStop1.TabIndex = 9;
            this.m_btnRecordStop1.Text = " STOP RECORD 1-1";
            this.m_btnRecordStop1.UseVisualStyleBackColor = true;
            this.m_btnRecordStop1.Click += new System.EventHandler(this.m_btnRecordStop1_Click);
            // 
            // textBoxRecordAVI1
            // 
            this.textBoxRecordAVI1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRecordAVI1.Location = new System.Drawing.Point(327, 153);
            this.textBoxRecordAVI1.Name = "textBoxRecordAVI1";
            this.textBoxRecordAVI1.ReadOnly = true;
            this.textBoxRecordAVI1.Size = new System.Drawing.Size(606, 26);
            this.textBoxRecordAVI1.TabIndex = 10;
            // 
            // textBoxRecordAVI2
            // 
            this.textBoxRecordAVI2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRecordAVI2.Location = new System.Drawing.Point(327, 202);
            this.textBoxRecordAVI2.Name = "textBoxRecordAVI2";
            this.textBoxRecordAVI2.ReadOnly = true;
            this.textBoxRecordAVI2.Size = new System.Drawing.Size(606, 26);
            this.textBoxRecordAVI2.TabIndex = 11;
            // 
            // m_btnRecordStart2
            // 
            this.m_btnRecordStart2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnRecordStart2.Location = new System.Drawing.Point(7, 181);
            this.m_btnRecordStart2.Name = "m_btnRecordStart2";
            this.m_btnRecordStart2.Size = new System.Drawing.Size(154, 43);
            this.m_btnRecordStart2.TabIndex = 12;
            this.m_btnRecordStart2.Text = " START RECORD 1-2";
            this.m_btnRecordStart2.UseVisualStyleBackColor = true;
            this.m_btnRecordStart2.Click += new System.EventHandler(this.m_btnRecordStart2_Click);
            // 
            // m_btnRecordStop2
            // 
            this.m_btnRecordStop2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnRecordStop2.Location = new System.Drawing.Point(167, 181);
            this.m_btnRecordStop2.Name = "m_btnRecordStop2";
            this.m_btnRecordStop2.Size = new System.Drawing.Size(154, 43);
            this.m_btnRecordStop2.TabIndex = 13;
            this.m_btnRecordStop2.Text = " STOP RECORD 1-2";
            this.m_btnRecordStop2.UseVisualStyleBackColor = true;
            this.m_btnRecordStop2.Click += new System.EventHandler(this.m_btnRecordStop2_Click);
            // 
            // m_checkGPU1
            // 
            this.m_checkGPU1.AutoSize = true;
            this.m_checkGPU1.Location = new System.Drawing.Point(327, 131);
            this.m_checkGPU1.Name = "m_checkGPU1";
            this.m_checkGPU1.Size = new System.Drawing.Size(201, 24);
            this.m_checkGPU1.TabIndex = 14;
            this.m_checkGPU1.Text = "INTEL GPU SUPPORT";
            this.m_checkGPU1.UseVisualStyleBackColor = true;
            this.m_checkGPU1.Click += new System.EventHandler(this.m_checkGPU1_Click);
            // 
            // m_checkGPU2
            // 
            this.m_checkGPU2.AutoSize = true;
            this.m_checkGPU2.Location = new System.Drawing.Point(327, 181);
            this.m_checkGPU2.Name = "m_checkGPU2";
            this.m_checkGPU2.Size = new System.Drawing.Size(201, 24);
            this.m_checkGPU2.TabIndex = 15;
            this.m_checkGPU2.Text = "INTEL GPU SUPPORT";
            this.m_checkGPU2.UseVisualStyleBackColor = true;
            this.m_checkGPU2.Click += new System.EventHandler(this.m_checkGPU2_Click);
            // 
            // m_checkAutoDeinterlace
            // 
            this.m_checkAutoDeinterlace.AutoSize = true;
            this.m_checkAutoDeinterlace.Location = new System.Drawing.Point(655, 35);
            this.m_checkAutoDeinterlace.Name = "m_checkAutoDeinterlace";
            this.m_checkAutoDeinterlace.Size = new System.Drawing.Size(191, 24);
            this.m_checkAutoDeinterlace.TabIndex = 16;
            this.m_checkAutoDeinterlace.Text = "AUTO DEINTERLACE";
            this.m_checkAutoDeinterlace.UseVisualStyleBackColor = true;
            this.m_checkAutoDeinterlace.Click += new System.EventHandler(this.m_checkAutoDeinterlace_Click);
            // 
            // timerCheckSignal
            // 
            this.timerCheckSignal.Interval = 1000;
            this.timerCheckSignal.Tick += new System.EventHandler(this.timerCheckSignal_Tick);
            // 
            // m_checkMP4_2
            // 
            this.m_checkMP4_2.AutoCheck = false;
            this.m_checkMP4_2.AutoSize = true;
            this.m_checkMP4_2.Checked = true;
            this.m_checkMP4_2.Location = new System.Drawing.Point(655, 180);
            this.m_checkMP4_2.Name = "m_checkMP4_2";
            this.m_checkMP4_2.Size = new System.Drawing.Size(200, 24);
            this.m_checkMP4_2.TabIndex = 24;
            this.m_checkMP4_2.TabStop = true;
            this.m_checkMP4_2.Text = "MP4 (H.264 + AAC)";
            this.m_checkMP4_2.UseVisualStyleBackColor = true;
            this.m_checkMP4_2.Click += new System.EventHandler(this.m_checkMP4_2_Click);
            // 
            // m_checkAVI_2
            // 
            this.m_checkAVI_2.AutoCheck = false;
            this.m_checkAVI_2.AutoSize = true;
            this.m_checkAVI_2.Location = new System.Drawing.Point(492, 180);
            this.m_checkAVI_2.Name = "m_checkAVI_2";
            this.m_checkAVI_2.Size = new System.Drawing.Size(200, 24);
            this.m_checkAVI_2.TabIndex = 23;
            this.m_checkAVI_2.TabStop = true;
            this.m_checkAVI_2.Text = "AVI (H.264 + PCM)";
            this.m_checkAVI_2.UseVisualStyleBackColor = true;
            this.m_checkAVI_2.Click += new System.EventHandler(this.m_checkAVI_2_Click);
            // 
            // m_checkMP4_1
            // 
            this.m_checkMP4_1.AutoCheck = false;
            this.m_checkMP4_1.AutoSize = true;
            this.m_checkMP4_1.Checked = true;
            this.m_checkMP4_1.Location = new System.Drawing.Point(654, 131);
            this.m_checkMP4_1.Name = "m_checkMP4_1";
            this.m_checkMP4_1.Size = new System.Drawing.Size(200, 24);
            this.m_checkMP4_1.TabIndex = 22;
            this.m_checkMP4_1.TabStop = true;
            this.m_checkMP4_1.Text = "MP4 (H.264 + AAC)";
            this.m_checkMP4_1.UseVisualStyleBackColor = true;
            this.m_checkMP4_1.Click += new System.EventHandler(this.m_checkMP4_1_Click);
            // 
            // m_checkAVI_1
            // 
            this.m_checkAVI_1.AutoCheck = false;
            this.m_checkAVI_1.AutoSize = true;
            this.m_checkAVI_1.Location = new System.Drawing.Point(491, 131);
            this.m_checkAVI_1.Name = "m_checkAVI_1";
            this.m_checkAVI_1.Size = new System.Drawing.Size(200, 24);
            this.m_checkAVI_1.TabIndex = 21;
            this.m_checkAVI_1.TabStop = true;
            this.m_checkAVI_1.Text = "AVI (H.264 + PCM)";
            this.m_checkAVI_1.UseVisualStyleBackColor = true;
            this.m_checkAVI_1.CheckedChanged += new System.EventHandler(this.m_checkAVI_1_CheckedChanged);
            this.m_checkAVI_1.Click += new System.EventHandler(this.m_checkAVI_1_Click);
            // 
            // m_checkShowCloneVideo
            // 
            this.m_checkShowCloneVideo.AutoSize = true;
            this.m_checkShowCloneVideo.Location = new System.Drawing.Point(655, 58);
            this.m_checkShowCloneVideo.Name = "m_checkShowCloneVideo";
            this.m_checkShowCloneVideo.Size = new System.Drawing.Size(191, 24);
            this.m_checkShowCloneVideo.TabIndex = 25;
            this.m_checkShowCloneVideo.Text = "SHOW CLONE VIDEO";
            this.m_checkShowCloneVideo.UseVisualStyleBackColor = true;
            this.m_checkShowCloneVideo.Click += new System.EventHandler(this.m_checkShowCloneVideo_Click);
            // 
            // m_btnOSDSettings
            // 
            this.m_btnOSDSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnOSDSettings.Location = new System.Drawing.Point(487, 35);
            this.m_btnOSDSettings.Name = "m_btnOSDSettings";
            this.m_btnOSDSettings.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_btnOSDSettings.Size = new System.Drawing.Size(154, 43);
            this.m_btnOSDSettings.TabIndex = 26;
            this.m_btnOSDSettings.Text = "OSD SETTINGS +";
            this.m_btnOSDSettings.UseVisualStyleBackColor = true;
            this.m_btnOSDSettings.Click += new System.EventHandler(this.m_btnOSDSettings_Click);
            // 
            // comboDeinterlaceType
            // 
            this.comboDeinterlaceType.FormattingEnabled = true;
            this.comboDeinterlaceType.Items.AddRange(new object[] {
            "BLENDING",
            "MOTIONAD APTIVE"});
            this.comboDeinterlaceType.Location = new System.Drawing.Point(857, 246);
            this.comboDeinterlaceType.Name = "comboDeinterlaceType";
            this.comboDeinterlaceType.Size = new System.Drawing.Size(152, 26);
            this.comboDeinterlaceType.TabIndex = 73;
            this.comboDeinterlaceType.Visible = false;
            this.comboDeinterlaceType.SelectedIndexChanged += new System.EventHandler(this.comboDeinterlaceType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(854, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 20);
            this.label1.TabIndex = 72;
            this.label1.Text = "DEINTERLACE METHOD :";
            this.label1.Visible = false;
            // 
            // MySetupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 237);
            this.Controls.Add(this.comboDeinterlaceType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnOSDSettings);
            this.Controls.Add(this.m_checkShowCloneVideo);
            this.Controls.Add(this.m_checkMP4_2);
            this.Controls.Add(this.m_checkAVI_2);
            this.Controls.Add(this.m_checkMP4_1);
            this.Controls.Add(this.m_checkAVI_1);
            this.Controls.Add(this.m_checkAutoDeinterlace);
            this.Controls.Add(this.m_checkGPU2);
            this.Controls.Add(this.m_checkGPU1);
            this.Controls.Add(this.m_btnRecordStop2);
            this.Controls.Add(this.m_btnRecordStart2);
            this.Controls.Add(this.textBoxRecordAVI2);
            this.Controls.Add(this.textBoxRecordAVI1);
            this.Controls.Add(this.m_btnRecordStop1);
            this.Controls.Add(this.m_btnRecordStart1);
            this.Controls.Add(this.textBoxSnapshotJPG);
            this.Controls.Add(this.textBoxSnapshotBMP);
            this.Controls.Add(this.m_btnVideoQuality);
            this.Controls.Add(this.m_btnSnapshotJPG);
            this.Controls.Add(this.m_btnSnapshotBMP);
            this.Controls.Add(this.m_btnAudioInput);
            this.Controls.Add(this.m_btnVideoInput);
            this.Controls.Add(this.m_staticDeviceFormatInformation);
            this.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MySetupControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Yuan\'s Capture Card Demo Software";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MySetupControl_FormClosed);
            this.Load += new System.EventHandler(this.MySetupControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_staticDeviceFormatInformation;
        private System.Windows.Forms.Timer timerShowInfo;
        private System.Windows.Forms.Button m_btnVideoInput;
        private System.Windows.Forms.Button m_btnAudioInput;
        private System.Windows.Forms.Button m_btnSnapshotBMP;
        private System.Windows.Forms.Button m_btnSnapshotJPG;
        private System.Windows.Forms.Button m_btnVideoQuality;
        private System.Windows.Forms.TextBox textBoxSnapshotBMP;
        private System.Windows.Forms.TextBox textBoxSnapshotJPG;
        private System.Windows.Forms.Button m_btnRecordStart1;
        private System.Windows.Forms.Button m_btnRecordStop1;
        private System.Windows.Forms.TextBox textBoxRecordAVI1;
        private System.Windows.Forms.TextBox textBoxRecordAVI2;
        private System.Windows.Forms.Button m_btnRecordStart2;
        private System.Windows.Forms.Button m_btnRecordStop2;
        private System.Windows.Forms.CheckBox m_checkGPU1;
        private System.Windows.Forms.CheckBox m_checkGPU2;
        private System.Windows.Forms.CheckBox m_checkAutoDeinterlace;
        private System.Windows.Forms.Timer timerCheckSignal;
        private System.Windows.Forms.RadioButton m_checkMP4_2;
        private System.Windows.Forms.RadioButton m_checkAVI_2;
        private System.Windows.Forms.RadioButton m_checkMP4_1;
        private System.Windows.Forms.RadioButton m_checkAVI_1;
        private System.Windows.Forms.CheckBox m_checkShowCloneVideo;
        private System.Windows.Forms.Button m_btnOSDSettings;
        private System.Windows.Forms.ComboBox comboDeinterlaceType;
        private System.Windows.Forms.Label label1;
    }
}