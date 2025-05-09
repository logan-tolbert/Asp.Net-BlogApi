using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Migrations;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    DbSet<Article> Articles { get; set; }


}
