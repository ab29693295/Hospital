namespace Hospital
{
    partial class MyVideoPropertyDlg
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.m_sliderBrightness = new System.Windows.Forms.TrackBar();
            this.m_staticBrightness = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_sliderContrast = new System.Windows.Forms.TrackBar();
            this.m_staticContrast = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m_sliderHue = new System.Windows.Forms.TrackBar();
            this.m_staticHue = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.m_sliderSaturation = new System.Windows.Forms.TrackBar();
            this.m_staticSaturation = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.m_sliderSharpness = new System.Windows.Forms.TrackBar();
            this.m_staticSharpness = new System.Windows.Forms.Label();
            this.btnDefault = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderSharpness)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(242, 155);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 82;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 12);
            this.label4.TabIndex = 83;
            this.label4.Text = "BRIGHTNESS";
            // 
            // m_sliderBrightness
            // 
            this.m_sliderBrightness.Location = new System.Drawing.Point(93, 9);
            this.m_sliderBrightness.Maximum = 255;
            this.m_sliderBrightness.Name = "m_sliderBrightness";
            this.m_sliderBrightness.Size = new System.Drawing.Size(200, 45);
            this.m_sliderBrightness.TabIndex = 84;
            this.m_sliderBrightness.TickStyle = System.Windows.Forms.TickStyle.None;
            this.m_sliderBrightness.Scroll += new System.EventHandler(this.m_sliderBrightness_Scroll);
            // 
            // m_staticBrightness
            // 
            this.m_staticBrightness.AutoSize = true;
            this.m_staticBrightness.Location = new System.Drawing.Point(299, 16);
            this.m_staticBrightness.Name = "m_staticBrightness";
            this.m_staticBrightness.Size = new System.Drawing.Size(23, 12);
            this.m_staticBrightness.TabIndex = 85;
            this.m_staticBrightness.Text = "255";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 86;
            this.label5.Text = "CONTRAST";
            // 
            // m_sliderContrast
            // 
            this.m_sliderContrast.Location = new System.Drawing.Point(93, 34);
            this.m_sliderContrast.Maximum = 255;
            this.m_sliderContrast.Name = "m_sliderContrast";
            this.m_sliderContrast.Size = new System.Drawing.Size(200, 45);
            this.m_sliderContrast.TabIndex = 87;
            this.m_sliderContrast.TickStyle = System.Windows.Forms.TickStyle.None;
            this.m_sliderContrast.Scroll += new System.EventHandler(this.m_sliderContrast_Scroll);
            // 
            // m_staticContrast
            // 
            this.m_staticContrast.AutoSize = true;
            this.m_staticContrast.Location = new System.Drawing.Point(299, 41);
            this.m_staticContrast.Name = "m_staticContrast";
            this.m_staticContrast.Size = new System.Drawing.Size(23, 12);
            this.m_staticContrast.TabIndex = 88;
            this.m_staticContrast.Text = "255";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 12);
            this.label7.TabIndex = 89;
            this.label7.Text = "HUE";
            // 
            // m_sliderHue
            // 
            this.m_sliderHue.Location = new System.Drawing.Point(93, 58);
            this.m_sliderHue.Maximum = 255;
            this.m_sliderHue.Name = "m_sliderHue";
            this.m_sliderHue.Size = new System.Drawing.Size(200, 45);
            this.m_sliderHue.TabIndex = 90;
            this.m_sliderHue.TickStyle = System.Windows.Forms.TickStyle.None;
            this.m_sliderHue.Scroll += new System.EventHandler(this.m_sliderHue_Scroll);
            // 
            // m_staticHue
            // 
            this.m_staticHue.AutoSize = true;
            this.m_staticHue.Location = new System.Drawing.Point(300, 64);
            this.m_staticHue.Name = "m_staticHue";
            this.m_staticHue.Size = new System.Drawing.Size(23, 12);
            this.m_staticHue.TabIndex = 91;
            this.m_staticHue.Text = "255";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 92;
            this.label8.Text = "SATURATION";
            // 
            // m_sliderSaturation
            // 
            this.m_sliderSaturation.Location = new System.Drawing.Point(93, 82);
            this.m_sliderSaturation.Maximum = 255;
            this.m_sliderSaturation.Name = "m_sliderSaturation";
            this.m_sliderSaturation.Size = new System.Drawing.Size(200, 45);
            this.m_sliderSaturation.TabIndex = 93;
            this.m_sliderSaturation.TickStyle = System.Windows.Forms.TickStyle.None;
            this.m_sliderSaturation.Scroll += new System.EventHandler(this.m_sliderSaturation_Scroll);
            // 
            // m_staticSaturation
            // 
            this.m_staticSaturation.AutoSize = true;
            this.m_staticSaturation.Location = new System.Drawing.Point(299, 89);
            this.m_staticSaturation.Name = "m_staticSaturation";
            this.m_staticSaturation.Size = new System.Drawing.Size(23, 12);
            this.m_staticSaturation.TabIndex = 94;
            this.m_staticSaturation.Text = "255";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 115);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 12);
            this.label9.TabIndex = 95;
            this.label9.Text = "SHARPNESS";
            // 
            // m_sliderSharpness
            // 
            this.m_sliderSharpness.Location = new System.Drawing.Point(94, 105);
            this.m_sliderSharpness.Maximum = 255;
            this.m_sliderSharpness.Name = "m_sliderSharpness";
            this.m_sliderSharpness.Size = new System.Drawing.Size(200, 45);
            this.m_sliderSharpness.TabIndex = 96;
            this.m_sliderSharpness.TickStyle = System.Windows.Forms.TickStyle.None;
            this.m_sliderSharpness.Scroll += new System.EventHandler(this.m_sliderSharpness_Scroll);
            // 
            // m_staticSharpness
            // 
            this.m_staticSharpness.AutoSize = true;
            this.m_staticSharpness.Location = new System.Drawing.Point(299, 115);
            this.m_staticSharpness.Name = "m_staticSharpness";
            this.m_staticSharpness.Size = new System.Drawing.Size(23, 12);
            this.m_staticSharpness.TabIndex = 97;
            this.m_staticSharpness.Text = "255";
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(161, 155);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 98;
            this.btnDefault.Text = "DEFAULT";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // MyVideoPropertyDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 190);
            this.ControlBox = false;
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.m_staticSharpness);
            this.Controls.Add(this.m_sliderSharpness);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.m_staticSaturation);
            this.Controls.Add(this.m_sliderSaturation);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.m_staticHue);
            this.Controls.Add(this.m_sliderHue);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_staticContrast);
            this.Controls.Add(this.m_sliderContrast);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_staticBrightness);
            this.Controls.Add(this.m_sliderBrightness);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonOK);
            this.Name = "MyVideoPropertyDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SET VIDEO PROPERTY";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MyVideoPropertyDlg_Load);
            this.Shown += new System.EventHandler(this.MyVideoPropertyDlg_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MyVideoPropertyDlg_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_sliderSharpness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar m_sliderBrightness;
        private System.Windows.Forms.Label m_staticBrightness;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar m_sliderContrast;
        private System.Windows.Forms.Label m_staticContrast;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar m_sliderHue;
        private System.Windows.Forms.Label m_staticHue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar m_sliderSaturation;
        private System.Windows.Forms.Label m_staticSaturation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar m_sliderSharpness;
        private System.Windows.Forms.Label m_staticSharpness;
        internal System.Windows.Forms.Button btnDefault;
    }
}