using BlogApi.Models;

namespace BlogApi.Repository
{
    public class ArticleRepo : IArticleRepo
    {
     
        public Task<Article> CreateAsync(Article article)
        {
            throw new NotImplementedException();
        }
        
        public Task<Article?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Article>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Article article)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
