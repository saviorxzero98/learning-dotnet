using EventDelegate.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDelegate.Sample01
{
    public class Sample
    {
        public const string Name = "Sample 01";

        public void RunSample()
        {
            Console.WriteLine("Run {0}", Name);

            // New Class
            Sample01.OnPrintListener keyboardInput = new Sample01.OnPrintListener();
            Sample01.OnPrintListener voiceInput = new Sample01.OnPrintListener();

            // Assign Event Handler
            keyboardInput.Type += delegate (int id, string message)
            {
                Console.WriteLine("Sender ID : " + id + ", Key In : " + message);
                return true;
            };

            voiceInput.Type += delegate (int id, string message)
            {
                Console.WriteLine("Sender ID : " + id + ", Speech : " + message);
                return true;
            };
            voiceInput.Type += NewViewClick;

            // Call Method
            bool keyboardResult = keyboardInput.PrintText(0, "Hello Keyboard");
            bool voiceResult = voiceInput.PrintText(1, "Hello Voice");

            Console.WriteLine("\n========================================\n");
        }

        public bool NewViewClick(int id, string message)
        {
            Console.WriteLine("Sender ID : " + id + ", Repeat Speech : " + message);
            return true;
        }
    }
}
