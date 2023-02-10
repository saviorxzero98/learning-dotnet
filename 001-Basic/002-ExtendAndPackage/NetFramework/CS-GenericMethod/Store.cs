using GenericMethod.Item;

namespace GenericMethod
{
    public class Store
    {
        public enum ComputerName { MacBook, SurfaceBook, ChromeBook, Note7 }

        public static Computer Buy<T>(int price) where T : Computer
        {
            switch (typeof(T).Name)
            {
                case "MacBook":
                    return new MacBook(price);
                case "SurfaceBook":
                    return new SurfaceBook(price);
                case "ChromeBook":
                    return new ChromeBook(price);
                default:
                    return new Note7(price);
            }
        }

        public static Computer Buy(int price, ComputerName name)
        {
            switch (name)
            {
                case ComputerName.MacBook:
                    return new MacBook(price);
                case ComputerName.SurfaceBook:
                    return new SurfaceBook(price);
                case ComputerName.ChromeBook:
                    return new ChromeBook(price);
                default:
                    return new Note7(price);
            }
        }
    }
}
