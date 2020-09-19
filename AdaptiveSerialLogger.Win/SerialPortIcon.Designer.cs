namespace AdaptiveSerialLogger.Win
{
    partial class SerialPortIcon
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.chkPort = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picIcon.Image = global::AdaptiveSerialLogger.Win.Properties.Resources.serial_normal;
            this.picIcon.Location = new System.Drawing.Point(2, 2);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(126, 58);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            this.picIcon.Click += new System.EventHandler(this.pictureBox1_Click);
            this.picIcon.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // chkPort
            // 
            this.chkPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.chkPort.Location = new System.Drawing.Point(15, 59);
            this.chkPort.Name = "chkPort";
            this.chkPort.Size = new System.Drawing.Size(105, 33);
            this.chkPort.TabIndex = 1;
            this.chkPort.Text = "NOT SET";
            this.chkPort.UseVisualStyleBackColor = true;
            this.chkPort.CheckedChanged += new System.EventHandler(this.chkPort_CheckedChanged);
            // 
            // SerialPortIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.chkPort);
            this.Name = "SerialPortIcon";
            this.Size = new System.Drawing.Size(132, 91);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.CheckBox chkPort;
        private System.Windows.Forms.PictureBox picIcon;
    }
}
