using System;

namespace GenericInterface.Item
{
    public class MacBook : Computer<int>
    {
        private int _id = 123456;
        public int Id
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
            Console.WriteLine("Enter Mac OS, ID : " + Id);
        }
    }
}
