using BlogApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.DataAccess;

public class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>().HasData(
            new Article
            {
                Id = 1,
                Title = "Introduction to C#",
                Content = "This article covers the basics of C# programming language.",
                Author = "Logan Tolbert",
                Tags = "C#,Programming,Basics",
                CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc)
            },
            new Article
            {
                Id = 2,
                Title = "Understanding ASP.NET Core",
                Content = "A beginner's guide to building web APIs using ASP.NET Core.",
                Author = "Logan Tolbert",
                Tags = "ASP.NET Core,Web API",
                CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc)
            },
            new Article
            {
                Id = 3,
                Title = "Working with Entity Framework Core",
                Content = "Learn how to use EF Core for data access in .NET applications.",
                Author = "Logan Tolbert",
                Tags = "EF Core,Database,ORM",
                CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc)
            },
            new Article
            {
                Id = 4,
                Title = "Getting Started with Docker",
                Content = "An introduction to containerization using Docker.",
                Author = "Logan Tolbert",
                Tags = "Docker,DevOps,Containers",
                CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc)
            },
            new Article
            {
                Id = 5,
                Title = "Microservices Architecture Basics",
                Content = "Overview of designing and building microservices applications.",
                Author = "Logan Tolbert",
                Tags = "Microservices,Architecture,Cloud",
                CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc)
            },
            new Article
            {
                Id = 6,
                Title = "Serverless API with AWS Lambda",
                Content = "How to create and deploy serverless APIs using AWS Lambda and API Gateway.",
                Author = "Logan Tolbert",
                Tags = "AWS,Lambda,Serverless,API",
                CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }


}
