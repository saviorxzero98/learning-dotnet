using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TaiwanCalendarService.Models;

namespace TaiwanCalendarService.Providers
{
    /// <summary>
    /// 使用到新北市政府資料開放平台的資料
    /// http://data.ntpc.gov.tw/od/detail?oid=308DCD75-6434-45BC-A95F-584DA4FED251
    /// </summary>
    public class NewTaipeiOpenCalendarProvider : AbstractOpenCalendarProvider
    {
        public const string Host = "http://data.ntpc.gov.tw/od/data/api/";
        public const string Method = "7B7A8FD9-2722-4F17-B515-849E00073865;jsessionid=A4D8A5B86CF88F83718D1FDD93AAD2A9?$format=json";

        public NewTaipeiOpenCalendarProvider()
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

                    List<OpenCalendarModel> openCalendars = JsonConvert.DeserializeObject<List<OpenCalendarModel>>(responseText);

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
