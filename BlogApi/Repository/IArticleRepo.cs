using BlogApi.Models;

namespace BlogApi.Repository
{
    public interface IArticleRepo
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int id);
        Task<Article> CreateAsync(Article article);
        Task UpdateAsync(Article article);
        Task DeleteAsync(int id);
    }
}
