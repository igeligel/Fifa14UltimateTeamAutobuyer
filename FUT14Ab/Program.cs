using System;
using System.Windows.Forms;

namespace FUT14AB
{
    internal static class Program
    {
        /// <summary>
        /// Main entry point of application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
