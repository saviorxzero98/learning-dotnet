﻿using BackgroundWorkSample.Extensions;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorkSample.BackgroundServices
{
    public class BackgroundJobQueue
    {
        public bool IsDisposed { get; private set; }
        private readonly ConcurrentQueue<BackgroundTaskContext> _taskQueue;
        private readonly SemaphoreSlim _signal;

        public BackgroundJobQueue()
        {
            _taskQueue = new ConcurrentQueue<BackgroundTaskContext>();
            _signal = new SemaphoreSlim(0);
        }


        /// <summary>
        /// 加入 Background Task
        /// </summary>
        /// <param name="task"></param>
        public void Enqueue(Func<CancellationToken, Task> task)
        {
            Enqueue(new BackgroundTaskContext(task));
        }

        /// <summary>
        /// 加入 Background Task
        /// </summary>
        /// <param name="task"></param>
        /// <param name="exception"></param>
        public void Enqueue(Func<CancellationToken, Task> task, Action<Exception> exception)
        {
            Enqueue(new BackgroundTaskContext(task, exception));
        }

        /// <summary>
        /// 加入 Background Task
        /// </summary>
        /// <param name="taskContext"></param>
        public void Enqueue(BackgroundTaskContext taskContext)
        {
            _taskQueue.Enqueue(taskContext);
            _signal.Release();
        }

        /// <summary>
        /// 取出 Background Task
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<BackgroundTaskContext> DequeueAsync(CancellationToken ct)
        {
            await _signal.WaitAsync(ct);
            _taskQueue.TryDequeue(out var ticket);

            return ticket;
        }

        /// <summary>
        /// 執行 Background Task
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var taskContext = await DequeueAsync(cancellationToken);

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

        /// <summary>
        /// 執行 Background Task (平行)
        /// </summary>
        /// <param name="threads"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ParallelExecuteAsync(int threads, CancellationToken cancellationToken)
        {
            ParallelOptions options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = (threads > 0) ? threads : Environment.ProcessorCount
            };

            await _taskQueue.ParallelForEachAsync(options, async (item) =>
            {
                var taskContext = await DequeueAsync(cancellationToken);

                try
                {
                    await taskContext.ExecuteAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    taskContext.OnException(e);
                }
            });
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                _signal.Dispose();
                IsDisposed = true;
            }
        }
    }

    public class BackgroundTaskContext
    {
        private readonly Func<CancellationToken, Task> _task;
        private readonly Action<Exception> _exception;


        public BackgroundTaskContext(Func<CancellationToken, Task> task)
        {
            _task = task;
            _exception = (exception) => { };
        }
        public BackgroundTaskContext(Func<CancellationToken, Task> task, Action<Exception> exception)
        {
            _task = task;
            _exception = exception;
        }

        public Task ExecuteAsync(CancellationToken ct)
        {
            return _task(ct);
        }

        public void OnException(Exception ex)
        {
            _exception(ex);
        }
    }
}
