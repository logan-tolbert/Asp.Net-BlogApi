using DataAccess;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Blog.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<BlogDbContext>();

builder.Services.AddScoped<IArticleRepo, ArticleRepo>();
builder.Services.AddScoped<IArticleService, ArticleService>();

var app = builder.Build();

app.MapControllers();

app.Run();

public abstract partial class Program
{

}