using Hangfire;
using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.States;
using HangfireCustomAutoRetrySample.Exceptions;
using System;
using System.Linq;

namespace HangfireCustomAutoRetrySample.JobFilters
{
    public class CustomAutoRetryAttribute : JobFilterAttribute, IElectStateFilter
    {
        private readonly ILog _logger = LogProvider.For<CustomAutoRetryAttribute>();

        public const string ErrorRetryParameter = "ErrorRetryCount";
        public const string BusyRetryParameter = "BusyRetryCount";

        private enum RetryType
        {
            Error,
            Busy
        }

        private CustomAutoRetryOptions _retryOptions;
        private CustomAutoRetryOptions RetryOptions
        {
            get
            {
                if (_retryOptions == null)
                {
                    _retryOptions = new CustomAutoRetryOptions();
                }
                return _retryOptions;
            }
        }
        public int ErrorRetryAttempts
        {
            get
            {
                return RetryOptions.ErrorRetryAttempts;
            }
            set
            {
                RetryOptions.ErrorRetryAttempts = value;
            }
        }
        public int ErrorRetryDelayInSeconds
        {
            get
            {
                return RetryOptions.ErrorRetryDelayInSeconds;
            }
            set
            {
                RetryOptions.ErrorRetryDelayInSeconds = value;
            }
        }
        public int BusyRetryAttempts
        {
            get
            {
                return RetryOptions.BusyRetryAttempts;
            }
            set
            {
                RetryOptions.BusyRetryAttempts = value;
            }
        }
        public int BusyRetryDelayInSeconds
        {
            get
            {
                return RetryOptions.BusyRetryDelayInSeconds;
            }
            set
            {
                RetryOptions.BusyRetryDelayInSeconds = value;
            }
        }

        public bool LogEvents { get; set; }
        public AttemptsExceededAction OnAttemptsExceeded { get; set; }

        public CustomAutoRetryAttribute()
        {
            _retryOptions = new CustomAutoRetryOptions();
            LogEvents = true;
            OnAttemptsExceeded = AttemptsExceededAction.Fail;
            Order = 20;
        }

        public void OnStateElection(ElectStateContext context)
        {
            // 檢查是否執行成功          
            if (context.CandidateState is FailedState &&
                context.CandidateState != null)
            {
                var failedState = context.CandidateState as FailedState;

                // 取出重試的設定
                CustomAutoRetryOptions retryOptions;
                if (TryGetAutoRetryOptions(context, out CustomAutoRetryOptions customOptions))
                {
                    retryOptions = customOptions;
                }
                else
                {
                    retryOptions = new CustomAutoRetryOptions().SetErrorRetry(ErrorRetryAttempts)
                                                               .SetBusyRetry(BusyRetryAttempts, BusyRetryDelayInSeconds);
                }

                // 依據 Exception 類型做對應的處理
                if (failedState.Exception is BusyServiceException)
                {
                    HandleBusyRetry(context, failedState, retryOptions);
                }
                else
                {
                    HandleErrorRetry(context, failedState, retryOptions);
                }
            }
        }


        #region Retry Handle

        /// <summary>
        /// 閒置檢查
        /// </summary>
        /// <param name="context"></param>
        /// <param name="failedState"></param>
        /// <param name="retryOptions"></param>
        protected void HandleBusyRetry(ElectStateContext context, FailedState failedState, CustomAutoRetryOptions retryOptions)
        {
            // 取得相關參數
            int retryAttempt = context.GetJobParameter<int>(BusyRetryParameter) + 1;
            int maxAttempts = retryOptions.BusyRetryAttempts;
            int delayInSeconds = retryOptions.BusyRetryDelayInSeconds;

            if (retryAttempt <= maxAttempts)
            {
                // 執行次數+1
                context.SetJobParameter(BusyRetryParameter, retryAttempt);

                // 安排重試
                ScheduleAgainLater(context, RetryType.Busy, retryAttempt, maxAttempts,
                                   delayInSeconds, failedState);
            }
            else if (retryAttempt > maxAttempts && OnAttemptsExceeded == AttemptsExceededAction.Delete)
            {
                // 刪除
                TransitionToDeleted(context, RetryType.Busy, maxAttempts, failedState);
            }
            else
            {
                LogFaildToProcessTheJob(context, failedState);
            }
        }

