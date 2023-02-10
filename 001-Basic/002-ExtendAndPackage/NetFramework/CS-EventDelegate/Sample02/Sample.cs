using System;

namespace EventDelegate.Sample02
{
    public class Sample
    {
        public const string Name = "Sample 02";

        public void RunSample()
        {
            Console.WriteLine("Run {0}", Name);

            // New Class
            Sample02.OnPrintListener keyboardInput = new Sample02.OnPrintListener();
            Sample02.OnPrintListener voiceInput = new Sample02.OnPrintListener();

            // Assign Event Handler
            keyboardInput.Type += delegate (Sample02.OnPrintListener.OnTypeEventArgs args)
            {
                Console.WriteLine("Sender ID : " + args.Id + ", Key In : " + args.Message);
                return true;
            };

            voiceInput.Type += delegate (Sample02.OnPrintListener.OnTypeEventArgs args)
            {
                Console.WriteLine("Sender ID : " + args.Id + ", Speech : " + args.Message);
                return true;
            };
            voiceInput.Type += NewViewClick;

            // Call Method
            bool keyboardResult = keyboardInput.PrintText(0, "Hello Keyboard");
            bool voiceResult = voiceInput.PrintText(1, "Hello Voice");

            Console.WriteLine("\n========================================\n");
        }

        public bool NewViewClick(Sample02.OnPrintListener.OnTypeEventArgs args)
        {
            Console.WriteLine("Sender ID : " + args.Id + ", Repeat Speech : " + args.Message);
            return true;
        }
    }
}
