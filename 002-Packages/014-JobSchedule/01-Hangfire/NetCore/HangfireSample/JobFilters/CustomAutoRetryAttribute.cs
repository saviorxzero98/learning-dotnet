using Hangfire;
using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.States;
using Hangfire.Storage;
using HangfireSample.Exceptions;
using System;
using System.Linq;

namespace HangfireSample.JobFilters
{
    public class CustomAutoRetryAttribute : JobFilterAttribute, IElectStateFilter, IApplyStateFilter
    {
        private readonly ILog _logger = LogProvider.For<CustomAutoRetryAttribute>();

        public const string ErrorRetryParameter = "RetryCount";
        public const string IdleCheckRetryParameter = "IdleCheckRetryCount";

        private enum RetryType
        {
            Error,
            IdelCheck
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
        public int ErrorAttempts
        {
            get
            {
                return RetryOptions.ErrorAttempts;
            }
            set
            {
                RetryOptions.ErrorAttempts = value;
            }
        }
        public int ErrorDelayInSeconds
        {
            get
            {
                return RetryOptions.ErrorDelayInSeconds;
            }
        }
        public int IdelCheckAttempts
        {
            get
            {
                return RetryOptions.IdleCheckAttempts;
            }
            set
            {
                RetryOptions.IdleCheckAttempts = value;
            }
        }
        public int IdelChackDelayInSeconds
        {
            get
            {
                return RetryOptions.IdleCheckDelayInSeconds;
            }
            set
            {
                RetryOptions.IdleCheckDelayInSeconds = value;
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
            var failedState = context.CandidateState as FailedState;
            if (failedState == null)
            {
                return;
            }

            // 取出重試的設定
            CustomAutoRetryOptions retryOptions;
            if (TryGetAutoRetryOptions(context, out CustomAutoRetryOptions customOptions))
            {
                retryOptions = customOptions;
            }
            else
            {
                retryOptions = new CustomAutoRetryOptions().SetErrorRetry(ErrorAttempts)
                                                           .SetIdleCheckRetry(IdelCheckAttempts, IdelChackDelayInSeconds);
            }

            if (failedState.Exception is BusyBotException)
            {
                HandleIdelCheckRetry(context, failedState, retryOptions);
            }
            else
            {
                HandleErrorRetry(context, failedState, retryOptions);
            }           
        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.NewState is ScheduledState &&
                context.NewState.Reason != null &&
                context.NewState.Reason.StartsWith("Retry attempt"))
            {
                transaction.AddToSet("retries", context.BackgroundJob.Id);
            }
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.OldStateName == ScheduledState.StateName)
            {
                transaction.RemoveFromSet("retries", context.BackgroundJob.Id);
            }
        }



        #region Retry Handle

        /// <summary>
        /// 閒置檢查
        /// </summary>
        /// <param name="context"></param>
        /// <param name="failedState"></param>
        /// <param name="retryOptions"></param>
        protected void HandleIdelCheckRetry(ElectStateContext context, FailedState failedState, CustomAutoRetryOptions retryOptions)
        {
            // 取得相關參數
            int retryAttempt = context.GetJobParameter<int>(IdleCheckRetryParameter) + 1;
            int maxAttempts = retryOptions.IdleCheckAttempts;
            int delayInSeconds = retryOptions.IdleCheckDelayInSeconds;

            if (retryAttempt <= maxAttempts)
            {
                // 執行次數+1
                context.SetJobParameter(IdleCheckRetryParameter, retryAttempt);

                // 安排重試
                ScheduleAgainLater(context, RetryType.IdelCheck, retryAttempt, maxAttempts, 
                                   delayInSeconds, failedState);
            }
            else if (retryAttempt > maxAttempts && OnAttemptsExceeded == AttemptsExceededAction.Delete)
            {
                // 刪除
                TransitionToDeleted(context, RetryType.IdelCheck, maxAttempts, failedState);
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
            int maxAttempts = retryOptions.ErrorAttempts;
            int delayInSeconds = retryOptions.ErrorDelayInSeconds;

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
