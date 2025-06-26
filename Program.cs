using System;
using System.Windows.Forms;

namespace TourBookingApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var dbHelper = new DatabaseHelper("Host=172.20.7.53;Username=st2991;Password=pwd2991;Database=db2991_10;SearchPath=individ");

            using (var loginForm = new LoginForm(dbHelper))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainForm(dbHelper));
                }
            }
        }
    }
}