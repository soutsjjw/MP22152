using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;

namespace SampleAPI.Controllers;

/*
dotnet tool install -g dotnet-aspnet-codegenerator

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

dotnet aspnet-codegenerator controller --controllerName ArticleController -async -api -actions -m Article -dc BlogContext -outDir Controllers
*/

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly BlogContext _context;

    public ArticleController(BlogContext context)
    {
        _context = context;
    }

    // GET: api/Article
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
    {
        if (_context.Articles == null)
        {
            return NotFound();
        }
        return await _context.Articles.ToListAsync();
    }

    // GET: api/Article/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticle(int id)
    {
        if (_context.Articles == null)
        {
            return NotFound();
        }
        var article = await _context.Articles.FindAsync(id);

        if (article == null)
        {
            return NotFound();
        }

        return article;
    }

    // PUT: api/Article/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutArticle(int id, Article article)
    {
        if (id != article.Id)
        {
            return BadRequest();
        }

        _context.Entry(article).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArticleExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Article
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Article>> PostArticle(Article article)
    {
        if (_context.Articles == null)
        {
            return Problem("Entity set 'BlogContext.Articles'  is null.");
        }
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetArticle", new { id = article.Id }, article);
    }

    // DELETE: api/Article/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        if (_context.Articles == null)
        {
            return NotFound();
        }
        var article = await _context.Articles.FindAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ArticleExists(int id)
    {
        return (_context.Articles?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
