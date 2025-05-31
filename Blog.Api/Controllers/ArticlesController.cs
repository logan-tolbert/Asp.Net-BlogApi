using Blog.Api.DTOs;
using Blog.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

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

        return Created($"api/v0/articles/{createdArticle.Id}", createdArticle);
    }

    // *-- Read --*
    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] string? tag,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
           
            var pagedArticles = await _service.GetArticlesPaginatedAsync(page, pageSize);
            return Ok(pagedArticles);
        }
        else
        {

            var articles = await _service.GetArticlesByTagAsync(tag);
            var pagedArticles = articles
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            return Ok(pagedArticles);
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var article = await _service.GetArticleByIdAsync(id);
        if (article == null)
        {
            return NotFound(id);
        }

        return Ok(article);
    }

    [HttpGet("tags")]
    public async Task<IActionResult> GetTagsAsync()
    {
        var tags = await _service.GetAllTagsAsync();
        return Ok(tags);
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
