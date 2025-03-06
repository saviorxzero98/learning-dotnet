using MediatorPatternSample.Colleagues;
using MediatorPatternSample.Mediators;

namespace MediatorPatternSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var alice = new SmsAccount("alice");
            var jack = new EmailAccount("jack");
            var john = new EmailAccount("john");

            var mediator = new ChatRoomMessageMediator("admin");
            await mediator.JoinAsync(alice);
            await mediator.JoinAsync(jack);
            await mediator.JoinAsync(john);

            await mediator.BroadcastMessageAsync(new Message("大家好"));

            await mediator.SendPrivateMessageAsync("jack", new Message("哈嘍! jack"));
        }
    }
}
