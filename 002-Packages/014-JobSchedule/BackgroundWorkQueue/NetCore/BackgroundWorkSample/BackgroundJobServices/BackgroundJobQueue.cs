using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundJobServices
{
    public class BackgroundJobQueue : IBackgroundJobQueue
    {
        public bool IsDisposed { get; private set; }
        private readonly ConcurrentQueue<IBackgroundJob> _jobQueue;
        private readonly SemaphoreSlim _signal;

        public BackgroundJobQueue()
        {
            _jobQueue = new ConcurrentQueue<IBackgroundJob>();
            _signal = new SemaphoreSlim(0);
        }

        /// <summary>
        /// Enqueue Background Job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public virtual void Enqueue(IBackgroundJob job)
        {
            _jobQueue.Enqueue(job);
            _signal.Release();
        }

        /// <summary>
        /// Enqueue Background Job
        /// </summary>
        /// <param name="job"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual Task EnqueueAsync(IBackgroundJob job, CancellationToken token = default)
        {
            _jobQueue.Enqueue(job);
            _signal.Release();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Dequeue Background Job
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<IBackgroundJob> DequeueAsync(CancellationToken token = default)
        {
            await _signal.WaitAsync(token);
            _jobQueue.TryDequeue(out var ticket);

            return ticket;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            if (!IsDisposed)
            {
                _signal.Dispose();
                IsDisposed = true;
            }
        }
    }
}
