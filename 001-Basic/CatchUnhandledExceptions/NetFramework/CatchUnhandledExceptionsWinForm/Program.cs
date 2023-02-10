using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatchUnhandledExceptionsWinForm
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1.註冊Unhandled Exception Handler
            Application.ThreadException += ThreadException;
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            TaskScheduler.UnobservedTaskException += UnobservedTaskException;

            // 2.開啟視窗
            Application.Run(new Form1());

            // 3.關閉視窗後執行
            throw new Exception("Console Error");
        }

        // 攔截UI Unhandled Exception
        static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Thread Exception");
        }

        // 攔截Console Unhandled Exception
        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled Exception");
        }

        // 攔截Task Unhandled Exception
        static void UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.InnerException.Message, "Unobserved Task Exception");
        }
    }
}
