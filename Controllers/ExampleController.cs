using FirstApp.Data;
using FirstApp.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly ExampleDbContext _context;

        public ExampleController(ExampleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Example>>> GetExamples()
        {
            //return Ok(examples);
            return Ok(await _context.Examples.ToListAsync());
        }

        // Can write this as [HttpGet("{id}")]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Example>> GetExampleById(int id)
        {
            //var example = examples.FirstOrDefault(e => e.Id == id);
            var example = await _context.Examples.FindAsync(id);

            if (example is null)
                return NotFound();

            return Ok(example);
        }

        [HttpPost]
        public async Task<ActionResult<Example>> AddExample(Example newExample)
        {
            if (newExample is null)
                return BadRequest();

            //newExample.Id = examples.Max(e => e.Id) + 1;
            //examples.Add(newExample);

            _context.Examples.Add(newExample);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExampleById), new { id = newExample.Id }, newExample);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExample(int id, Example updatedExample)
        {
            //var example = examples.FirstOrDefault(e => e.Id == id);
            var example = await _context.Examples.FindAsync(id);

            if (example is null)
                return NotFound();

            example.Label = updatedExample.Label;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExample(int id)
        {
            //var example = examples.FirstOrDefault(e => e.Id == id);
            var example = await _context.Examples.FindAsync(id);

            if (example is null)
                return NotFound();

            //examples.Remove(example);
            _context.Examples.Remove(example);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
