using Blog.Api.Exceptions;
using Blog.Api.Services;
using Blog.DataAccess.Repository;
using DataAccess;
using Microsoft.AspNetCore.Http.Features;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method}:{context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.Add("requestId", context.HttpContext.TraceIdentifier);
        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.Add("traceId", activity?.Id);
    };
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Blog Platform API",
        Version = "v0.0.x",
        Description = "API for managing articles and blog content"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    options.AddPolicy("FrontendClientPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins("https://yourfrontend.com")
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .WithHeaders("Content-Type", "Authorization");
    });
});

builder.Services.AddDbContext<BlogDbContext>();

builder.Services.AddScoped<IArticleRepo, ArticleRepo>();
builder.Services.AddScoped<IArticleService, ArticleService>();

var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
    app.UseHsts();
    app.UseHttpsRedirection();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseCors(app.Environment.IsProduction() ? "FrontendClientPolicy" : "AllowAllOrigins");

app.UseAuthorization();

app.UseStatusCodePages();

if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("EnableScalarDocs"))
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });

    app.MapScalarApiReference(options =>
    {
        options.Title = "Blog Platform API";
        options.Theme = ScalarTheme.Laserwave;
        options.Favicon = "/favicon.svg";
        options.Layout = ScalarLayout.Modern;
        options.DarkMode = true;
        options.CustomCss = "* { font-family: 'Monaco', sans-serif; }";
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Fetch);
        options.WithDefaultHttpClient(ScalarTarget.Python, ScalarClient.Requests);
        options.WithDefaultHttpClient(ScalarTarget.Java, ScalarClient.OkHttp);
        options.WithDefaultHttpClient(ScalarTarget.Go, ScalarClient.Http);
        options.WithDefaultHttpClient(ScalarTarget.Ruby, ScalarClient.NetHttp);
    });
}

app.MapControllers();
app.Run();

//  *-- For testing --*
public abstract partial class Program
{

}