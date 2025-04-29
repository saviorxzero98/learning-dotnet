using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundJobServices
{
    public class BackgroundJobService : BackgroundService, IBackgroundJobService
    {
        private readonly IBackgroundJobQueue _jobQueue;

        public BackgroundJobService(IBackgroundJobQueue jobQueue)
        {
            _jobQueue = jobQueue;
        }

        /// <summary>
        /// Execute Background Job
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var job = await _jobQueue.DequeueAsync(cancellationToken);

                Task task = new Task(async () =>
                {
                    try
                    {
                        await job.ExecuteAsync(cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        await job.OnExceptionAsync(e, cancellationToken).ConfigureAwait(false);
                    }
                });
                task.Start();
            }
        }

        /// <summary>
        /// Close Service
        /// </summary>
        public override void Dispose()
        {
            _jobQueue.Dispose();
            base.Dispose();
        }
    }
}
