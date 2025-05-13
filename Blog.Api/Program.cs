using DataAccess;
using DataAccess.Repository;
using Blog.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();

// Database
builder.Services.AddDbContext<BlogDbContext>();

builder.Services.AddScoped<IArticleRepo, ArticleRepo>();
builder.Services.AddScoped<IArticleService, ArticleService>();

var app = builder.Build();

app.UseExceptionHandler();

// Configures UseStatusCodePages to re-execute the pipeline and generate Problem Details
app.UseStatusCodePages(async statusCodeContext =>
{
    // TODO: Refactor Problem Details setup into a dedicated ProblemDetailsService for better organization.
    var problemDetailsService = statusCodeContext.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

    var httpContext = statusCodeContext.HttpContext;
    var request = httpContext.Request;
    var response = httpContext.Response;

    if (!response.HasStarted && response.StatusCode >= 400)
    {
        await problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = new ProblemDetails
            {
                Status = response.StatusCode,
                Title = ReasonPhrases.GetReasonPhrase(response.StatusCode),
                Instance = request.Path
            }
        });
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();


//  *-- For testing --*
public abstract partial class Program
{

}