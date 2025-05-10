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
        public IActionResult GetArticles()
        {
            return Ok("GET: /articles  Success!");
        }

        [HttpGet]
        [Route("articles/{id}")]
        public IActionResult GetArticleById(int id)
        {
            return Ok($"GET: /articles/{id} Success!");
        }

        [HttpPost]
        [Route("articles")]
        public IActionResult CreateArticle(Article newArticle)
        {
            return Ok("POST: /articles Success!");
        }

        [HttpPut]
        [Route("articles/{id}")]
        public IActionResult UpdateArticle(int id)
        {
            return Ok($"PUT: /articles/{id} Success!");
        }

        [HttpDelete]
        [Route("articles/{id}")]
        public IActionResult DeleteArticle(int id)
        {
            return Ok($"DELETE: /articles/{id} Success!");
        }
    }
}
