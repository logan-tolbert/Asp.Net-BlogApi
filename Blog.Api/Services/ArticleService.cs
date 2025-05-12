using DataAccess.Repository;
using BlogApi.DTOs;

namespace BlogApi.Services
{
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

        // Fixed: ArticleService => Implement GetArticlesAsync()
        public async Task<IEnumerable<ArticleResponse>> GetArticlesAsync()
        {
            var result = await _repo.GetAllAsync();
            return result.Select(article => article.ToArticleResponse());
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
            var result = _repo.DeleteAsync(id);
            return result;
        }
    }
}
