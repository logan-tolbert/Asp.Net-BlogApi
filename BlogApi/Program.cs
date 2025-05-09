using BlogApi.Migrations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlite("Data Source=Data/blog.db"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
