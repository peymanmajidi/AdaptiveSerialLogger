using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdaptiveSerialLogger.Win.Services
{
    class TextFile
    {
        public static string DataToSave { get; set; }
        public static void OpenFile(string port_name, bool banner = true)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;

                var file = path + "\\" + Program.FOLDER + "\\_" + port_name + ".txt";
                CreateFile(port_name, banner);

                System.Diagnostics.Process.Start("notepad.exe", file);
            }
            catch
            {

                
            }

        }

        public static void SaveToFile(string filename)
        {
            try
            {

                File.WriteAllText(filename, TextFile.DataToSave);

                System.Diagnostics.Process.Start("notepad.exe", filename);
            }
            catch
            {


            }

        }


        public static void CreateFile(string port_name, bool banner = false)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;

                var file = path + "\\" + Program.FOLDER + "\\_" + port_name + ".txt";
                if (File.Exists(file) == false)
                {
                    File.Create(file).Close();
                    if (banner)
                        File.WriteAllText(file, TextFile.Banner());

                    var body = $"☐ Serial Port: [{port_name}]\r\n☐ Create Date: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}\r\n\r\n☐ Logs:\r\n";
                    File.AppendAllText(file, body);
                }
            }
            catch
            {


            }

        }

        public static void Append(string port_name, string data, bool banner = false, bool newLineAppend = false)
        {
            try
            {
                
                if (newLineAppend)
                    data = Environment.NewLine + data;

                DataToSave += data;
            }
            catch
            {


            }

        }
        public static string Help()
        {
            return $@"Help:
Checked `Add New-Line end of each data': Add a new line character between old and new stream data

Contact Information:
 USA: +1-520-270-1304
 Germany: +49-176-2290-3563 +49-176-6870-7392
 Director@adaptiveagrotech.com
 Redmond@adaptiveagrotech.com

Report Bugs: me@peymanmajidi.ir
Copyright © AdaptiveAgroTech Consultancy Int.
2008 - {DateTime.Now.Year}
";
        }

        public static void OpenFolder()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var folder = Path.Combine(path, Program.FOLDER);

            System.Diagnostics.Process.Start("Explorer.exe", folder);


        }
        public static string Banner()
        {
            return @"███████████████████████████████████████████████
██▀▄─██▄─▄▄▀██▀▄─██▄─▄▄─█─▄─▄─█▄─▄█▄─█─▄█▄─▄▄─█
██─▀─███─██─██─▀─███─▄▄▄███─████─███▄▀▄███─▄█▀█
▀▄▄▀▄▄▀▄▄▄▄▀▀▄▄▀▄▄▀▄▄▄▀▀▀▀▄▄▄▀▀▄▄▄▀▀▀▄▀▀▀▄▄▄▄▄▀
███████████▀██████████████████████████████████
██▀▄─██─▄▄▄▄█▄─▄▄▀█─▄▄─█─▄─▄─█▄─▄▄─█─▄▄▄─█─█─█
██─▀─██─██▄─██─▄─▄█─██─███─████─▄█▀█─███▀█─▄─█
▀▄▄▀▄▄▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▀▀▄▄▄▀▀▄▄▄▄▄▀▄▄▄▄▄▀▄▀▄▀

";
        }
        public static string Logo()
        {
            return @"
              _             _   _            
     /\      | |           | | (_)           
    /  \   __| | __ _ _ __ | |_ ___   _____  
   / /\ \ / _` |/ _` | '_ \| __| \ \ / / _ \ 
  / ____ \ (_| | (_| | |_) | |_| |\ V /  __/ 
 /_/    \_\__,_|\__,_| .__/ \__|_| \_/ \___| 
     /\              | | | |          | |    
    /  \   __ _ _ __ |_| | |_ ___  ___| |__  
   / /\ \ / _` | '__/ _ \| __/ _ \/ __| '_ \ 
  / ____ \ (_| | | | (_) | ||  __/ (__| | | |
 /_/    \_\__, |_|  \___/ \__\___|\___|_| |_|
           __/ |                             
          |___/                              
Visit: https://www.adaptiveagrotech.com/
";
        }

    }
}
