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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptItemsController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }

        public ReceiptItemsController(IMapper mapper, IMediator mediator)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
        }

        // GET: api/ReceiptItems/5/1
        [HttpGet("{receiptId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ReceiptDTO.ItemDTO>> GetReceiptItem(long receiptId, int wareId)
        {
            try
            {
                Receipt.Item item = await this.Mediator.Send(new FindReceiptItemByReceiptIdAndWareIdCommand(receiptId, wareId));
                return this.Ok(this.Mapper.Map<ReceiptDTO.ItemDTO>(item));
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

        // GET: api/ReceiptItems/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<ReceiptDTO.ItemDTO>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                Page<Receipt.Item> entity = await this.Mediator.Send(new FindReceiptItemsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<ReceiptDTO.ItemDTO>>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/ReceiptItems/5
        [HttpPut("{receiptId}/{wareId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutReceiptItem(long receiptId, int wareId, ReceiptDTO.ItemDTO item)
        {
            if (receiptId != item.ReceiptId || wareId != item.WareId)
            {
                return this.BadRequest();
            }

            try
            {
                await this.Mediator.Send(new UpdateReceiptItemCommand(item.WareId, item.PositionId, item.ReceiptId, item.CountOrdered, item.CountReceived, item.UtcProcessed));
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

        // GET: api/ReceiptItems/Deleted/1/20
        [HttpGet("Deleted/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<ReceiptDTO.ItemDTO>>> GetDeleted(int page, int itemsPerPage)
        {
            try
            {
                Page<Receipt.Item> entity = await this.Mediator.Send(new FindDeletedReceiptItemsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<ReceiptDTO.ItemDTO>>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("FindPositionsForReceiptItem/{receiptId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<PositionCountDTO>>> FindPositionsForReceiptItem(long receiptId, int wareId)
        {
            try
            {
                IEnumerable<PositionCount> counts = await this.Mediator.Send(new FindPositionsForReceiptItemCommand(receiptId, wareId));
                return this.Ok(this.Mapper.Map<IEnumerable<PositionCount>, IEnumerable<PositionCountDTO>>(counts));
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

        [HttpPost("PlaceReceiptItemAtPosition/{receiptId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PositionDTO>> PlaceReceiptItemAtPosition(long receiptId, int wareId, PositionCountDTO positionCount)
        {
            try
            {
                PositionCount count = this.Mapper.Map<PositionCountDTO, PositionCount>(positionCount);
                Position position = await this.Mediator.Send(new StoreReceiptItemAtPositionCommand(receiptId, wareId, count));
                return this.Ok(this.Mapper.Map<PositionDTO>(position));
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
    }
}
