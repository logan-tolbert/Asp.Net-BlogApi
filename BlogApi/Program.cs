using BlogApi.DataAccess;
using BlogApi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlite("Data Source=DataAccess/blog.db"));

builder.Services.AddScoped<IArticleRepo, ArticleRepo>();

var app = builder.Build();

app.MapControllers();

app.Run();

public abstract partial class Program
{

}