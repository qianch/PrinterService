using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PrinterService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
			bool flag = false;
			new Mutex(true, Application.ProductName, out flag);
			if (flag)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new PrinterService());
			}
			else
			{
				MessageBox.Show("应用程序已经在运行中...");
				Thread.Sleep(1000);
				Environment.Exit(1);
			}
		}
	}
}
