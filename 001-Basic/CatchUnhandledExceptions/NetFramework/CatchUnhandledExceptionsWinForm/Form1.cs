using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatchUnhandledExceptionsWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 4.Throw Exception
            throw new Exception("UI Error");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 5.新增2個Thread
            Task.Factory.StartNew(() =>
            {
                throw new Exception("Task 1 Error");
            });

            Task.Factory.StartNew(() =>
            {
                throw new Exception("Task 2 Error");
            });
            Thread.Sleep(100);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
