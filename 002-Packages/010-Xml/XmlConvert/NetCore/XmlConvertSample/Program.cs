using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XmlConvertSample.Models;

namespace XmlConvertSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new UserProfile()
            {
                Id = 1,
                Name = "Ace",
                Detial = new UserProfileDetial()
                {
                    Height = 168,
                    Weight = 66,
                    Birth = DateTime.Parse("1912-03-03 08:00"),
                    Emails = new List<string>()
                },
                IsBlocked = false,
                CreateDate = DateTime.Parse("1912-03-03 08:00")
            };

            var dictionary = new Dictionary<string, string>()
            {
                { "A", "AAA" },
                { "B", "BBB" }
            };
            
            Convert(user);
            Convert(JToken.FromObject(user));
            Convert(dictionary);
        }

        static void Convert<T>(T data)
        {
            var xml = XmlConverter.SerializeObject(data, new XmlWriterSettings()
            {
                Encoding = Encoding.UTF8,
                Indent = true
            });
            Console.WriteLine(xml);

            T newData = XmlConverter.DeserializeObject<T>(xml);
            JToken json = XmlConverter.DeserializeObject(xml);
        }
    }
}
