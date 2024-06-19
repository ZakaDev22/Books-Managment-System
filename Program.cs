using IslamIsLife.Global_Classes;
using IslamIsLife.Main_And_Login_Forms;
using IslamIsLife.People_Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslamIsLife
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(new LoginForm());

            // Application.Run(new MainForm(clsGlobal.LoginForm));


            // Application.Run(new ShowPersonInfo(1));

           //  Application.Run(new TestForm());


        }
    }
}
