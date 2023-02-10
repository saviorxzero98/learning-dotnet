using System;
using System.Threading;
using System.Threading.Tasks;

namespace CatchUnhandledExceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.註冊Unhandled Exception Handler
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            TaskScheduler.UnobservedTaskException += UnobservedTaskException;

            // 2.新增2個Thread
            Task.Factory.StartNew(() =>
            {
                throw new Exception("Task 1 Error!");
            });
            Task.Factory.StartNew(() =>
            {
                // 3.Try-Catch
                try
                {
                    throw new Exception("Error");
                }
                catch
                {
                    Console.WriteLine("Handled Exception\n");
                }

                throw new Exception("Task 2 Error!");
            });
            Thread.Sleep(100);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Read
            Console.ReadLine();

            // 4.Throw Exception
            throw new Exception("Error");
        }

        // 攔截Console Unhandled Exception
        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Unhandled UI Exception");
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("\n");
            Environment.Exit(1);
        }

        // 攔截Task Unhandled Exception
        static void UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine("Unobserved Task Exception");
            Console.WriteLine(e.Exception.ToString());
            Console.WriteLine("\n");
        }
    }
}
