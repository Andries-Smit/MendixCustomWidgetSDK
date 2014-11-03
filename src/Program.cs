using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MendixWidgets
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load the file if we start it up!
            Main frmMain = new Main();
            if (args.Length > 0)
            {

                frmMain.argument = args[0];
            }

            Application.Run(frmMain);
        }
    }
}
