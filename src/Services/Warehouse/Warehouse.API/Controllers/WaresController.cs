using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.API.Models;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaresController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public WaresController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Wares
        [HttpGet]
        public IEnumerable<Ware> GetWares()
        {
            return _context.Wares;
        }

        // GET: api/Wares/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWare([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ware = await _context.Wares.FindAsync(id);

            if (ware == null)
            {
                return NotFound();
            }

            return Ok(ware);
        }

        // PUT: api/Wares/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWare([FromRoute] int id, [FromBody] Ware ware)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ware.Id)
            {
                return BadRequest();
            }

            _context.Entry(ware).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WareExists(id))
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

        // POST: api/Wares
        [HttpPost]
        public async Task<IActionResult> PostWare([FromBody] Ware ware)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Wares.Add(ware);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWare", new { id = ware.Id }, ware);
        }

        // DELETE: api/Wares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWare([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ware = await _context.Wares.FindAsync(id);
            if (ware == null)
            {
                return NotFound();
            }

            _context.Wares.Remove(ware);
            await _context.SaveChangesAsync();

            return Ok(ware);
        }

        private bool WareExists(int id)
        {
            return _context.Wares.Any(e => e.Id == id);
        }
    }
}