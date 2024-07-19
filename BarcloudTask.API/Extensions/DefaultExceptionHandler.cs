using BarcloudTask.DataBase.Models;
using BarcloudTask.Service.Interface;
using Microsoft.AspNetCore.Diagnostics;

namespace BarcloudTask.API.Extensions;

public class DefaultExceptionHandler(ILogger<DefaultExceptionHandler> _logger, ICommonService _common) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception.ToString());
        await _common.AddError(new ErrorsLog
        {
            Function = "ErrorHandler",
            Message = exception.ToString(),
        }).ConfigureAwait(false);
        await httpContext.Response.WriteAsJsonAsync(exception, cancellationToken);

        return true;
    }
}
