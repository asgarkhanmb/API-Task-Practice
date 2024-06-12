using API_Practice_Task.Data;
using API_Practice_Task.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Practice_Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var book = await _context.Books.ToListAsync();
            return Ok(book);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data = await _context.Books.FindAsync(id);

            if (data is null) return NotFound();
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id is null) return BadRequest();

            var data = await _context.Books.FindAsync(id);

            if (data is null) return NotFound();

            _context.Books.Remove(data);

            await _context.SaveChangesAsync();

            return Ok();

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Books.AddAsync(book);

            await _context.SaveChangesAsync();

            return CreatedAtAction("Create", book);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var data = await _context.Books.FindAsync(id);

            if (data is null) return NotFound();

            data.Name = book.Name;
            data.Page = book.Page;

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string search)
        {
            return Ok(search == null ? await _context.Books.ToListAsync() : await _context.Books.Where(m => m.Name.Contains(search)).ToListAsync());
        }
    }
}
