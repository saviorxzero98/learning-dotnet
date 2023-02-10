using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericInterface.Item
{
    public class SurfaceBook : Computer<string>
    {
        private string _id = "A00000000";
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public void Boot()
        {
            Console.WriteLine("Enter Windows, ID : " + Id);
        }
    }
}
