using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repository;

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

    public async Task<IEnumerable<Article>> GetFilteredAndPaginatedAsync(
     string tag, DateTime? startDate, DateTime? endDate, int page, int pageSize)
    {
        IQueryable<Article> query = _db.Articles;

        if (!string.IsNullOrWhiteSpace(tag))
        {
            query = query.Where(a => a.Tags.ToLower().Contains(tag.ToLower()));
        }
        if (startDate.HasValue)
        {
            query = query.Where(a => a.CreatedAt >= startDate.Value);
        }
        if (endDate.HasValue)
        {
            query = query.Where(a => a.CreatedAt <= endDate.Value);
        }

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetAvailableTagsAsync()
    {
        return await _db.Articles
            .Where(a => !string.IsNullOrWhiteSpace(a.Tags))
            .Select(a => a.Tags)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(int id, Article updatedArticle)
    {
        var existingArticle = await _db.Articles.FindAsync(id);
        if (existingArticle == null)
        {
            return false;
        }

        existingArticle.Title = updatedArticle.Title;
        existingArticle.Content = updatedArticle.Content;
        existingArticle.Author = updatedArticle.Author;
        existingArticle.Tags = updatedArticle.Tags;
        existingArticle.UpdatedAt = updatedArticle.UpdatedAt;

        var rowsAffected = await _db.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)   
    {
        var article = await _db.Articles.SingleOrDefaultAsync(a => a.Id == id);

        //TODO: Handle possible null reference 
        //       - ?? change return type ??
        _db.Articles.Remove(article!);
        var deletions = await _db.SaveChangesAsync(true);

        return deletions > 0;
    }
}
