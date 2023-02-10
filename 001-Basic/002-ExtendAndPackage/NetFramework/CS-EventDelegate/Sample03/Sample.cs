using System;

namespace EventDelegate.Sample03
{
    public class Sample
    {
        public const string Name = "Sample 03";

        public void RunSample()
        {
            Console.WriteLine("Run {0}", Name);

            // New Class
            Sample03.OnPrintListener keyboardInput = new Sample03.OnPrintListener();
            Sample03.OnPrintListener voiceInput = new Sample03.OnPrintListener();

            // Assign Event Handler
            keyboardInput.Type += delegate (object sender, Sample03.OnPrintListener.OnTypeEventArgs args)
            {
                Console.WriteLine("Sender ID : " + args.Id + ", Key In : " + args.Message);
            };

            voiceInput.Type += delegate (object sender, Sample03.OnPrintListener.OnTypeEventArgs args)
            {
                Console.WriteLine("Sender ID : " + args.Id + ", Speech : " + args.Message);
            };
            voiceInput.Type += NewViewClick;

            // Call Method
            bool keyboardResult = keyboardInput.PrintText(0, "Hello Keyboard");
            bool voiceResult = voiceInput.PrintText(1, "Hello Voice");

            Console.WriteLine("\n========================================\n");
        }

        public void NewViewClick(object sender, Sample03.OnPrintListener.OnTypeEventArgs args)
        {
            Console.WriteLine("Sender ID : " + args.Id + ", Repeat Speech : " + args.Message);
        }
    }
}
