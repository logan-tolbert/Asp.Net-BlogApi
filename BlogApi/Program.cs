using BlogApi.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlite("Data Source=DataAccess/blog.db"));

var app = builder.Build();

app.MapControllers();

app.Run();

public abstract partial class Program
{

}