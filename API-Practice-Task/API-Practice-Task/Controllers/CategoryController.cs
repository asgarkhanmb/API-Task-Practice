using API_Practice_Task.Data;
using API_Practice_Task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Practice_Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _context.Categories.ToListAsync();
            return Ok(category);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var data = await _context.Categories.FindAsync(id);

            if (data is null) return NotFound();
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if(id is null) return BadRequest();

            var data = await _context.Categories.FindAsync(id);

            if (data is null) return NotFound();

            _context.Categories.Remove(data);

            await _context.SaveChangesAsync();  

            return Ok();

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return CreatedAtAction("Create", category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)   return BadRequest(ModelState);

            var data = await _context.Categories.FindAsync(id);

            if (data is null) return NotFound();

            data.Name = category.Name;

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string search)
        {
            return Ok(search == null ? await _context.Categories.ToListAsync(): await _context.Categories.Where(m => m.Name.Contains(search)).ToListAsync());
        }
    }
}
