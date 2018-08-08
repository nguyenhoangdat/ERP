using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Warehouse.API.Models;
using Warehouse.API.Models.Extensions;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> CreateStockTakingWithWarehouseId([FromRoute] int id)
        {
            if (!ModelState.IsValid || !_context.Warehouses.Any(x => x.Id == id))
            {
                return BadRequest(ModelState);
            }

            StockTaking stockTaking = new StockTaking
            {
                Id = 0,
                StockTakingItems = (from warehouse in _context.Warehouses
                                    join section in _context.Sections on warehouse.Id equals section.WarehouseId
                                    join position in _context.Positions on section.Id equals position.SectionId
                                    join movement in _context.Movements on position.Id equals movement.PositionId
                                    join storedItem in _context.StoredItems on movement.StoredItemId equals storedItem.Id
                                    where warehouse.Id == id
                                    select new StockTakingItem()
                                    {
                                        StockTakingId = 0,
                                        StoredItemId = storedItem.Id,
                                        PositionId = position.Id,
                                        CurrentStock = storedItem.Count(),
                                        CountedStock = 0
                                    }).ToList(),
                WarehouseId = id,
                Area = StockTakingArea.Warehouse
            };

            EntityEntry<StockTaking> entry = await _context.StockTakings.AddAsync(stockTaking);

            return Ok(entry.Entity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CreateStockTakingWithSectionId([FromRoute] int id)
        {
            if (!ModelState.IsValid || !_context.Sections.Any(x => x.Id == id))
            {
                return BadRequest(ModelState);
            }

            StockTaking stockTaking = new StockTaking
            {
                Id = 0,
                StockTakingItems = (from warehouse in _context.Warehouses
                                    join section in _context.Sections on warehouse.Id equals section.WarehouseId
                                    join position in _context.Positions on section.Id equals position.SectionId
                                    join movement in _context.Movements on position.Id equals movement.PositionId
                                    join storedItem in _context.StoredItems on movement.StoredItemId equals storedItem.Id
                                    where section.Id == id
                                    select new StockTakingItem()
                                    {
                                        StockTakingId = 0,
                                        StoredItemId = storedItem.Id,
                                        PositionId = position.Id,
                                        CurrentStock = storedItem.Count(),
                                        CountedStock = 0
                                    }).ToList(),
                WarehouseId = _context.Sections.Where(x => x.Id == id).FirstOrDefault().WarehouseId,
                Area = StockTakingArea.Section
            };

            EntityEntry<StockTaking> entry = await _context.StockTakings.AddAsync(stockTaking);

            return Ok(entry.Entity);
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