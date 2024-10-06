using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Discs.Models;

namespace Discs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscsController : ControllerBase
    {
        private readonly DiscGolfDBContext _context;

        public DiscsController(DiscGolfDBContext context)
        {
            _context = context;
        }

        // GET: api/discs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disc>>> GetDiscs()
        {
            return await _context.Discs.ToListAsync();
        }

        // GET: api/discs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Disc>> GetDisc(int id)
        {
            var disc = await _context.Discs.FindAsync(id);

            if (disc == null)
            {
                return NotFound();
            }

            return disc;
        }

        // PUT: api/BasicDiscs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisc(int id, Disc disc)
        {
            if (id != disc.Id)
            {
                return BadRequest();
            }

            _context.Entry(disc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BasicDiscs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Disc>> PostDisc(Disc disc)
        {
            _context.Discs.Add(disc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDisc", new { id = disc.Id }, disc);
        }

        // DELETE: api/BasicDiscs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisc(int id)
        {
            var disc = await _context.Discs.FindAsync(id);
            if (disc == null)
            {
                return NotFound();
            }

            _context.Discs.Remove(disc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscExists(int id)
        {
            return _context.Discs.Any(e => e.Id == id);
        }
    }
}
