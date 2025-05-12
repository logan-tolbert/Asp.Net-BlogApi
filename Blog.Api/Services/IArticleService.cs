using BlogApi.DTOs;

namespace BlogApi.Services;

public interface IArticleService
{
    Task<ArticleResponse> CreateArticleAsync(ArticleCreateRequest? request);
    Task<ArticleResponse> GetArticleByIdAsync(int id);
    Task<IEnumerable<ArticleResponse>> GetArticlesAsync();
    Task<List<string>> GetAllTagsAsync();
    Task<List<ArticleResponse>> GetArticlesByTagAsync(string tag);
    Task<bool> UpdateArticleAsync(int id, ArticleUpdateRequest? request);
    Task<bool> DeleteArticleAsync(int id);


}
