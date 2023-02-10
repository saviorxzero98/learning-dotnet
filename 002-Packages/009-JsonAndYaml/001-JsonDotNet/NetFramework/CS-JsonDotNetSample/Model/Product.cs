using Newtonsoft.Json;
using System.Collections.Generic;

namespace CS_JsonDotNetSample.Model
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Unit { get; set; }
        public string ProductionSite { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Info { get; set; }
    }
}
