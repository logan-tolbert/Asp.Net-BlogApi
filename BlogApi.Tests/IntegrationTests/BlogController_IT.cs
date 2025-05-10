using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace BlogApi.Tests.IntegrationTests;

public class BlogController_IT
    (WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetArticles_ReturnsOkResult()
    {
        // Act
        var response = await _client.GetAsync("api/blog/articles");

        // Assert
        Assert.True(response.IsSuccessStatusCode, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetArticlesById_ReturnsOkResult()
    {
        // Act
        var response = await _client.GetAsync("api/blog/articles/1");

        // Assert
        Assert.True(response.IsSuccessStatusCode, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateArticle_ReturnsOkResult()
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
        var response = await _client.PostAsJsonAsync("api/blog/articles", newArticle);

        // Assert
        Assert.True(response.IsSuccessStatusCode, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task UpdateArticle_ReturnsOkResult()
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
        var response = await _client.PutAsJsonAsync($"api/blog/articles/{updatedArticle.Id}", updatedArticle);

        // Assert
        Assert.True(response.IsSuccessStatusCode, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteArticle_ReturnsOkResult()
    {
        // Arrange
        var id = 1;

        // Act
        var response = await _client.DeleteAsync($"api/blog/articles/{id}");

        // Assert
        Assert.True(response.IsSuccessStatusCode, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

}
