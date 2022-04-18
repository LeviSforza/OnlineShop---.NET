using Lista10.DAL;
using Lista10.Data;
using Lista10.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lista10.Controllers
{
    [EnableCors]
    [Route("api/article")]
    [ApiController]
    public class ArticleApiController : ControllerBase
    {
        private IRepository<Article> repository;
        public ArticleApiController(IRepository<Article> repo)
        {
            repository = repo;
        }

            // GET: api/<CategoryApiController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> Get()
        {
            return await repository.GetAll();
        }

        // GET api/<CategoryApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> Get(int id)
        {
            var article = repository.Get(id);
            if (article == null)
            {
                return NotFound();
            }

            return await article;
        }

        // POST api/<CategoryApiController>
        [HttpPost]
        public async Task<ActionResult<Article>> Post([FromBody] Article value)
        {
            Article article = new Article
            {
                Name = value.Name,
                Price = value.Price,
                CategoryID = value.CategoryID,
                ImageUrl = "/image/nophoto.jpg"
            };

            return await repository.Add(article);
        }
 

        // PUT api/<CategoryApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Article article)
        {
            article.ImageUrl = "/image/nophoto.jpg";
            if (id != article.ArticleID)
            {
                return BadRequest();
            }

            var res = await repository.Update(article);
            if (res == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/<CategoryApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await repository.Delete(id);

            if (article == 2)
            {
                return NoContent();
            }
            return NoContent();
        }

        [HttpGet("next/{id},{n},{catId}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetNextN(int id, int n, int catId)
        {
            return await repository.GetNextN(id, n, catId);
        }


    }
}
