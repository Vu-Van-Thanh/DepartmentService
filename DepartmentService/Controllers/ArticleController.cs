using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DepartmentService.Services;
using DepartmentService.API.DTO;

namespace DepartmentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleInfo>>> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticles();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleInfo>> GetArticleById(Guid id)
        {
            var article = await _articleService.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpPost]
        public async Task<ActionResult<ArticleInfo>> CreateArticle(ArticleUpsertRequest article)
        {
            var createdArticle = await _articleService.UpsertArticle(article);
            return CreatedAtAction(nameof(GetArticleById), new { id = article.ArticleId }, createdArticle);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArticleInfo>> UpdateArticle(Guid id, ArticleUpsertRequest article)
        {
            if (id != article.ArticleId)
            {
                return BadRequest();
            }
            var updatedArticle = await _articleService.UpsertArticle(article);
            return Ok(updatedArticle);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(Guid id)
        {
            var result = await _articleService.DeleteArticle(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("upload-word")]
        public async Task<IActionResult> UploadWordFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            if (!file.FileName.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Only .docx files are allowed.");
            }

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    await _articleService.ExtractNewsFromStream(stream, file.FileName);
                }

                return Ok(new { message = "File processed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error processing file", error = ex.Message });
            }
        }
    }
}
