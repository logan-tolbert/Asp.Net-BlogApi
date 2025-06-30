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

    [Fact]
    public async Task CreateAsync_WithInvalidArticle_Returns400BadRequest()
    {
        // Arrange
        var invalidArticle = new
        {
            Content = "This article covers the basics of C# programming language.",
            Tags = "C#,Programming,Basics"
        };
        // Act
        var response = await _client.PostAsJsonAsync("api/v0/articles/", invalidArticle);

        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.BadRequest, "Expected internal server error status code.");
    }

    // TODO: Implement CreateAsync_WhenServiceFails_Returns500InternalServerError
    //[Fact]
    //public async Task CreateAsync_WhenServiceFails_Returns500InternalServerError(){}
  
    
    // *-- Read --*
    [Fact]
    public async Task GetAsync_WithExistingId_Returns200OK()
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
    public async Task GetAllAsync_WhenCalled_Returns200OK()
    {
        // Act
        var response = await _client.GetAsync("api/v0/articles");

        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.OK, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    // TODO: Implement GetAllAsync_WhenServiceFails_Returns500InternalServerError
    //[Fact]
    //public async Task GetAllAsync_WWhenServiceFails_Returns500InternalServerError(){}

    [Fact]
    public async Task GetAllAsync_WithPagination_ReturnsPagedArticles()
    {
        // Arrange
        var page = 1;
        var pageSize = 10;

        // Act
        var response = await _client.GetAsync($"api/v0/articles?page={page}&pageSize={pageSize}");

        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.OK, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetAllAsync_WithTagFilter_ReturnsFilteredArticles()
    {
        // Arrange
        var tag = "C#";
        // Act
        var response = await _client.GetAsync($"api/v0/articles?tag={tag}");
        // Assert
        Assert.True(response.StatusCode == HttpStatusCode.OK, "Expected a successful status code.");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetAllAsync_WithDateRange_ReturnsFilteredArticles()
    {
        // Arrange
        var startDate = "2023-01-01";
        var endDate = "2023-12-31";
        // Act
        var response = await _client.GetAsync($"api/v0/articles?startDate={startDate}&endDate={endDate}");
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
