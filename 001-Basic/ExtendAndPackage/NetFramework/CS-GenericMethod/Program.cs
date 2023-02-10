using GenericMethod.Item;

namespace GenericMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Computer macBook = Store.Buy<MacBook>(50000);
            macBook.Boot();

            Computer surfaceBook = Store.Buy<SurfaceBook>(50000);
            surfaceBook.Boot();

            Computer chromeBook = Store.Buy<ChromeBook>(20000);
            chromeBook.Boot();

            Computer note7 = Store.Buy<Note7>(10000);
            note7.Boot();
        }
    }
}
