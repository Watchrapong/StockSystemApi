using Microsoft.AspNetCore.Diagnostics;

namespace StockSystem.Api.Exception
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            if (exception is AppException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status200OK;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Status = "error",
                    Message = exception.Message ?? "มีบางอย่างไม่ถูกต้อง"
                });
                return await ValueTask.FromResult(true);
            }
            return await ValueTask.FromResult(false);
        }
    }
}