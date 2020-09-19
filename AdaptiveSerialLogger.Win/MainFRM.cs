using AdaptiveSerialLogger.Win.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var folder = Path.Combine(path, Program.FOLDER);
            if (Directory.Exists(folder) == false)
                Directory.CreateDirectory(folder);
            LoadAllPorts();
        }

        private void LoadAllPorts()
        {
            panel.Controls.Clear();
            PortTools.Ports.Clear();
            btnConnnect.Enabled = true;
            foreach (var port_name in PortTools.GetList())
            {
                var port = new Port();
                var icon = new SerialPortIcon()
                {
                    Name = port_name,
                    PortName = port_name,
                    Icon = Properties.Resources.serial_gray,

                };
                port.Icon = icon;
                panel.Controls.Add(port.Icon);
                PortTools.Ports.Add(port);

            }


        }

        private void btnConnect(object sender, EventArgs e)
        {

            PortTools.CloseAllPorts();
            btnConnnect.Enabled = false;
            btnConnnect.Text = "...";
            Cursor = Cursors.WaitCursor;

            new Thread(() => ConnectToAll()).Start();
        }

        private void ConnectToAll()
        {
            foreach (var port in PortTools.Ports.Where(p => p.Icon.Checked).ToList())
            {
                var result = PortTools.AddListener(port.Icon.PortName);
                if (result)
                    port.Icon.Icon = Properties.Resources.serial_normal;
                else
                    port.Icon.Icon = Properties.Resources.serial_ban;

            }
            btnDisConnect.Enabled = true;
            btnConnnect.Text = "Connect All";
            //panel.Enabled = false;
            Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PortTools.CloseAllPorts();
            LoadAllPorts();
        }

        private void MainFRM_FormClosed(object sender, FormClosedEventArgs e)
        {
            PortTools.CloseAllPorts();
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

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextFile.OpenFolder();

        }
    }
}
