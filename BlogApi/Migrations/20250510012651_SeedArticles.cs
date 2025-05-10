using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "id", "author", "content", "created_at", "tags", "title", "updated_at" },
                values: new object[,]
                {
                    { 1, "Logan Tolbert", "This article covers the basics of C# programming language.", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), "C#,Programming,Basics", "Introduction to C#", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "Logan Tolbert", "A beginner's guide to building web APIs using ASP.NET Core.", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ASP.NET Core,Web API", "Understanding ASP.NET Core", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "Logan Tolbert", "Learn how to use EF Core for data access in .NET applications.", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), "EF Core,Database,ORM", "Working with Entity Framework Core", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "Logan Tolbert", "An introduction to containerization using Docker.", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Docker,DevOps,Containers", "Getting Started with Docker", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "Logan Tolbert", "Overview of designing and building microservices applications.", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Microservices,Architecture,Cloud", "Microservices Architecture Basics", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, "Logan Tolbert", "How to create and deploy serverless APIs using AWS Lambda and API Gateway.", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc), "AWS,Lambda,Serverless,API", "Serverless API with AWS Lambda", new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "id",
                keyValue: 6);
        }
    }
}
