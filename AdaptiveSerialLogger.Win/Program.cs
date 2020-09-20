using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdaptiveSerialLogger.Win
{
    static class Program
    {
       public static string FOLDER = "Logs";
       public static int MAX_LOG_LENGTH = 100000;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFRM());
        }
    }
}
