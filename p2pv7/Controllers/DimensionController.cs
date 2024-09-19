using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using p2pv7.Data;
using p2pv7.Models;

namespace p2pv7.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DimensionsController : ControllerBase
    {
        private readonly DataContext _context;

        public DimensionsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dimension>>> GetDimensions()
        {
            if (_context.Dimensions == null)
                return NotFound();

            return await _context.Dimensions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dimension>> GetDimension(int id)
        {
            if (_context.Dimensions == null)
                return NotFound();

            var dimension = await _context.Dimensions.FindAsync(id);

            if (dimension == null)
                return NotFound();

            return dimension;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimension(int id, Dimension dimension)
        {
            if (id != dimension.Id)
                return BadRequest();

            _context.Entry(dimension).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimensionExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Dimension>> PostDimension(Dimension dimension)
        {
            if (_context.Dimensions == null)
                return Problem("Entity set 'DataContext.Dimensions'  is null.");

            _context.Dimensions.Add(dimension);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDimension", new { id = dimension.Id }, dimension);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimension(Guid id)
        {
            if (_context.Dimensions == null)
                return NotFound();

            var dimension = await _context.Dimensions.FindAsync(id);

            if (dimension == null)
                return NotFound();

            _context.Dimensions.Remove(dimension);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimensionExists(int id)
            => (_context.Dimensions?.Any(e => e.Id == id)).GetValueOrDefault();


    }
}
