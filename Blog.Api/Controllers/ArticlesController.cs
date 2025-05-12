using BlogApi.DTOs;
using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[Route("api/v0/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    public readonly IArticleService _service;
    public ArticlesController(IArticleService service)
    {
        _service = service;
    }

    // *-- Create --* 
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ArticleCreateRequest newArticle)
    {
        var createdArticle = await _service.CreateArticleAsync(newArticle);

        if (createdArticle == null)
        {
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                detail: "Article creation failed.");
        }

        return CreatedAtAction(nameof(GetById),
            new { id = createdArticle.Id }, createdArticle);
    }

    // *-- Read --*
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var articles = await _service.GetArticlesAsync();

        if (articles == null)
        {
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                detail: "An error occurred while retrieving articles.");
        }

        return Ok(articles);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var article = await _service.GetArticleByIdAsync(id);
        if (article == null)
        {
            return NotFound(id);
        }

        return Ok(article);
    }

    // *-- Update --*
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ArticleUpdateRequest updatedArticle)
    {
        return await _service.UpdateArticleAsync(id, updatedArticle) 
            ? NoContent() : BadRequest();
    }

    // *-- Delete --*
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return await _service.DeleteArticleAsync(id) 
            ? NoContent() : BadRequest();
    }
}
