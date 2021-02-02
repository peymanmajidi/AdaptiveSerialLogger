using AdaptiveSerialLogger.Win.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        bool first_connect = true;
        private void MainFRM_Load(object sender, EventArgs e)
        {


            var path = AppDomain.CurrentDomain.BaseDirectory;
            var folder = Path.Combine(path, Program.FOLDER);

            if (Directory.Exists(folder) == false)
                Directory.CreateDirectory(folder);
            LoadAllPorts();
            cmbBaod.SelectedIndex = 11;

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
                    port.Icon.IconClick += Icon_IconClick;

                    PortTools.Ports.Add(port);

                    comboBox1.Items.Add(port_name);


                }
                catch
                {


                }

            }


            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }


        }

        private void Icon_IconClick(object sender, EventArgs e)
        {
            var icon = (SerialPortIcon)sender;
            try
            {
                comboBox1.SelectedItem = icon.Name;
                btnConnnect.Text = $"Connect";
            }
            catch (Exception)
            {


            }
        }



        private void btnConnect_Clicked(object sender, EventArgs e)
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
            Int32.TryParse(cmbBaod.Text, out int bRate);
            var parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);

            var task = new Task(() => ConnectToAll(bRate, parity, databit));
            task.Start();
            txtLog.Clear();
            timer1.Enabled = true;


            if (!first_connect)
                TextFile.DataToSave = "";
            txtByteToSave.Text = $"{TextFile.DataToSave.Length} byte(s) to save";


            first_connect = false;



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
            btnConnnect.Text = "Connect";
            //panel.Enabled = false;
            lblMessage.Text = "Done!";

            Cursor = Cursors.Default;
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



        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TextFile.DataToSave.Length < 1)
            {

                MessageBox.Show("Nothing to save yet", "Empty File", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            var save = new SaveFileDialog()
            {
                Filter = "CSV File|*.csv|Text File|*.txt",

            };
            if (save.ShowDialog() == DialogResult.OK)
                TextFile.SaveToFile(save.FileName);

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            txtLog.Clear();
            TextFile.DataToSave = "";
            txtByteToSave.Text = $"{TextFile.DataToSave.Length} byte(s) to save";


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtLog.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtLog.Text = TextFile.Help() + "\r\n" + txtLog.Text;

        }

        private void button4_Click(object sender, EventArgs e)
        {

            Application.Restart();
            Environment.Exit(0);


        }

        private void cmbDataFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            PortTools.DataFormat = (DataFormat)cmbDataFormat.SelectedIndex;
            AlreadyConnectWarning();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtLog.Text = TextFile.Help() + "\r\n" + txtLog.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtMessageTosend_Click(object sender, EventArgs e)
        {
            txtMessageTosend.SelectAll();
        }

        private void txtMessageTosend_MouseClick(object sender, MouseEventArgs e)
        {
            txtMessageTosend.SelectAll();

        }

        private void btnSend_Clicked(object sender, EventArgs e)
        {
            try
            {
                var msg = txtMessageTosend.Text;
                var port_name = comboBox1.SelectedItem.ToString();
                var serial = PortTools.GetPort(port_name);
                if (serial == null)
                {
                    MessageBox.Show("Please Connect to the port first\r\nThen try to send data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                var port = serial.serialPort;
                if (port.IsOpen)
                {
                    new Thread(() =>
                    {
                        port.Write(msg);
                    }).Start();

                    txtLog.Text = $"Message Sent to {port_name}: '{msg}'" + Environment.NewLine + txtLog.Text;


                }
                else
                    MessageBox.Show("Please Connect to the port first\r\nThen try to send data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            catch (Exception ex)
            {
                txtLog.Text = ex.Message + Environment.NewLine + txtLog.Text;

            }
        }

        private void cmbBaod_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlreadyConnectWarning();
        }

        private void AlreadyConnectWarning()
        {
            if (btnConnnect.Enabled == false)
            {
                MessageBox.Show("You are already connected.\r\nIf you want to apply new configs, Please connect again", "New Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void cmbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlreadyConnectWarning();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PresetButtons(sender);
        }

        private void PresetButtons(object sender)
        {
            var msg = ((Button)sender).Text;
            txtMessageTosend.Text = msg;
            btnSend_Clicked(null, null);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            PresetButtons(sender);
        }

        private void btnR(object sender, EventArgs e)
        {
            PresetButtons(sender);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PresetButtons(sender);
        }
        string lastLog = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lastLog != TextFile.DataToLog)
            {
                lastLog = TextFile.DataToLog;
                var log = TextFile.DataToLog + Environment.NewLine + txtLog.Text;
                if (log.Length > Program.MAX_LOG_LENGTH)
                    log = log.Substring(0, Program.MAX_LOG_LENGTH);
                txtLog.Text = log;

                TextFile.DataToSave += TextFile.Data;
                if (chkNewline.Checked)
                    TextFile.DataToSave += Environment.NewLine;

                txtByteToSave.Text = $"{TextFile.DataToSave.Length} byte(s) to save";

            }
        }

        private void txtByteToSave_Click(object sender, EventArgs e)
        {
            new TextViewFRM().ShowDialog();
            txtByteToSave.Text = $"{TextFile.DataToSave.Length} byte(s) to save";

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            new TextViewFRM().ShowDialog();
            txtByteToSave.Text = $"{TextFile.DataToSave.Length} byte(s) to save";


        }

        private void button11_Click(object sender, EventArgs e)
        {
            btnConnect_Clicked( sender,  e);
            btnR( sender,  e);
        }
    }
}
