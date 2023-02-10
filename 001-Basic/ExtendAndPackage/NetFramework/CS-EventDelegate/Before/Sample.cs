using EventDelegate.Listener;
using System;

namespace EventDelegate.Before
{
    public class Sample
    {
        public const string Name = "Before";

        public void RunSample()
        {
            Console.WriteLine("Run {0}", Name);

            // New Class
            IOnPrintListener keyboardInput = new Before.OnKeyboardInputPrintListener();
            IOnPrintListener voiceInput = new Before.OnVoiceInputPrintListener();

            // Call Method
            bool keyboardResult = keyboardInput.PrintText(0, "Hello Keyboard");
            bool voiceResult = voiceInput.PrintText(1, "Hello Voice");

            Console.WriteLine("\n========================================\n");
        }


    }
}
