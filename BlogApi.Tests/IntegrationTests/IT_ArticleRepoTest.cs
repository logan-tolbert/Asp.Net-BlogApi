using BlogApi.DataAccess.Entities;
using BlogApi.DataAccess.Repository;
using BlogApi.Tests.TestHelpers;

namespace BlogApi.Tests.IntegrationTests;
public class IT_ArticleRepoTest
{
    // *-- Create --*
    [Fact]
    public async Task CreateAsync_CreatesAndReturnsArticle_WithValidInput()
    {
        // Arrange
        await using var ctx = await DbContextHelper.ConfigureTestContext();
        var repo = new ArticleRepo(ctx);
        var newArticle = new Article
        {
            Title = "How to test the repositories",
            Content = "This article covers the basics of repository testing when using EF Core.",
            Author = "Logan Tolbert",
            Tags = "C#, Programming, Testing, EF Core"
        };

        // Act 
        var result = await repo.CreateAsync(newArticle);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newArticle.Title, result.Title);
        Assert.Equal(newArticle.Content, result.Content); 
        Assert.Equal(newArticle.Author, result.Author);
        Assert.Equal(newArticle.Tags, result.Tags); 
    }

    // *-- Read --* 
    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectArticle_WhenExists()
    {
        // Arrange
        await using var ctx = await DbContextHelper.ConfigureTestContext();
        var repo = new ArticleRepo(ctx);
        var article = new Article
        {
            Id = 1,
            Title = "Introduction to C#",
            Content = "This article covers the basics of C# programming language.",
            Author = "Logan Tolbert",
            Tags = "C#,Programming,Basics",
            CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc)
        };

        // Act
        var result = await repo.GetByIdAsync(article.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(article.Id, result.Id);
        Assert.Equal(article.Title, result.Title);
        Assert.Equal(article.Content, result.Content);
        Assert.Equal(article.Author, result.Author);
        Assert.Equal(article.Tags, result.Tags);
        Assert.Equal(article.CreatedAt, result.CreatedAt);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotExists()
    {
        // Arrange 
        await using var ctx = await DbContextHelper.ConfigureTestContext();
        var repo = new ArticleRepo(ctx);

        // Act
        var result = await repo.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }


    // *-- Update --*
    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WithSuccessfulUpdate()
    {
        // Arrange
        await using var ctx = await DbContextHelper.ConfigureTestContext();
        var repo = new ArticleRepo(ctx);
        var update = new Article
        {
            Id = 1,
            Title = "Intro to C#",
            Content = "This article covers the basics of C# programming language.",
            Author = "Logan Tolbert",
            Tags = "C#,Programming,Basics",
            CreatedAt = new DateTime(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2025, 5, 9, 2, 0, 0, DateTimeKind.Utc)
        };

        // Act 
        var result = await repo.UpdateAsync(1, update); 

        // Assert 
        Assert.True(result);
    }

    // *-- Delete --*
    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WithSuccessfulUpdate()
    {
        // Arrange
        await using var ctx = await DbContextHelper.ConfigureTestContext();
        var repo = new ArticleRepo(ctx);
        const int id = 1;

        // Act 
        var result = await repo.DeleteAsync(id);

        // Assert 
        Assert.True(result);
    }
}


