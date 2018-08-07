using BeeSys.Shared.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoccerApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string sPath = Environment.GetEnvironmentVariable("Wasp3.5", EnvironmentVariableTarget.Machine);

            AssemblyResolver.AddPath
                    (
                        Path.Combine(sPath, "Shared Resources")
                    );

            Process[] result = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (result.Length > 1)
            {
                MessageBox.Show("There is already a instance running.", "Information");
                System.Environment.Exit(0);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SoccerApp());
        }
    }
}
