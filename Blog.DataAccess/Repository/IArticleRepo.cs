using DataAccess.Entities;

namespace DataAccess.Repository
{
    public interface IArticleRepo
    {
        Task<Article> CreateAsync(Article article);
        Task<Article?> GetByIdAsync(int id);
        Task<IEnumerable<Article>> GetAllAsync();
        Task<IEnumerable<Article>> GetAllPaginatedAsync(int page, int pageSize);
        Task<IEnumerable<string>> GetAvailableTagsAsync();
        Task<bool> UpdateAsync(int id, Article article);
        Task<bool> DeleteAsync(int id);
    }
}
