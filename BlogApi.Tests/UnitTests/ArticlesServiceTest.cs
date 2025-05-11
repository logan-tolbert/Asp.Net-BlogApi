using BlogApi.DataAccess.Entities;
using BlogApi.DataAccess.Repository;
using BlogApi.DTOs;
using BlogApi.Services;
using Moq;

namespace BlogApi.Tests.UnitTests;

public class ArticleServiceTests
{
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

    //[Fact]
    //public async Task CreateArticleAsync_ReturnsArgumentNullException_WithNullCreateRequest()
    //{
    //    // Arrange

    //    // Act 

    //    // Assert
    //}

    //[Fact]
    //public async Task CreateArticleAsync_ReturnsArgumentException_IfTitleIsNull()
    //{
    //    // Arrange

    //    // Act 

    //    // Assert
    //}

    // *-- Read --*

    // *-- Update --*

    // *-- Delete --*
}
