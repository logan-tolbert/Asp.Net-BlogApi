using BlogApi.DataAccess.Repository;
using BlogApi.Tests.TestHelpers;

namespace BlogApi.Tests.IntegrationTests;
public class IT_ArticleRepoTest
{
    // *-- Create --*

    // *-- Read --* 
    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectArticle_WhenExists()
    {
        // Arrange
        await using var ctx = await DbContextHelper.ConfigureTestContext();
        var repo = new ArticleRepo(ctx);
        const int id = 1;
        
        // Act
        var result = await repo.GetByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
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

    // *-- Delete --*
}


