using Hangfire.Client;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HangfireSample.JobFilters
{
    public class SingleJobAttribute : JobFilterAttribute, IClientFilter, IApplyStateFilter
    {
        protected readonly IConfiguration _configuration;
        public SingleJobAttribute(IServiceProvider service)
        {
            _configuration = service.GetService<IConfiguration>();
        }


        public void OnCreating(CreatingContext filterContext)
        {
            var connection = filterContext.Connection as JobStorageConnection;

            // We can't handle old storages
            if (connection == null)
            {
                return;
            }

            // We should run this filter only for background jobs based on 
            // recurring ones
            //if (!filterContext.Parameters.ContainsKey("RecurringJobId"))
            //{
            //    return;
            //}

            //var recurringJobId = filterContext.Parameters["RecurringJobId"] as string;

            // RecurringJobId is malformed. This should not happen, but anyway.
            //if (string.IsNullOrWhiteSpace(recurringJobId))
            //{
            //    return;
            //}

            //var running = connection.GetValueFromHash($"recurring-job:{recurringJobId}", "Running");
            //if ("yes".Equals(running, StringComparison.OrdinalIgnoreCase))
            //{
            //    filterContext.Canceled = true;
            //}

            if (TryGetSingleJobOptions(filterContext, out SingleJobOptions options) &&
                    options != null && !string.IsNullOrWhiteSpace(options.JobId))
            {
                var running = connection.GetValueFromHash(options.JobId, "Running");
                if ("yes".Equals(running, StringComparison.OrdinalIgnoreCase))
                {
                    filterContext.Canceled = true;
                }
            }   
        }

        public void OnCreated(CreatedContext filterContext)
        {

        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.NewState is EnqueuedState)
            {
                //var recurringJobId = SerializationHelper.Deserialize<string>(context.Connection.GetJobParameter(context.BackgroundJob.Id, "RecurringJobId"));
                //if (string.IsNullOrWhiteSpace(recurringJobId))
                //{
                //    return;
                //}
                //transaction.SetRangeInHash($"recurring-job:{recurringJobId}", new[] { new KeyValuePair<string, string>("Running", "yes") });

                
                if (TryGetSingleJobOptions(context, out SingleJobOptions options) &&
                    options != null && !string.IsNullOrWhiteSpace(options.JobId))
                {
                    transaction.SetRangeInHash(options.JobId, new[] { new KeyValuePair<string, string>("Running", "yes") });
                }
            }
            else if (context.NewState.IsFinal || context.NewState is FailedState)
            {
                //var recurringJobId = SerializationHelper.Deserialize<string>(context.Connection.GetJobParameter(context.BackgroundJob.Id, "RecurringJobId"));
                //if (string.IsNullOrWhiteSpace(recurringJobId))
                //{
                //    return;
                //}
                //transaction.SetRangeInHash($"recurring-job:{recurringJobId}", new[] { new KeyValuePair<string, string>("Running", "no") });

                if (TryGetSingleJobOptions(context, out SingleJobOptions options) &&
                    options != null && !string.IsNullOrWhiteSpace(options.JobId))
                {
                    transaction.SetRangeInHash(options.JobId, new[] { new KeyValuePair<string, string>("Running", "no") });
                }
            }
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {

        }


        
        private bool TryGetSingleJobOptions(ApplyStateContext context, out SingleJobOptions options)
        {
            var args = context.BackgroundJob.Job.Args.ToList();

            foreach (var arg in args)
            {
                if (arg != null && arg.GetType() == typeof(SingleJobOptions))
                {
                    options = new SingleJobOptions((SingleJobOptions)arg);
                    return true;
                }
            }

            options = new SingleJobOptions();
            return false;
        }
        private bool TryGetSingleJobOptions(CreatingContext context, out SingleJobOptions options)
        {
            var args = context.Job.Args.ToList();

            foreach (var arg in args)
            {
                if (arg != null && arg.GetType() == typeof(SingleJobOptions))
                {
                    options = new SingleJobOptions((SingleJobOptions)arg);
                    return true;
                }
            }

            options = new SingleJobOptions();
            return false;
        }
    }
}
