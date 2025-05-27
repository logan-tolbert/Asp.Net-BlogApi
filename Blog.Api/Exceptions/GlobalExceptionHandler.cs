using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Exceptions;

internal sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    /// <summary>
    /// Tries to handle the specified exception asynchronously within the ASP.NET Core pipeline.
    /// Implementations of this method can provide custom exception-handling logic for different scenarios.
    /// </summary>
    /// <param name="httpContext">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" /> for the request.</param>
    /// <param name="exception">The unhandled exception.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous read operation. The value of its <see cref="P:System.Threading.Tasks.ValueTask`1.Result" />
    /// property contains the result of the handling operation.
    /// <see langword="true" /> if the exception was handled successfully; otherwise <see langword="false" />.
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception switch
        {
            ApplicationException _ => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "An error occurred while processing your request.",
                Detail = exception.Message,
            }
        });
    }
}