using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaptiveSerialLogger.Win.Services
{
    public class Port
    {
        public SerialPortIcon Icon = new SerialPortIcon();
        public bool HasNewLine = false;
        public SerialPort serialPort = new SerialPort();
        public string Data;
    }
}
