using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptiveSerialLogger.Win.Services
{
    class PortTools
    {
        public static List<Port> Ports = new List<Port>();
        public static Port Last;




        
        public static List<string> GetList()
        {
            return SerialPort.GetPortNames().OrderBy(x=>x.Length).ThenBy(x=>x).ToList();

        }

        public static List<Port> GetListOfSelected()
        {
            return PortTools.Ports.Where(p => p.Icon.Checked).ToList();

        }



        public static bool AddListener(string port_name, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8)
        {
            ClosePort(port_name);
            var mySerialPort = new SerialPort(port_name);

            try
            {
                mySerialPort.BaudRate = baudRate;
                mySerialPort.Parity = parity;
                mySerialPort.StopBits = StopBits.One;
                mySerialPort.DataBits = dataBits;
                mySerialPort.Handshake = Handshake.None;

                mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                mySerialPort.Open();
                Ports.Add(new Port()
                {
                    serialPort = mySerialPort,
                    Data = ""
                });
                return true;

            }
            catch
            {


            }
            return false;
        }

        public static void CloseAllPorts()
        {

            foreach (var serialPort in Ports)
            {
              if (serialPort.serialPort.IsOpen)
                        serialPort.serialPort.Close();
                if (serialPort.Icon != null)
                    serialPort.Icon.Icon = Properties.Resources.serial_gray;
            }
           
        }

        public static void ClosePort(string port_name)
        {
            var port = GetPort(port_name);
            if (port == null)
                return;

            if (port.serialPort.IsOpen)
                port.serialPort.Close();
            port.serialPort = new SerialPort();
            port.Data = "";
            port.Icon.Icon = Properties.Resources.serial_gray;
        }

        public static Port GetPort(string port_name)
        {
            return Ports.FirstOrDefault(p => p.serialPort.PortName.Equals(port_name));

        }

        public static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort)sender;
            var data = sp.ReadExisting();
            var my_port = (SerialPort)sender;
            var port = GetPort(my_port.PortName);
            port.Data += data + "\r\n";

            Last = new Port
            {
                serialPort = my_port,
                Data = data
            };


        }


    }
}
