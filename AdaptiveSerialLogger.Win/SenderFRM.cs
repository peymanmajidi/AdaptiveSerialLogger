using AdaptiveSerialLogger.Win.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdaptiveSerialLogger.Win
{
    public partial class SenderFRM : Form
    {

        public Port Port;
        public SenderFRM(Port port)
        {
            InitializeComponent();
            Port = port;
        }

        private void SenderFRM_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var msg = txtMessageTosend.Text;
            var port = Port.serialPort;
            if(port.IsOpen)
                        port.Write(msg);
            else
                MessageBox.Show("not connected yet" , "", MessageBoxButtons.OK, MessageBoxIcon.Error );
        }
    }
}
