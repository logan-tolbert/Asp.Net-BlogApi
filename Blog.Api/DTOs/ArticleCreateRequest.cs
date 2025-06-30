using DataAccess.Entities;

namespace Blog.Api.DTOs;

public record ArticleCreateRequest
{
    public required string Title { get; init; }
    public required string Content { get; init; }
    public required string Author { get; init; }
    public string Tags { get; init; } = string.Empty!;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

    public ArticleCreateRequest()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}


public static class ArticleCreateRequestExtensions
{
    public static Article ToArticle(this ArticleCreateRequest request)
    {
        return new Article
        {
            Title = request.Title,
            Content = request.Content,
            Author = request.Author,
            Tags = request.Tags,
            CreatedAt = request.CreatedAt,
            UpdatedAt = request.UpdatedAt
        };
    }

}

