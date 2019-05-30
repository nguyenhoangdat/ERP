using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restmium.ERP.Services.Warehouse.API.Models.Application;
using Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTakingItemsController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }

        public StockTakingItemsController(IMapper mapper, IMediator mediator)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
        }

        // GET: api/StockTakingItems/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<StockTakingDTO.ItemDTO>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                Page<StockTaking.Item> entity = await this.Mediator.Send(new FindStockTakingItemsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<StockTakingDTO.ItemDTO>>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/StockTakingItems/5
        [HttpPut("{stockTakingId}/{positionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutStockTakingItem(int stockTakingId, long positionId, StockTakingDTO.ItemDTO item)
        {
            if (stockTakingId != item.StockTakingId || positionId != item.PositionId)
            {
                return this.BadRequest();
            }

            try
            {
                await this.Mediator.Send(new UpdateStockTakingItemCommand(item.StockTakingId, item.WareId, item.PositionId, item.CurrentStock, item.CountedStock, item.UtcCounted));
                return this.NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/StockTakingItems/MoveToBin/1/20
        [HttpGet("MoveToBin/{stockTakingId}/{positionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTakingDTO.ItemDTO>> GetMoveToBin(int stockTakingId, long positionId)
        {
            try
            {
                StockTaking.Item entity = await this.Mediator.Send(new MoveStockTakingItemToBinCommand(positionId, stockTakingId));
                return this.Ok(this.Mapper.Map<StockTakingDTO.ItemDTO>(entity));
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/StockTakingItems/Deleted/1/20
        [HttpGet("Deleted/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<StockTakingDTO.ItemDTO>>> GetDeleted(int page, int itemsPerPage)
        {
            try
            {
                Page<StockTaking.Item> entity = await this.Mediator.Send(new FindDeletedStockTakingItemsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<StockTakingDTO.ItemDTO>>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
