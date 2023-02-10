using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDelegate.Listener
{
    public interface IOnPrintListener
    {
        bool PrintText(int id, string message);
    }
}
