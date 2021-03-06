﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdaptiveSerialLogger.Win.Services
{
    enum DataFormat
    {
        Auto = 0,
        NewLine = 1,
        Sequence = 2
    }
    class PortTools
    {
        public static List<Port> Ports = new List<Port>();
 


        public static DataFormat DataFormat
        {
            get; set;
        } = DataFormat.Auto;




        public static List<string> GetList()
        {
            return SerialPort.GetPortNames().OrderBy(x => x.Length).ThenBy(x => x).ToList();

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
                mySerialPort.NewLine = Environment.NewLine;

                mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                mySerialPort.Disposed += SerialPort_Disposed;


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

        private static void SerialPort_Disposed(object sender, EventArgs e)
        {
            var mySerialPort = (SerialPort)sender;
            if (mySerialPort.IsOpen)
            {
                mySerialPort.DiscardInBuffer();
                mySerialPort.Close(); }
           
        }

        public static void CloseAllPorts()
        {
           
                foreach (var item in Ports)
                {
                    try
                    {
                        if (item.serialPort.IsOpen)
                        {
                            item.serialPort.DiscardInBuffer();
                            item.serialPort.Close();
                        }

                    }
                    catch
                    {


                    }

                    try
                    {
                        if (item.Icon != null)
                            item.Icon.Icon = Properties.Resources.serial_gray;
                    }
                    catch
                    {


                    }
                }
         

        }

        public static void ClosePort(string port_name)
        {
            var port = GetPort(port_name);
            if (port == null)
                return;

            try
            {
                if (port.serialPort.IsOpen)
                    port.serialPort.Close();
                port.serialPort = new SerialPort();
                port.Data = "";
                port.Icon.Icon = Properties.Resources.serial_gray;
            }
            catch
            {


            }
        }

        public static Port GetPort(string port_name)
        {
            return Ports.FirstOrDefault(p => p.serialPort.PortName.Equals(port_name));

        }

        public static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort)sender;
            string data;
            if (DataFormat == DataFormat.Sequence)
                data = sp.ReadExisting();
            else if (DataFormat == DataFormat.NewLine)
                data = sp.ReadLine();
            else
            {
                var myport = Ports.FirstOrDefault(p=> p.serialPort == sp);
                if (myport.HasNewLine)
                    data = sp.ReadLine();
                else
                {
                    data = sp.ReadExisting();
                    myport.HasNewLine = data.Contains(sp.NewLine);

                }

            }



            var my_port = (SerialPort)sender;
            TextFile.Data = data;
            TextFile.DataToLog = $"Port: [{my_port.PortName}] Time:{DateTime.Now.ToString("HH:mm:ss")} Data: `{data}`";
           


        }


    }
}
