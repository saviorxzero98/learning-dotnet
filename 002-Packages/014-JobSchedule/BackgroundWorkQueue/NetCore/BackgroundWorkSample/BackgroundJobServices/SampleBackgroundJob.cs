using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundJobServices
{
    public class SampleBackgroundJob : IBackgroundJob
    {
        private readonly Func<CancellationToken, Task> _executeFunc;
        private readonly Func<Exception, CancellationToken, Task> _onExceptionFunc;

        public SampleBackgroundJob(
            Func<CancellationToken, Task> executeFunc)
        {
            _executeFunc = (executeFunc != null) ? executeFunc : (token) => Task.CompletedTask;
            _onExceptionFunc = (exception, token) => Task.CompletedTask;
        }
        public SampleBackgroundJob(
            Func<CancellationToken, Task> executeFunc,
            Func<Exception, CancellationToken, Task> onExceptionFunc)
        {
            _executeFunc = (executeFunc != null) ? executeFunc : (token) => Task.CompletedTask;
            _onExceptionFunc = (onExceptionFunc != null) ? onExceptionFunc : (exception, token) => Task.CompletedTask;
        }

        /// <summary>
        /// Execute Job
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual Task ExecuteAsync(CancellationToken token = default)
        {
            return _executeFunc(token);
        }

        /// <summary>
        /// Exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual Task OnExceptionAsync(Exception exception, CancellationToken token = default)
        {
            return _onExceptionFunc(exception, token);
        }
    }
}
