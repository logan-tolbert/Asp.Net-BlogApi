using Blog.Api.Services;
using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();

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
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    options.AddPolicy("FrontendClientPolicy", builder =>
    {
        builder.WithOrigins("https://yourfrontend.com")
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
    app.UseExceptionHandler("/error");
    app.UseHsts();
    app.UseHttpsRedirection();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

if (app.Environment.IsProduction())
{
    app.UseCors("FrontendClientPolicy");
}
else
{
    app.UseCors("AllowAllOrigins");
}

app.UseAuthorization();

app.UseStatusCodePages(async statusCodeContext =>
{
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
        options.CustomCss = "* { font-family: 'Monaco'; }";
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