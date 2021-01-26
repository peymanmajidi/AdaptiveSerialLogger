namespace AdaptiveSerialLogger.Win
{
    partial class TextViewFRM
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
            this.txtMessageTosend = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMessageTosend
            // 
            this.txtMessageTosend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessageTosend.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageTosend.Location = new System.Drawing.Point(0, 0);
            this.txtMessageTosend.Multiline = true;
            this.txtMessageTosend.Name = "txtMessageTosend";
            this.txtMessageTosend.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageTosend.Size = new System.Drawing.Size(528, 592);
            this.txtMessageTosend.TabIndex = 555;
            this.txtMessageTosend.TextChanged += new System.EventHandler(this.txtMessageTosend_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(339, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // TextViewFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 592);
            this.Controls.Add(this.txtMessageTosend);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TextViewFRM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data To Save";
            this.Load += new System.EventHandler(this.SenderFRM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessageTosend;
        private System.Windows.Forms.Button button1;
    }
}