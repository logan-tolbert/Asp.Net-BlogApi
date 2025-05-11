using BlogApi.DataAccess.Entities;

namespace BlogApi.DTOs;

public record ArticleResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty!;
    public string Content { get; set; } = string.Empty!;
    public string Author { get; set; } = string.Empty!;
    public string Tags { get; set; } = string.Empty!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}

public static class ArticleExtensions
{
    public static ArticleResponse ToArticleResponse(this Article article)
    {
        return new ArticleResponse
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            Author = article.Author,
            Tags = article.Tags,
            CreatedAt = article.CreatedAt,
            UpdatedAt = article.UpdatedAt,
        };
    }
}


