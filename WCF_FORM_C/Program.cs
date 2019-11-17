using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCF_FORM_C
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static Home Form;
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);

            Form = new Home();

            Application.EnableVisualStyles();
            Application.Run(Form);

           
        }
    }
}
