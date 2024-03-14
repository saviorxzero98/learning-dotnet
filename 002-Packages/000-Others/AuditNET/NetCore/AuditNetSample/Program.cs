using Audit.Core;
using Audit.Core.Providers;
using AuditNetSample.Models;

namespace AuditNetSample
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoAuditLog();
        }

        static void DemoAuditLog()
        {
            Audit.Core.Configuration.Setup().UseInMemoryProvider();

            var user = new User() { Name = "123" };
            using (var s = new AuditScopeFactory().Create(new AuditScopeOptions()
            {
                CreationPolicy = EventCreationPolicy.InsertOnEnd,
                EventType = "TestUser",
                TargetGetter = () => user
            }))
            {
                user.Name = "abc";
            }

            var dp = (InMemoryDataProvider)Configuration.DataProvider;
            var event0 = dp.GetEvent(0);
            var allEvents = dp.GetAllEvents();
        }
    }
}
