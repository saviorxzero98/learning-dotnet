using Newtonsoft.Json;

namespace TaiwanCalendarService.Models
{
    public class OpenCalendarModel
    {
        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "isHoliday")]
        public string IsHoliday { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
