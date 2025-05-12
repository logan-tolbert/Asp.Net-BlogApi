using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApi.Tests.TestHelpers;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext configuration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<BlogDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add the in-memory database using DbContextHelper
            services.AddDbContext<BlogDbContext>(async options =>
            {
                var context = await DbContextHelper.ConfigureTestContext();
                options.UseSqlite(context.Database.GetDbConnection());
            });

            // Ensure the database is seeded
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
            db.Database.EnsureCreated();
        });
    }
}
