using BarcloudTask.DataBase.Models;
using BarcloudTask.Service.Interface;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.States;

namespace BarcloudTask.API.Extensions
{
    public class HangfireExceptionFilter(ILogger<HangfireExceptionFilter> _logger, ICommonService _common) : JobFilterAttribute, IServerFilter, IElectStateFilter
    {

        public void OnPerforming(PerformingContext context)
        {
        }

        public void OnPerformed(PerformedContext context)
        {
        }

        public void OnStateElection(ElectStateContext context)
        {
            if (context.CandidateState is FailedState failedState)
            {
                var exception = failedState.Exception;
                _logger.LogError(exception, "Job {JobId} failed: {ExceptionMessage}", context.BackgroundJob.Id, exception.Message);
                _common.AddError(new ErrorsLog
                {
                    Function = "HangFire",
                    Message = exception.Message,
                });
            }
        }
    }
}
