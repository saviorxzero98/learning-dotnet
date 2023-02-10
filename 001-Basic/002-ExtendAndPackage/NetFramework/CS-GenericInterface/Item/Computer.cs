using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericInterface.Item
{
    public interface Computer<T>
    {
        T Id { get; set; }
        void Boot();
    }
}
