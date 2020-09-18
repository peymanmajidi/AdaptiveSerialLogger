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
        public static List<Port> Ports= new List<Port>();
        public static Port Last;




        public static List<string> GetList()
        {
            var ports = SerialPort.GetPortNames().ToList();
            return ports;
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

        public static IEnumerable<string> ClosePorts()
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
                if(port != null)
                    yield return serialPort.serialPort.PortName;

            }
            Ports.Clear();
        }

        public static void ClosePort(string port)
        {

            foreach (var serialPort in Ports.Where(p=>p.serialPort.PortName.Equals(port)))
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

        public static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort)sender;
            var data = sp.ReadExisting();
            var my_port = (SerialPort)sender;
            var port = Ports.FirstOrDefault(p=>p.serialPort.PortName.Equals(my_port.PortName));
            port.Data += data + "\r\n";

            Last = port;


        }


    }
}
