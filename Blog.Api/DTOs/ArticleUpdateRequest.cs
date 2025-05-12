using DataAccess.Entities;

namespace BlogApi.DTOs;

public class ArticleUpdateRequest
{
    public string Title { get; set; } = string.Empty!;
    public string Content { get; set; } = string.Empty!;
    public string Author { get; set; } = string.Empty!;
    public string Tags { get; set; } = string.Empty!;
    public DateTime UpdatedAt { get; set; } 
}

public static class ArticleUpdateExtensions
{
    public static Article ToArticle(this ArticleUpdateRequest update)
    {
        return new Article
        {
 
            Title = update.Title,
            Content = update.Content,
            Author = update.Author,
            Tags = update.Tags,
            UpdatedAt = update.UpdatedAt,
        };
    }
}