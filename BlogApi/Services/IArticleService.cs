using BlogApi.DTOs;

namespace BlogApi.Services
{
    public interface IArticleService
    {
        Task<ArticleResponse> CreateArticleAsync(ArticleCreateRequest? request);

    }
}
