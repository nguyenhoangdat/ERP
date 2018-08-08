using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.API.Models;
using Warehouse.API.Models.StockTaking;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTakingsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StockTakingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StockTakings
        [HttpGet]
        public IEnumerable<StockTaking> GetStockTakings()
        {
            return _context.StockTakings;
        }

        // GET: api/StockTakings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockTaking([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockTaking = await _context.StockTakings.FindAsync(id);

            if (stockTaking == null)
            {
                return NotFound();
            }

            return Ok(stockTaking);
        }

        // PUT: api/StockTakings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockTaking([FromRoute] int id, [FromBody] StockTaking stockTaking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stockTaking.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockTaking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockTakingExists(id))
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

        // POST: api/StockTakings
        [HttpPost]
        public async Task<IActionResult> PostStockTaking([FromBody] StockTaking stockTaking)
        {
            this.ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.StockTakings.Add(stockTaking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockTaking", new { id = stockTaking.Id }, stockTaking);
        }

        // DELETE: api/StockTakings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockTaking([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockTaking = await _context.StockTakings.FindAsync(id);
            if (stockTaking == null)
            {
                return NotFound();
            }

            _context.StockTakings.Remove(stockTaking);
            await _context.SaveChangesAsync();

            return Ok(stockTaking);
        }

        private bool StockTakingExists(int id)
        {
            return _context.StockTakings.Any(e => e.Id == id);
        }
    }
}