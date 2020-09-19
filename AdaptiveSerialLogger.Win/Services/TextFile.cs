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
        public static void OpenFile(string port_name)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;

            var file = path + "\\" + Program.FOLDER + "\\_" + port_name + ".txt";
            if (File.Exists(file) == false)
            {
                File.Create(file).Close();
               
            }

            System.Diagnostics.Process.Start("notepad.exe", file);

        }
        public static void OpenFolder()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var folder = Path.Combine(path, Program.FOLDER);

            System.Diagnostics.Process.Start("Explorer.exe", folder);


        }

    }
}
