using Blog.Api.DTOs;
using Blog.Api.Exceptions;
using Blog.Api.Extensions;
using Blog.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

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
    [Route(Endpoints.Articles.Post)]
    public async Task<IActionResult> PostAsync([FromBody] ArticleCreateRequest newArticle)
    {
        var createdArticle = await _service.CreateArticleAsync(newArticle);
        if (createdArticle is null)
        {
            return this.ArticleCreationFailed();
        }
        return Created($"{Endpoints.Articles.Post}/{createdArticle.Id}", createdArticle);
    }

    // *-- Read --*
    [HttpGet]
    [Route(Endpoints.Articles.GetAll)]
    //TODO: Implement DTO for query parameters to encapsulate filtering and pagination logic
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
    [Route(Endpoints.Articles.Get)]
    public async Task<IActionResult> GetAsync([FromRoute]int id)
    {
        var article = await _service.GetArticleByIdAsync(id);
        if (article is null)
        {
            return this.ArticleNotFound();
        }
        return Ok(article);
    }

    [HttpGet]
    [Route(Endpoints.Articles.GetTags)]
    public async Task<IActionResult> GetTagsAsync()
    {
        var tags = await _service.GetAllTagsAsync();
        return Ok(tags);
    }

    // *-- Update --*
    [HttpPut]
    [Route(Endpoints.Articles.Put)]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] ArticleUpdateRequest updatedArticle)
    {
        return await _service.UpdateArticleAsync(id, updatedArticle)
            ? NoContent() : this.ArticleNotFound();
    }

    // *-- Delete --*
    [HttpDelete]
    [Route(Endpoints.Articles.Delete)]
    public async Task<IActionResult> DeleteAsync([FromRoute]int id)
    {
        return await _service.DeleteArticleAsync(id)
            ? NoContent() : this.ArticleNotFound();
    }
}
