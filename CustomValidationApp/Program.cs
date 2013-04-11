using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace CustomValidationApp
{
    public class Program
    {
        public static void Main (string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo ("es-BO");
            Application.EnableVisualStyles ();
            Application.Run (new Register ());
        }
    }
}

