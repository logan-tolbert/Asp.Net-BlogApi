using BlogApi.DTOs;
using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        public readonly IArticleService _service;
        public BlogController(IArticleService service) 
        { 
            _service = service;
        }

        // *-- Create --* 
        [HttpPost]
        [Route("articles")]
        public async Task<IActionResult> CreateAsync(ArticleCreateRequest newArticle)
        {
            var createdArticle = await _service.CreateArticleAsync(newArticle);
            return Ok(createdArticle);
        }

        // *-- Read --*
        [HttpGet]
        [Route("articles")]
        public async Task<IActionResult> GetAsync()
        {
            var articles = await _service.GetArticlesAsync();
            return Ok(articles);
        }

        [HttpGet]
        [Route("articles/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var article = await _service.GetArticleByIdAsync(id);
            return Ok(article);
        }


        // *-- Update --*
        [HttpPut]
        [Route("articles/{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromBody] ArticleUpdateRequest updatedArticle)
        {
            var result = await _service.UpdateArticleAsync(id, updatedArticle);
            return Ok(result);
        }

        // *-- Delete --
        [HttpDelete]
        [Route("articles/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteArticleAsync(id);
            return Ok(result);
        }
    }
}
