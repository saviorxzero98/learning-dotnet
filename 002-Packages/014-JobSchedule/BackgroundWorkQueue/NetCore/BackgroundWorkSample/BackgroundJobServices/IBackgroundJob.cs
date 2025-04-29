using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundJobServices
{
    public interface IBackgroundJob
    {
        /// <summary>
        /// Execute Job
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task ExecuteAsync(CancellationToken token = default);

        /// <summary>
        /// Exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task OnExceptionAsync(Exception exception, CancellationToken token = default);
    }
}
