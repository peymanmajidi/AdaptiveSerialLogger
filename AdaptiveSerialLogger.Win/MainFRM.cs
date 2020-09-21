using AdaptiveSerialLogger.Win.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
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

            cmbDataFormat.SelectedIndex = cmbParity.SelectedIndex = 0;
        }

        private void LoadAllPorts()
        {
            panel.Controls.Clear();
            PortTools.Ports.Clear();
            btnConnnect.Enabled = true;
            foreach (var port_name in PortTools.GetList())
            {
                try
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
                catch 
                {

                   
                }
            }


        }

        private void btnConnect(object sender, EventArgs e)
        {
            if (PortTools.GetListOfSelected().Count() == 0)
            {

                MessageBox.Show("Please select at least 1 Serial port", "None Serial Port selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                panel.Focus();
                return;
            }


            PortTools.CloseAllPorts();
            btnConnnect.Enabled = false;
            btnConnnect.Text = "...";
            lblMessage.Text = "Please wait...";
            statusStrip1.Refresh();
            Cursor = Cursors.WaitCursor;
            Int32.TryParse(txtDatabit.Text, out int databit);
            Int32.TryParse(txtBrate.Text, out int bRate);
            var parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);

            var task = new Task(() => ConnectToAll(bRate, parity, databit));
            task.Start();
            txtData.Clear();
            timer1.Enabled = true;



        }

        private void ConnectToAll(int bRate, Parity parity, int databit)
        {
            foreach (var port in PortTools.GetListOfSelected())
            {


                try
                {
                    var result = PortTools.AddListener(port.Icon.PortName, bRate, parity, databit);

                    if (result)
                        port.Icon.Icon = Properties.Resources.serial_normal;
                    else
                        port.Icon.Icon = Properties.Resources.serial_ban;

                }
                catch 
                {

                }


            }
            btnDisConnect.Enabled = true;
            btnConnnect.Text = "Connect All";
            //panel.Enabled = false;
            lblMessage.Text = "Done!";

            Cursor = Cursors.Default;
        }

        private void CheckEntries()
        {
            Int32.TryParse(txtDatabit.Text, out int databit);
            Int32.TryParse(txtBrate.Text, out int bRate);


        }

        private void btnDis(object sender, EventArgs e)
        {
            PortTools.CloseAllPorts();
            btnConnnect.Enabled = true;
            btnDisConnect.Enabled = false;


            timer1.Enabled = false;

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
                TextFile.Append(port.serialPort.PortName, port.Data, chkBanner.Checked, chkNewline.Checked);

                var log = $"Port: [{port.serialPort.PortName}] Time:{DateTime.Now.ToString("HH:mm:ss")} Data: `{port.Data}`\r\n" + txtData.Text;
                if (log.Length > Program.MAX_LOG_LENGTH)
                    log = log.Substring(0, Program.MAX_LOG_LENGTH);
                txtData.Text = log;

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

        private void button2_Click_1(object sender, EventArgs e)
        {
            txtData.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtData.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.adaptiveagrotech.com/");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Restart();

        }

        private void cmbDataFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            PortTools.DataFormat = (DataFormat)cmbDataFormat.SelectedIndex;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtData.Text = TextFile.Help() + "\r\n" + txtData.Text;
        }
    }
}
