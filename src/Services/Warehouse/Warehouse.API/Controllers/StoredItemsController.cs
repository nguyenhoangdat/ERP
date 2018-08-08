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
    public class StoredItemsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StoredItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StoredItems
        [HttpGet]
        public IEnumerable<StoredItem> GetStoredItems()
        {
            return _context.StoredItems;
        }

        // GET: api/StoredItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoredItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storedItem = await _context.StoredItems.FindAsync(id);

            if (storedItem == null)
            {
                return NotFound();
            }

            return Ok(storedItem);
        }

        // PUT: api/StoredItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoredItem([FromRoute] int id, [FromBody] StoredItem storedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storedItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(storedItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoredItemExists(id))
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

        // POST: api/StoredItems
        [HttpPost]
        public async Task<IActionResult> PostStoredItem([FromBody] StoredItem storedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.StoredItems.Add(storedItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoredItem", new { id = storedItem.Id }, storedItem);
        }

        // DELETE: api/StoredItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoredItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storedItem = await _context.StoredItems.FindAsync(id);
            if (storedItem == null)
            {
                return NotFound();
            }

            _context.StoredItems.Remove(storedItem);
            await _context.SaveChangesAsync();

            return Ok(storedItem);
        }

        private bool StoredItemExists(int id)
        {
            return _context.StoredItems.Any(e => e.Id == id);
        }
    }
}