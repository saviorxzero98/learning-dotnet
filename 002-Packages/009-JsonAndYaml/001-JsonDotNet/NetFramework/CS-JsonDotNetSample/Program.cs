using CS_JsonDotNetSample.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace CS_JsonDotNetSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertSample_6();
            //ConvertSample_1();  // Object和Json String之間的轉換
            //ConvertSample_2();  // Object、JObject和Json String之間的轉換
            //ConvertSample_3();  // DataTable和Json String之間的轉換
            //ConvertSample_4();
            //ConvertSample_5();
        }

        /// <summary>
        /// 1. Object -> Json String
        /// 2. Json String -> Object
        /// </summary>
        static void ConvertSample_1()
        {
            Cart cart = CreateCart2("Shop Cart.");

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            };

            // 1. Object -> Json String
            string cartJson = JsonConvert.SerializeObject(cart, settings);

            // 2. Json String -> Object
            Cart newCart = JsonConvert.DeserializeObject<Cart>(cartJson);
        }

        /// <summary>
        /// 1. Object -> JObject
        /// 2. JObject -> Json string
        /// 3. Json string -> JObject
        /// 4. Json string -> Object
        /// </summary>
        static void ConvertSample_2()
        {
            Cart cart = CreateCart("Shop Cart.");

            // 1. Object -> JObject
            JObject cartJObject = JObject.FromObject(cart);

            // 2. JObject -> Json string
            string cartJson = cartJObject.ToString();
            

            // 3. Json string -> JObject
            JObject newCartJObject = JObject.Parse(cartJson);

            // 4. Json string -> Object
            Cart newCart = JsonConvert.DeserializeObject<Cart>(cartJson);
        }

        /// <summary>
        /// 1. List Object -> Json String
        /// 2. Json -> DataTime
        /// 3. DataTime -> Json String
        /// </summary>
        static void ConvertSample_3()
        {
            Cart cart = CreateCart("Shop Cart.");

            // 1. List Object -> Json String
            string cartJson = JsonConvert.SerializeObject(cart.Products);

            // 2. Json String -> DataTime
            DataTable table = JsonConvert.DeserializeObject<DataTable>(cartJson);

            // 3. DataTime -> Json
            string newCartJson = JsonConvert.SerializeObject(table);
        }

        static void ConvertSample_4()
        {
            Cart cart = CreateCart("SShop Cart.");

            // 1. Object -> XML
            string cartXml = XmlConvert.SerializeObject(cart);

            // 2. XML -> Object
            Cart newCart = XmlConvert.DeserializeObject<Cart>(cartXml);
        }

        static void ConvertSample_5()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("AA.BB", 5);
            dict.Add("BB", "AA");
            dict.Add("CC", 2.2);

            // 1. List Object -> Json String
            string cartJson = JsonConvert.SerializeObject(dict);
        }

        static List<Product> ConvertSample_6()
        {
            string json = "[{ \"Name\":\"Apple\", \"Price\": \"50\", \"PlaceOfOrigin\": \"Aomori\", \"Rating\": 5 }, { \"Name\":\"PineApple\", \"Price\": \"100\", \"PlaceOfOrigin\": \"Tashu\", \"Rating\": 5}]";

            var products = JsonConvert.DeserializeObject<List<Product>>(json);
            return products;
        }

        static Cart CreateCart(string cartName)
        {
            Cart cart = new Cart();
            cart.Name = cartName;
            cart.Products.Add(new Product() { Name = "Apple.S", Unit = 12, Price = 2.5 });
            cart.Products.Add(new Product() { Name = "ApplePine.S", Unit = 2, Price = 6 });
            cart.Products.Add(new Product() { Name = "Pen.S", Unit = 10, Price = 0.5 });
            return cart;
        }

        static Cart CreateCart2(string cartName)
        {
            Cart cart = new Cart();
            cart.Name = cartName;
            cart.Products.Add(new Product() { Name = "Apple.S" });
            cart.Products.Add(new Product() { Name = "ApplePine.S" });
            cart.Products.Add(new Product() { Name = "Pen.S" });
            return cart;
        }
    }
}
