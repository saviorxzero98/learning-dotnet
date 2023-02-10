using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceSample.BackgroundJobs
{
    public class BackgroundJobService : BackgroundService
    {
        private readonly BackgroundJobQueue _backgroundJobQueue;

        public BackgroundJobService(BackgroundJobQueue jobQueue)
        {
            _backgroundJobQueue = jobQueue;
        }

        /// <summary>
        /// 執行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var taskContext = await _backgroundJobQueue.DequeueAsync(cancellationToken);

                Task task = new Task(async () =>
                {
                    try
                    {
                        await taskContext.ExecuteAsync(cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        taskContext.OnException(e);
                    }
                });
                task.Start();
            }
        }

        public override void Dispose()
        {
            _backgroundJobQueue.Dispose();
            base.Dispose();
        }
    }
}
