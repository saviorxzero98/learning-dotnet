using MediatR;

namespace MediatRSample.Events
{
    public class AddDemoEvent : INotification
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
