using System.Net;
using System.Net.Http.Json;
using BlogApi.Tests.TestHelpers;

namespace Blog.Api.Tests.IntegrationTests;

public class ItBlogControllerTests
    (CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    // *-- Create --*
    [Fact]
    public async Task CreateAsync_WithValidArticle_Returns201Created()
    {
        // Arrange
        var newArticle = new
        {
            Title = "Introduction to C#",
            Content = "This article covers the basics of C# programming language.",
            Author = "Logan Tolbert",
            Tags = "C#,Programming,Basics"
        };

        // Act
        var response = await _client.PostAsJsonAsync("api/v0/articles/", newArticle);
        
        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.Created, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    // *-- Read --*
    [Fact]
    public async Task GetByIdAsync_WithExistingId_Returns200OK()
    {
        // Arrange
        const int id = 1;
        // Act
        var response = await _client.GetAsync($"api/v0/articles/{id}");

        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.OK, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetAsync_WhenCalled_Returns200OK()
    {
        // Act
        var response = await _client.GetAsync("api/v0/articles");

        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.OK, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    // *-- Update --
    [Fact]
    public async Task UpdateAsync_WithValidArticle_Returns204NoContent()
    {
        // Arrange
        var updatedArticle = new
        {   
            Id = 1,
            Title = "Introduction to C#",
            Content = "This article covers the basics of C# programming language.",
            Author = "Logan Tolbert",
            Tags = "C#,Programming,Basics"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"api/v0/articles/{updatedArticle.Id}", updatedArticle);

        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.NoContent, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    // *-- Delete --
    [Fact]
    public async Task DeleteAsync_WithExistingId_Returns204NoContent()
    {
        // Arrange
        var id = 1;

        // Act
        var response = await _client.DeleteAsync($"api/v0/articles/{id}");

        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.NoContent, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

}
