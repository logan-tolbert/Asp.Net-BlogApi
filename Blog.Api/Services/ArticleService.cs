using Blog.Api.DTOs;
using Blog.DataAccess.Repository;

namespace Blog.Api.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepo _repo;

    public ArticleService(IArticleRepo repo)
    {
        _repo = repo;
    }

    // *-- Create --*
    public async Task<ArticleResponse> CreateArticleAsync(ArticleCreateRequest? request)
    {
        var result = await _repo.CreateAsync(request!.ToArticle());
        return result.ToArticleResponse();
    }

    // *-- Read --*
    public async Task<ArticleResponse> GetArticleByIdAsync(int id)
    {
        var result = await _repo.GetByIdAsync(id);
        return result!.ToArticleResponse();
    }

    public async Task<IEnumerable<ArticleResponse>> GetArticlesAsync()
    {
        var result = await _repo.GetAllAsync();
        return result.Select(article => article.ToArticleResponse());
    }

    public async Task<IEnumerable<ArticleResponse>> GetArticlesFilteredAndPaginatedAsync(
         string tag, DateTime? startDate, DateTime? endDate, int page, int pageSize)
    {
        var effectiveTag = string.IsNullOrWhiteSpace(tag) ? string.Empty : tag;
        var result = await _repo.GetFilteredAndPaginatedAsync(effectiveTag, startDate, endDate, page, pageSize);
        return result.Select(article => article.ToArticleResponse());
    }

    public async Task<IEnumerable<string>> GetAllTagsAsync()
    {
        var tagStrings = await _repo.GetAvailableTagsAsync();
        return tagStrings
            .SelectMany(ParseTags)
            .Distinct()
            .ToList();
    }

    public async Task<IEnumerable<ArticleResponse>> GetArticlesByTagAsync(string tag)
    {
        // This method can be replaced by the filtered approach if needed.
        var articles = await _repo.GetAllAsync();
        var filtered = articles
            .Where(a => !string.IsNullOrEmpty(a.Tags))
            .Where(a => ParseTags(a.Tags).Contains(tag.ToLower()))
            .ToList();
        return filtered.Select(a => a.ToArticleResponse()).ToList();
    }

    // *-- Update --*
    public async Task<bool> UpdateArticleAsync(int id, ArticleUpdateRequest? request)
    {
        var result = await _repo.UpdateAsync(id, request!.ToArticle());
        return result;
    }

    // *-- Delete --*
    public Task<bool> DeleteArticleAsync(int id)
    {
        return _repo.DeleteAsync(id);
    }

    // TODO: Refactor – move to a utility class or extension method
    private static List<string> ParseTags(string tagString)
    {
        return tagString
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(t => t.Trim().ToLower())
            .ToList();
    }
}
