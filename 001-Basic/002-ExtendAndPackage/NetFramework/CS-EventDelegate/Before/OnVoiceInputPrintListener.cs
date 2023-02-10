using EventDelegate.Listener;
using System;

namespace EventDelegate.Before
{
    public class OnVoiceInputPrintListener : IOnPrintListener
    {
        public bool PrintText(int id, string message)
        {
            Console.WriteLine("Sender ID : " + id + ", Speech : " + message);
            return true;
        }
    }
}
