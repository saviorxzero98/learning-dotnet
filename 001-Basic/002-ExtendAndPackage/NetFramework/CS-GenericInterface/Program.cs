using GenericInterface.Item;

namespace GenericInterface
{
    public class Program
    {
        static void Main(string[] args)
        {
            (new MacBook()).Boot();
            (new SurfaceBook()).Boot();
        }
    }
}
