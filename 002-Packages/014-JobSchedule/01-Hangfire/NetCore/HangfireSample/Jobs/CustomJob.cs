using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using HangfireSample.JobFilters;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireSample.Jobs
{
    public class CustomJob
    {
        [JobDisplayName("{0}")]
        public Task ExecuteJobAsync(string jobName, SingleJobOptions jobOptions, PerformContext context, 
                                    IJobCancellationToken cancellationToken)
        {
            var shutdownToken = (cancellationToken != null) ? cancellationToken.ShutdownToken : default(CancellationToken);

            using (var registration = RegisterCancellationCallback(default(CancellationToken)))
            {
                context.WriteLine("Start");

                Thread.Sleep(20000);

                context.WriteLine("Complete");

                return Task.CompletedTask;
            }
        }

        public CancellationTokenRegistration RegisterCancellationCallback(CancellationToken cancellationToken)
        {
            var registration = default(CancellationTokenRegistration);
            if (cancellationToken != null)
            {
                registration = cancellationToken.Register(() =>
                {

                });
            }
            return registration;
        }
    }
}
