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

        // GET: api/ReceiptItems/5/2/1
        [HttpGet("{receiptId}/{positionId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ReceiptDTO.ItemDTO>> GetReceiptItem(long receiptId, long? positionId, int wareId)
        {
            try
            {
                Receipt.Item item = await this.Mediator.Send(new FindReceiptItemByIdCommand(receiptId, positionId, wareId));
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

        // GET: api/ReceiptItems/MoveToBin/1/3/20
        [HttpGet("MoveToBin/{receiptId}/{positionId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ReceiptDTO.ItemDTO>> GetMoveToBin(long receiptId, long? positionId, int wareId)
        {
            try
            {
                Receipt.Item entity = await this.Mediator.Send(new MoveReceiptItemToBinCommand(wareId, positionId, receiptId));
                return this.Ok(this.Mapper.Map<ReceiptDTO.ItemDTO>(entity));
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

        // GET: api/ReceiptItems/Restore/1/3/20
        [HttpGet("Restore/{receiptId}/{positionId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ReceiptDTO.ItemDTO>> GetRestore(long receiptId, long? positionId, int wareId)
        {
            try
            {
                Receipt.Item entity = await this.Mediator.Send(new RestoreReceiptItemFromBinCommand(wareId, positionId, receiptId));
                return this.Ok(this.Mapper.Map<ReceiptDTO.ItemDTO>(entity));
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
        public async Task<ActionResult<IEnumerable<PositionCountDTO>>> GetFindPositionsForReceiptItem(long receiptId, int wareId)
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

        [HttpGet("AssignReceiptItemToPosition/{receiptId}/{positionId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ReceiptDTO.ItemDTO>> GetAssignReceiptItemToPosition(long receiptId, long positionId, int wareId)
        {
            try
            {
                Receipt.Item entity = await this.Mediator.Send(new AssignReceiptItemToPositionCommand(receiptId, positionId, wareId));
                return this.Ok(this.Mapper.Map<ReceiptDTO.ItemDTO>(entity));
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

        [HttpGet("StoreUnitsForReceiptItemAtPosition/{receiptId}/{positionId}/{wareId}/{count}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ReceiptDTO.ItemDTO>> GetStoreUnitsForReceiptItemAtPosition(long receiptId, long positionId, int wareId, int count)
        {
            try
            {
                Receipt.Item entity = await this.Mediator.Send(new StoreUnitsForReceiptItemAtPositionCommand(receiptId, positionId, wareId, count));
                return this.Ok(this.Mapper.Map<ReceiptDTO.ItemDTO>(entity));
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
