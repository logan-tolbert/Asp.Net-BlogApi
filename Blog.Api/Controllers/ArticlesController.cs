using Blog.Api.DTOs;
using Blog.Api.Exceptions;
using Blog.Api.Extensions;
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
    public async Task<IActionResult> PostAsync([FromBody] ArticleCreateRequest newArticle)
    {
        var createdArticle = await _service.CreateArticleAsync(newArticle);
        if (createdArticle == null)
        {
            return this.ArticleCreationFailed();
        }
        return Created($"api/v0/articles/{createdArticle.Id}", createdArticle);
    }

    // *-- Read --*
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] string? tag,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var effectiveTag = string.IsNullOrWhiteSpace(tag) ? string.Empty : tag;
      
        var articles = await _service.GetArticlesFilteredAndPaginatedAsync(effectiveTag, startDate, endDate, page, pageSize);
        return Ok(articles);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var article = await _service.GetArticleByIdAsync(id);
        if (article == null)
        {
            return this.ArticleNotFound();

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
    public async Task<IActionResult> PutAsync(int id, [FromBody] ArticleUpdateRequest updatedArticle)
    {
        return await _service.UpdateArticleAsync(id, updatedArticle)
            ? NoContent() : this.ArticleNotFound();
    }

    // *-- Delete --*
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return await _service.DeleteArticleAsync(id)
            ? NoContent() : this.ArticleNotFound();
    }
}
