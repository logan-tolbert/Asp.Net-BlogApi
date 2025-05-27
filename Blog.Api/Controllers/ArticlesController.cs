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
    public async Task<IActionResult> GetAsync([FromQuery] string? tag)
    {
        var articles = string.IsNullOrWhiteSpace(tag)
            ? await _service.GetArticlesAsync()
            : await _service.GetArticlesByTagAsync(tag);

        if (articles == null)
        {
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                detail: "An error occurred while retrieving articles.");
        }

        return Ok(articles);
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
