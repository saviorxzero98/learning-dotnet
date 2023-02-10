using EventDelegate.Listener;

namespace EventDelegate.Sample01
{
    /// <summary>
    /// Sample 01
    /// </summary>
    public class OnPrintListener : IOnPrintListener
    {
        // Event Delegate
        public delegate bool PrintDelegate(int id, string message);

        // Event Handler
        public event PrintDelegate Type;

        // Implement
        public bool PrintText(int id, string message)
        {
            if (Type == null)
            {
                return false;
            }
            return Type.Invoke(id, message);
        }
    }
}
