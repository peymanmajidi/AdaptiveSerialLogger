using System;
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
            chkPort.Checked = !chkPort.Checked;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void chkPort_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
