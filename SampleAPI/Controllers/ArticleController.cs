using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;
using SampleAPI.Repository;

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
    private readonly IArticleRepository _articleRepository;

    public ArticleController(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    // GET: api/Article
    [HttpGet]
    public async Task<IEnumerable<Article>> GetArticles()
    {
        return await _articleRepository.GetAllAsync();
    }

    // GET: api/Article/5
    /// <summary>
    /// 取得特定一筆文章
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticle(int id)
    {
        var article = await _articleRepository.GetAsync(id);

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

        try
        {
            _articleRepository.UpdateAsync(article);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!(await ArticleExistsAsync(id)))
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Article> PostArticle(Article article)
    {
        _articleRepository.Create(article);

        return CreatedAtAction("GetArticle", new { id = article.Id }, article);
    }

    // DELETE: api/Article/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var article = await _articleRepository.GetAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        _articleRepository.DeleteAsync(article);

        return NoContent();
    }

    private async Task<bool> ArticleExistsAsync(int id)
    {
        return await _articleRepository.GetAsync(id) != null;
    }
}
