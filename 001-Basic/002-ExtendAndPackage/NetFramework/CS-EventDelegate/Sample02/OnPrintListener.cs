using EventDelegate.Listener;
using System;

namespace EventDelegate.Sample02
{
    public class OnPrintListener : IOnPrintListener
    {
        // Event Delegate
        public delegate bool TypeDelegate(OnTypeEventArgs args);

        // Event Handler
        public event TypeDelegate Type;

        // Implement Interface
        public bool PrintText(int id, string message)
        {
            if (Type == null)
            {
                return false;
            }
            return Type.Invoke(new OnTypeEventArgs(id, message));
        }

        // Event Args
        public class OnTypeEventArgs : EventArgs
        {
            public OnTypeEventArgs(int id, string message)
            {
                Id = id;
                Message = message;
            }

            public int Id { get; }

            public string Message { get; }
        }
    }
}
