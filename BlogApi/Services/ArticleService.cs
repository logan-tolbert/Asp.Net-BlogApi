using BlogApi.DataAccess.Repository;
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

        public async Task<ArticleResponse> CreateArticleAsync(ArticleCreateRequest? request)
        {
            //TODO: Handle possible null reference 
            //       - ?? change return type ??
            var result =  await _repo.CreateAsync(request!.ToArticle());
            return result.ToArticleResponse();

        }

       
    }
}
