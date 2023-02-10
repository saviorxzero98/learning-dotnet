using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CatchUnhandledExceptions
{
    [Activity(Label = "CatchUnhandledExceptions", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const string TAG = "CatchUnhandledExceptionsSample";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // 1.註冊Unhandled Exception Handler
            AndroidEnvironment.UnhandledExceptionRaiser += UnhandledExceptionRaiser;
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            TaskScheduler.UnobservedTaskException += UnobservedTaskException;
            Java.Lang.Thread.DefaultUncaughtExceptionHandler = new JavaUncaughtExceptionHandler();

            // 2.新增2個Thread
            Task.Factory.StartNew(() =>
            {
                throw new System.Exception("Task 1 Error!");
            });

            Task.Factory.StartNew(() =>
            {
                throw new Java.Lang.Exception("Task 2 Error!");
            });
            Thread.Sleep(100);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Try-Catch
            try
            {
                throw new System.Exception("Catch Exception Error");
            }
            catch (Exception e)
            {
                string message = "Handled Exception\n";
                Console.WriteLine(message);
                Log.Error(TAG, message);
            }

            // 4.Throw Exception
            throw new System.Exception("Error");
        }

        // 攔截Android EnvironmentUnhandled Unhandled Exception (Mono Runtime)
        private void UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            string message = e.Exception.ToString();
            Console.WriteLine(message);
            Log.Error(TAG, message);
            e.Handled = true;
        }

        // 攔截.Net (Console) Unhandled Exception (Mono Runtime)
        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string message = e.ExceptionObject.ToString();
            Console.WriteLine(message);
            Log.Error(TAG, message);
        }

        // 攔截Task (.Net) & Thread (Java) Unhandled Exception (Mono Runtime)
        private void UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            string message = e.Exception.ToString();
            Console.WriteLine(message);
            Log.Error(TAG, message);
        }

        // 攔截 Unhandled Exception (Java Runtime)
        private class JavaUncaughtExceptionHandler : Java.Lang.Object, Java.Lang.Thread.IUncaughtExceptionHandler
        {
            public void UncaughtException(Java.Lang.Thread thread, Java.Lang.Throwable e)
            {
                string message = $"{thread.Name} - {e.ToString()}";
                Console.WriteLine(message);
                Log.Error(TAG, message);
            }
        }
    }
}

