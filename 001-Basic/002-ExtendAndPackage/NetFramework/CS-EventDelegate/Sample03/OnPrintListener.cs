using EventDelegate.Listener;
using System;

namespace EventDelegate.Sample03
{
    public class OnPrintListener : IOnPrintListener
    {
        // Event Delegate
        public EventHandler<OnTypeEventArgs> Type { get; set; }

        // Implement Interface
        public bool PrintText(int id, string message)
        {
            if (Type == null)
            {
                return false;
            }
            Type.Invoke(this, new OnTypeEventArgs(id, message));
            return true;
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
