using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundJobServices
{
    public interface IBackgroundJobQueue : IDisposable
    {
        /// <summary>
        /// Enqueue Background Job
        /// </summary>
        /// <param name="job"></param>
        void Enqueue(IBackgroundJob job);

        /// <summary>
        /// Enqueue Background Job
        /// </summary>
        /// <param name="job"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task EnqueueAsync(IBackgroundJob job, CancellationToken token = default);

        /// <summary>
        /// Dequeue Background Job
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IBackgroundJob> DequeueAsync(CancellationToken token = default);
    }
}
