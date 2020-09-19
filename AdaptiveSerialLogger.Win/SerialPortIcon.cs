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
        public SerialPortIcon()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
    }
}
