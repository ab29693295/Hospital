namespace Hospital
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelNoSignal = new System.Windows.Forms.Label();
            this.timerCheckSignal = new System.Windows.Forms.Timer(this.components);
            this.CloneChannelPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // labelNoSignal
            // 
            this.labelNoSignal.AutoSize = true;
            this.labelNoSignal.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoSignal.ForeColor = System.Drawing.Color.Red;
            this.labelNoSignal.Location = new System.Drawing.Point(371, 208);
            this.labelNoSignal.Name = "labelNoSignal";
            this.labelNoSignal.Size = new System.Drawing.Size(219, 53);
            this.labelNoSignal.TabIndex = 0;
            this.labelNoSignal.Text = "没有信号";
            this.labelNoSignal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelNoSignal.Visible = false;
            // 
            // timerCheckSignal
            // 
            this.timerCheckSignal.Interval = 500;
            this.timerCheckSignal.Tick += new System.EventHandler(this.timerCheckSignal_Tick);
            // 
            // CloneChannelPanel
            // 
            this.CloneChannelPanel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.CloneChannelPanel.Location = new System.Drawing.Point(711, 327);
            this.CloneChannelPanel.Name = "CloneChannelPanel";
            this.CloneChannelPanel.Size = new System.Drawing.Size(240, 178);
            this.CloneChannelPanel.TabIndex = 1;
            this.CloneChannelPanel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1222, 680);
            this.Controls.Add(this.CloneChannelPanel);
            this.Controls.Add(this.labelNoSignal);
            this.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HDVR 高清影像录播系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNoSignal;
        private System.Windows.Forms.Timer timerCheckSignal;
        private System.Windows.Forms.Panel CloneChannelPanel;
    }
}

