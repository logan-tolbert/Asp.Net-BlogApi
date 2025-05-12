using DataAccess.Entities;
using DataAccess.Repository;
using BlogApi.DTOs;
using BlogApi.Services;
using Moq;
using Blog.Api.Services;

namespace BlogApi.Tests.UnitTests;

public class ArticleServiceTests
{
    // TODO: Testing - Reduce code duplication. Cleaner more efficient test data genearation
    private readonly IArticleService _service;
    private readonly Mock<IArticleRepo> _mock;
    private readonly IArticleRepo _mockRepo;

    public ArticleServiceTests()
    {
        _mock = new Mock<IArticleRepo>();
        _mockRepo = _mock.Object;
        _service = new ArticleService(_mockRepo); 
    }

    // *-- Create --*
    [Fact]
    public async Task CreateArticleAsync_ReturnsCreatedArticle_WithValidRequest()
    {
        // Arrange
        var articleRequest = new ArticleCreateRequest
        {
            Title = "Introduction to C#",
            Content = "This article covers the basics of C# programming language.",
            Author = "Logan Tolbert",
            Tags = "C#,Programming,Basics",
            CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc)
        };

        var createdArticle = new Article
        {
            Id = 1,
            Title = articleRequest.Title,
            Content = articleRequest.Content,
            Author = articleRequest.Author,
            Tags = articleRequest.Tags,
            CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc)
        };

        _mock.Setup(m => m.CreateAsync(It.IsAny<Article>()))
            .ReturnsAsync(createdArticle);

        // Act 
        var result = await _service.CreateArticleAsync(articleRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createdArticle.Id, result.Id);
        Assert.Equal(createdArticle.Title, result.Title);
        Assert.Equal(createdArticle.Content, result.Content);
        Assert.Equal(createdArticle.Author, result.Author);
        Assert.Equal(createdArticle.Tags, result.Tags);
    }


    // TODO: public async Task CreateArticleAsync_ReturnsArgumentNullException_WithNullCreateRequest()

    // TODO: public async Task CreateArticleAsync_ReturnsArgumentException_IfTitleIsNull()


    // *-- Read --*
    [Fact]
    public async Task GetArticleByIdAsync_ReturnsCorrectArticleResponse_WithValidRequest()
    {
        // Arrange
        var response = new Article
        {
            Id = 1,
            Title = "Introduction to C#",
            Content = "This article covers the basics of C# programming language.",
            Author = "Logan Tolbert",
            Tags = "C#,Programming,Basics",
            CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc)
        };

        _mock.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(response);

        // Act
        var result = await _service.GetArticleByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Id, result.Id);
        Assert.Equal(response.Title, result.Title);
        Assert.Equal(response.Content, result.Content);
        Assert.Equal(response.Author, result.Author);
        Assert.Equal(response.Tags, result.Tags);
    }

    // *-- Update --*
    [Fact]
    public async Task UpdateArticleAsync_ReturnsTrue_WithSuccessfulUpdate()
    {
        // Arrange
        var updateRequest = new ArticleUpdateRequest
        {
            Title = "Intro to C#",
            Content = "This article covers the basics of C# programming language.",
            Author = "Logan Tolbert",
            Tags = "C#,Programming,Basics",
            UpdatedAt = new DateTime(2025, 5, 9, 2, 0, 0, DateTimeKind.Utc)
        };

        _mock.Setup(m => m.UpdateAsync(It.IsAny<int>(), It.IsAny<Article>()))
         .ReturnsAsync(true);

        // Act
        var result = await _service.UpdateArticleAsync(1, updateRequest);

        Assert.True(result);
    }

    // *-- Delete --*
    [Fact]
    public async Task DeleteArticleAsync_ReturnsTrue_WithSuccessfulDeletion()
    {
        _mock.Setup(m => m.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        // Act
        var result = await _service.DeleteArticleAsync(1);

        Assert.True(result);
    }
}
