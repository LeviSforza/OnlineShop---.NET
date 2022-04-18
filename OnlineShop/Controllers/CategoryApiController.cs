using Lista10.DAL;
using Lista10.Data;
using Lista10.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lista10.Controllers
{
    [EnableCors]
    [Route("api/category")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private IRepository<Category> repository;
        public CategoryApiController(IRepository<Category> repo) => repository = repo;


        // GET: api/<CategoryApiController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await repository.GetAll();
        }

        // GET api/<CategoryApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = repository.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return await category;
        }


        // POST api/<CategoryApiController>
        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category value)
        {
            Category category = new Category
            {
                Name = value.Name
            };

            return await repository.Add(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Category category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            await repository.Update(category);

            return NoContent();
        }


        // DELETE api/<CategoryApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var category = await repository.Delete(id);
            if (category == 1)
            {
                return BadRequest();
            }
            if (category == 2)
            {
                return NotFound();
            }
            return NoContent();
        }


    
    }
}
