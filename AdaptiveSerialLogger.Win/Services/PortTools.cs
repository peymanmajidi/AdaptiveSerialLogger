using System;
using System.Collections.Generic;
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
           return SerialPort.GetPortNames().ToList();
            
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

        public static IEnumerable<string> CloseAllPorts()
        {

            foreach (var serialPort in Ports)
            {
                var port = serialPort.serialPort.PortName;
                try
                {
                    serialPort.serialPort.Close();

                }
                catch
                {
                    port = null;

                }
                if (port != null)
                    yield return serialPort.serialPort.PortName;

            }
            Ports.Clear();
        }

        public static void ClosePort(string port)
        {

            foreach (var serialPort in Ports.Where(p => p.serialPort.PortName.Equals(port)))
            {
                try
                {
                    serialPort.serialPort.Close();
                    Ports.Remove(serialPort);

                }
                catch
                {


                }

            }
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
