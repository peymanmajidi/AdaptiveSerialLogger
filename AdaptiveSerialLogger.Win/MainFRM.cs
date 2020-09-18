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
    public partial class MainFRM : Form
    {
        public MainFRM()
        {
            InitializeComponent();
        }

        private void MainFRM_Load(object sender, EventArgs e)
        {
            foreach (var port in PortTools.GetList())
            {
                lstPorts.Items.Add(port, false);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            foreach (var port_name in lstPorts.CheckedItems)
            {
                var result = PortTools.AddListener(port_name.ToString());
                //if (result)
                txtLog.Text += $"{port_name}: {result}\r\n";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            foreach (var port_name in PortTools.ClosePorts())
            {
                if (port_name != null)
                    txtLog.Text += $"{port_name}: Closed\r\n";
            }
        }

        private void MainFRM_FormClosed(object sender, FormClosedEventArgs e)
        {
            PortTools.ClosePorts();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (PortTools.Last != null)
            {
                var port = PortTools.Last;
                PortTools.Last = null;
                txtData.Clear();
                txtData.Text = "Port: " + port.serialPort.PortName + Environment.NewLine
                    + "Data: " + port.Data;
            }
        }
    }
}
