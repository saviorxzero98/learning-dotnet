using System;

namespace WebApiSample.Services
{
    public class GuidGenerator : IIdentifierGenerator
    {
        public string Create()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
