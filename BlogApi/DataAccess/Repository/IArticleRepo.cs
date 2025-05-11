using BlogApi.DataAccess.Entities;

namespace BlogApi.DataAccess.Repository
{
    public interface IArticleRepo
    {
        Task<Article> CreateAsync(Article article);
        Task<Article?> GetByIdAsync(int id);
        Task<IEnumerable<Article>> GetAllAsync();
        Task UpdateAsync(Article article);
        Task<bool> DeleteAsync(int id);
    }
}
