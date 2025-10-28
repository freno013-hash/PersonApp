// ============================================================
// Namn: Fredrik Beck-Norén
// E-post: fredrikbecknoren@gmail.com
// Kurs: L0002B – Inlämningsuppgift 3 (Windows Forms)
// Datum: 28/10-2025
// ============================================================

using System;
using System.Windows.Forms;

namespace PersonApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
