using WebApiSample.Services;

namespace WebApiTestSample.Services
{
    public class MockIdentifierGenerator : IIdentifierGenerator
    {
        public string Create()
        {
            return "This is Test";
        }
    }
}
