﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdaptiveSerialLogger.Win.Services;

namespace AdaptiveSerialLogger.Win
{
    public partial class SerialPortIcon : UserControl
    {
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler IconClick;

        public Port Port
        {
            get; set;
        }
        string portName;
        public Image Icon
        {
         set
            {
                picIcon.Image = value;
            }
        }

        public bool Checked
        {
            get
            {
                return chkPort.Checked;
            }
            set
            {
                chkPort.Checked = value;
            }
        }
        public string PortName
        {
            get
            {
                return portName;
            }
            set
            {
                chkPort.Text = value;
                portName = value;
            }
        }
        public SerialPortIcon()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.IconClick != null)
                this.IconClick(this, e);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void chkPort_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void picIcon_MouseClick(object sender, MouseEventArgs e)
        {
            chkPort.Checked = !chkPort.Checked;
        }

        private void picIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            chkPort.Checked = !chkPort.Checked;

            //TextFile.OpenFile(portName);
           // new SenderFRM(PortTools.GetPort(PortName)).Show();
        }
    }
}