        /// <summary>
        /// 錯誤重試
        /// </summary>
        /// <param name="context"></param>
        /// <param name="failedState"></param>
        /// <param name="retryOptions"></param>
        protected void HandleErrorRetry(ElectStateContext context, FailedState failedState, CustomAutoRetryOptions retryOptions)
        {
            // 取得相關參數
            int retryAttempt = context.GetJobParameter<int>(ErrorRetryParameter) + 1;
            int maxAttempts = retryOptions.ErrorRetryAttempts;
            int delayInSeconds = retryOptions.ErrorRetryDelayInSeconds;

            if (retryAttempt <= maxAttempts)
            {
                // 執行次數+1
                context.SetJobParameter(ErrorRetryParameter, retryAttempt);

                // 安排重試
                ScheduleAgainLater(context, RetryType.Error, retryAttempt, maxAttempts, delayInSeconds, failedState);
            }
            else if (retryAttempt > maxAttempts && OnAttemptsExceeded == AttemptsExceededAction.Delete)
            {
                // 刪除
                TransitionToDeleted(context, RetryType.Error, maxAttempts, failedState);
            }
            else
            {
                LogFaildToProcessTheJob(context, failedState);
            }
        }

        /// <summary>
        /// 記錄失敗的 Job 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="failedState"></param>
        protected void LogFaildToProcessTheJob(ElectStateContext context, FailedState failedState)
        {
            if (LogEvents)
            {
                string errorMessage = $"Failed to process the job '{context.BackgroundJob.Id}': an exception occurred.";
                _logger.ErrorException(errorMessage, failedState.Exception);
            }
        }

        #endregion


        #region Common Method

        /// <summary>
        /// 取出重試選項
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool TryGetAutoRetryOptions(ElectStateContext context, out CustomAutoRetryOptions options)
        {
            var args = context.BackgroundJob.Job.Args.ToList();

            foreach (var arg in args)
            {
                if (arg.GetType() == typeof(CustomAutoRetryOptions))
                {
                    options = new CustomAutoRetryOptions((CustomAutoRetryOptions)arg);
                    return true;
                }
            }

            options = new CustomAutoRetryOptions();
            return false;
        }

        /// <summary>
        /// 排定工作重試
        /// </summary>
        /// <param name="context"></param>
        /// <param name="retryAttempt"></param>
        /// <param name="maxAttempts"></param>
        /// <param name="delayInSeconds"></param>
        /// <param name="failedState"></param>
        private void ScheduleAgainLater(ElectStateContext context, RetryType retryType, int retryAttempt,
                                        int maxAttempts, int delayInSeconds, FailedState failedState)
        {
            var delay = TimeSpan.FromSeconds(delayInSeconds);

            string reason = GetRetryReason(retryType, retryAttempt, maxAttempts, failedState);
            if (delay == TimeSpan.Zero)
            {
                context.CandidateState = new EnqueuedState { Reason = reason };
            }
            else
            {
                context.CandidateState = new ScheduledState(delay) { Reason = reason };
            }

            if (LogEvents)
            {
                string warnMessage = $"Failed to process the job '{context.BackgroundJob.Id}': an exception occurred. [{retryType.ToString()}] Retry attempt {retryAttempt} of {maxAttempts} will be performed in {delay}.";
                _logger.WarnException(warnMessage, failedState.Exception);
            }
        }

        /// <summary>
        /// 刪除工作
        /// </summary>
        /// <param name="context"></param>
        /// <param name="maxAttempts"></param>
        /// <param name="failedState"></param>
        private void TransitionToDeleted(ElectStateContext context, RetryType retryType, int maxAttempts,
                                         FailedState failedState)
        {
            context.CandidateState = new DeletedState
            {
                Reason = (maxAttempts > 0)
                         ? "Exceeded the maximum number of retry attempts."
                         : "Retries were disabled for this job."
            };

            if (LogEvents)
            {
                string warnMessage = $"Failed to process the job '{context.BackgroundJob.Id}': an exception occured. [{retryType.ToString()}] Job was automatically deleted because the retry attempt count exceeded {maxAttempts}.";
                _logger.WarnException(warnMessage, failedState.Exception);
            }
        }

        /// <summary>
        /// 取得重試的理由
        /// </summary>
        /// <param name="retryType"></param>
        /// <param name="retryAttempt"></param>
        /// <param name="maxAttempts"></param>
        /// <param name="failedState"></param>
        /// <returns></returns>
        private string GetRetryReason(RetryType retryType, int retryAttempt, int maxAttempts, FailedState failedState)
        {
            const int maxMessageLength = 50;
            string exceptionMessage = failedState.Exception.Message;
            if (exceptionMessage.Length > maxMessageLength)
            {
                exceptionMessage = $"{exceptionMessage.Substring(0, maxMessageLength - 1)}…";
            }

            string reason = $"[{retryType.ToString()}] Retry attempt {retryAttempt} of {maxAttempts}: {exceptionMessage}";
            return reason;
        }

        #endregion
    }
}
