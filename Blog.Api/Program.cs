using DataAccess;
using DataAccess.Repository;
using BlogApi.Services;
using Microsoft.EntityFrameworkCore;

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