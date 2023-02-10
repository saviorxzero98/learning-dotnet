using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TaiwanCalendarService.Models;

namespace TaiwanCalendarService.Providers
{
    /// <summary>
    /// 使用到台北市政府資料開放平台的資料
    /// http://data.taipei/opendata/datalist/datasetMeta?oid=9cfba4c6-3caa-48ff-a926-f903c74c5736
    /// </summary>
    public class TaipeiOpenCalendarProvider : AbstractOpenCalendarProvider
    {
        public const string Host = "http://data.taipei/opendata/datalist/";
        public const string Method = "apiAccess?scope=resourceAquire&rid=c9b60d40-cb14-4796-9a6f-276fc1525128";

        public TaipeiOpenCalendarProvider()
        {
            _calendar = LoadCalendar();
            _lastUpdateDateTime = DateTime.Today;
        }

        public override List<OpenCalendarModel> LoadCalendar()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Set Host URL
                    client.BaseAddress = new Uri(Host);

                    // Http Get
                    HttpResponseMessage responseMessage = client.GetAsync(Method).Result;

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException(responseMessage.StatusCode.ToString());
                    }
                    var responseText = responseMessage.Content.ReadAsStringAsync().Result;

                    JObject result = JsonConvert.DeserializeObject<JObject>(responseText);
                    string jsonString = result["result"]["results"].ToString();

                    List<OpenCalendarModel> openCalendars = JsonConvert.DeserializeObject<List<OpenCalendarModel>>(jsonString);
                    return openCalendars;
                }
                catch
                {
                    return new List<OpenCalendarModel>();
                }
            }
        }
    }
}
