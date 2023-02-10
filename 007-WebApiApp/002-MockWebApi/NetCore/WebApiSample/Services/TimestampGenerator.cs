using System;

namespace WebApiSample.Services
{
    public class TimestampGenerator : IIdentifierGenerator
    {
        public string Create()
        {
            return DateTime.Now.Ticks.ToString();
        }
    }
}
