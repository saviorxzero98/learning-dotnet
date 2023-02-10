using EventDelegate.Listener;
using System;

namespace EventDelegate.Before
{
    public class OnKeyboardInputPrintListener : IOnPrintListener
    {
        public bool PrintText(int id, string message)
        {
            Console.WriteLine("Sender ID : " + id + ", Key In : " + message);
            return true;
        }
    }
}
