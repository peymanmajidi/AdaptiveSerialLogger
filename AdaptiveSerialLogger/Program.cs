using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptiveSerialLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get a list of serial port names.
            var ports = SerialPort.GetPortNames();

            Console.WriteLine("The following serial ports were found:");

            // Display each port name to the console.
            foreach (string port in ports)
            {
                Console.WriteLine(port);
            }

            SerialPort mySerialPort = new SerialPort("COM1");

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            mySerialPort.Open();

            Console.WriteLine("Listening....");
            Console.WriteLine();
            Console.ReadKey();
            mySerialPort.Close();

            Console.ReadLine();
        }

        private static void DataReceivedHandler(
                       object sender,
                       SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:");
            Console.Write(indata);

            callmyfunction(indata);
        }

        private static void callmyfunction(string data)
        {
            //....
        }
    }
}
