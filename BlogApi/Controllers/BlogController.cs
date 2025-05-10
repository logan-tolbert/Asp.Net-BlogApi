using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        [Route("articles")]
        public async Task<IActionResult> GetArticles()
        {
            await Task.CompletedTask;
            return Ok("GET: /articles Success!");
        }

        [HttpGet]
        [Route("articles/{id}")]
        public async Task<IActionResult> GetArticleById(int id)
        {
            await Task.CompletedTask;
            return Ok($"GET: /articles/{id} Success!");
        }

        [HttpPost]
        [Route("articles")]
        public async Task<IActionResult> CreateArticle(Article newArticle)
        {
            await Task.CompletedTask;
            return Ok("POST: /articles Success!");
        }

        [HttpPut]
        [Route("articles/{id}")]
        public async Task<IActionResult> UpdateArticle(int id,[FromBody] Article updatedArticle)
        {
            await Task.CompletedTask;
            return Ok($"PUT: /articles/{id} Success!");
        }

        [HttpDelete]
        [Route("articles/{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            await Task.CompletedTask;
            return Ok($"DELETE: /articles/{id} Success!");
        }
    }
}
