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
    public class StockTakingItemsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StockTakingItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StockTakingItems
        [HttpGet]
        public IEnumerable<StockTakingItem> GetGetStockTakingItems()
        {
            return _context.StockTakingItems;
        }

        // GET: api/StockTakingItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockTakingItem([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockTakingItem = await _context.StockTakingItems.FindAsync(id);

            if (stockTakingItem == null)
            {
                return NotFound();
            }

            return Ok(stockTakingItem);
        }

        // PUT: api/StockTakingItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockTakingItem([FromRoute] long id, [FromBody] StockTakingItem stockTakingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stockTakingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockTakingItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockTakingItemExists(id))
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

        // POST: api/StockTakingItems
        [HttpPost]
        public async Task<IActionResult> PostStockTakingItem([FromBody] StockTakingItem stockTakingItem)
        {
            this.ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.StockTakingItems.Add(stockTakingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockTakingItem", new { id = stockTakingItem.Id }, stockTakingItem);
        }

        // DELETE: api/StockTakingItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockTakingItem([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockTakingItem = await _context.StockTakingItems.FindAsync(id);
            if (stockTakingItem == null)
            {
                return NotFound();
            }

            _context.StockTakingItems.Remove(stockTakingItem);
            await _context.SaveChangesAsync();

            return Ok(stockTakingItem);
        }

        private bool StockTakingItemExists(long id)
        {
            return _context.StockTakingItems.Any(e => e.Id == id);
        }
    }
}