using SampleAPI.Models;

namespace SampleAPI.Repository;

public interface IArticleRepository
{
    void Create(Article article);
    void UpdateAsync(Article article);
    void DeleteAsync(Article article);
    Task<Article?> GetAsync(int id);
    Task<IEnumerable<Article>> GetAllAsync();
    Task SaveChangesAsync();
}

