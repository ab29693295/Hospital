namespace Hospital
{
    partial class MyAudioInputDlg
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
            this.RadioButtonInputEMBEDDEDLINEIN = new System.Windows.Forms.RadioButton();
            this.RadioButtonInputEMBEDDEDAUDIO = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RadioButtonInputEMBEDDEDLINEIN
            // 
            this.RadioButtonInputEMBEDDEDLINEIN.AutoSize = true;
            this.RadioButtonInputEMBEDDEDLINEIN.Location = new System.Drawing.Point(12, 44);
            this.RadioButtonInputEMBEDDEDLINEIN.Name = "RadioButtonInputEMBEDDEDLINEIN";
            this.RadioButtonInputEMBEDDEDLINEIN.Size = new System.Drawing.Size(65, 16);
            this.RadioButtonInputEMBEDDEDLINEIN.TabIndex = 84;
            this.RadioButtonInputEMBEDDEDLINEIN.TabStop = true;
            this.RadioButtonInputEMBEDDEDLINEIN.Text = "LINE-IN";
            this.RadioButtonInputEMBEDDEDLINEIN.UseVisualStyleBackColor = true;
            this.RadioButtonInputEMBEDDEDLINEIN.Click += new System.EventHandler(this.RadioButtonInputEMBEDDEDLINEIN_Click);
            // 
            // RadioButtonInputEMBEDDEDAUDIO
            // 
            this.RadioButtonInputEMBEDDEDAUDIO.AutoSize = true;
            this.RadioButtonInputEMBEDDEDAUDIO.Location = new System.Drawing.Point(12, 18);
            this.RadioButtonInputEMBEDDEDAUDIO.Name = "RadioButtonInputEMBEDDEDAUDIO";
            this.RadioButtonInputEMBEDDEDAUDIO.Size = new System.Drawing.Size(125, 16);
            this.RadioButtonInputEMBEDDEDAUDIO.TabIndex = 83;
            this.RadioButtonInputEMBEDDEDAUDIO.TabStop = true;
            this.RadioButtonInputEMBEDDEDAUDIO.Text = "EMBEDDED AUDIO";
            this.RadioButtonInputEMBEDDEDAUDIO.UseVisualStyleBackColor = true;
            this.RadioButtonInputEMBEDDEDAUDIO.Click += new System.EventHandler(this.RadioButtonInputEMBEDDEDAUDIO_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(168, 44);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 86;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // MyAudioInputDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 81);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.RadioButtonInputEMBEDDEDLINEIN);
            this.Controls.Add(this.RadioButtonInputEMBEDDEDAUDIO);
            this.Name = "MyAudioInputDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SET AUDIO INPUT";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MyAudioInputDlg_Load);
            this.Shown += new System.EventHandler(this.MyAudioInputDlg_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MyAudioInputDlg_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.RadioButton RadioButtonInputEMBEDDEDLINEIN;
        internal System.Windows.Forms.RadioButton RadioButtonInputEMBEDDEDAUDIO;
        internal System.Windows.Forms.Button buttonOK;
    }
}