using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;

namespace SampleAPI.Repository;

public class ArticleRepository : IArticleRepository
{
    private readonly BlogContext _context;

    public ArticleRepository(BlogContext context)
    {
        _context = context;
    }

    public async void Create(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
    }

    public async void DeleteAsync(Article article)
    {
        if (article == null)
        {
            throw new ArgumentNullException("文章為空 找不到文章");
        }
        else
        {
            _context.Entry(article).State = EntityState.Deleted;
            await SaveChangesAsync();
        }
    }

    public async Task<Article?> GetAsync(int id)
    {
        return await _context.Articles.FindAsync(id);
    }

    public async Task<IEnumerable<Article>> GetAllAsync()
    {
        return await _context.Articles.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async void UpdateAsync(Article article)
    {
        _context.Entry(article).State = EntityState.Modified;
        await SaveChangesAsync();
    }
}

