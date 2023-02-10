using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorkSample.BackgroundServices
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
                await _backgroundJobQueue.ExecuteAsync(cancellationToken);
            }
        }

        public override void Dispose()
        {
            _backgroundJobQueue.Dispose();
            base.Dispose();
        }
    }
}
