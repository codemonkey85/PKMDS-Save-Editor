using System;
using System.Windows.Forms;

namespace PKMDS_Save_Editor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string filePath = string.Empty;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                filePath = args[0];
            }
            Application.Run(new frmMain(filePath));
        }
    }
}
