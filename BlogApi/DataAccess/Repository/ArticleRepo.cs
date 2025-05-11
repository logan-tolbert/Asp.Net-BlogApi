using BlogApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.DataAccess.Repository;

public class ArticleRepo : IArticleRepo
{
    private readonly BlogDbContext _db;

    public ArticleRepo(BlogDbContext db)
    {
        _db = db;
    }

    public async Task<Article> CreateAsync(Article article)
    {
        _db.Articles.Add(article);
        await _db.SaveChangesAsync();
        return article;
    }

    public async Task<Article?> GetByIdAsync(int id)
    {
        return await _db.Articles.SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Article>> GetAllAsync()
    {
        var articles = await _db.Articles.ToListAsync();
        return articles;
    }

    public async Task UpdateAsync(Article article)
    {
        _db.Articles.Update(article);
        await _db.SaveChangesAsync();
    }


    public async Task<bool> DeleteAsync(int id)   
    {
        var article = await _db.Articles.SingleOrDefaultAsync(a => a.Id == id);

        //TODO: Handle possible null reference
        _db.Articles.Remove(article!);
        var deletions = await _db.SaveChangesAsync(true);

        return deletions > 0;
    }
}
