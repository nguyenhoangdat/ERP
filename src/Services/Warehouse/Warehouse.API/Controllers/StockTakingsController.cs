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
    public class StockTakingsController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }

        public StockTakingsController(IMapper mapper, IMediator mediator)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
        }

        // GET: api/StockTakings/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTakingDTO>> Get(int id)
        {
            try
            {
                StockTaking stockTaking = await this.Mediator.Send(new FindStockTakingByIdCommand(id));
                return this.Ok(this.Mapper.Map<StockTakingDTO>(stockTaking));
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

        // GET: api/StockTakings/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<StockTakingDTO>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                Page<StockTaking> entity = await this.Mediator.Send(new FindStockTakingsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<StockTakingDTO>>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get: api/StockTakings/CreateForWarehouse/5/false
        [HttpGet("CreateForWarehouse/{warehouseId}/{includeEmptyPositions}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTakingDTO>> GetCreateForWarehouse(int warehouseId, bool includeEmptyPositions)
        {
            try
            {
                StockTaking stockTaking = await this.Mediator.Send(new CreateStockTakingForWarehouseCommand(warehouseId, includeEmptyPositions));
                return this.Ok(this.Mapper.Map<StockTakingDTO>(stockTaking));
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

        // Get: api/StockTakings/CreateForSection/5/false
        [HttpGet("CreateForSection/{sectionId}/{includeEmptyPositions}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTakingDTO>> GetCreateForSection(int sectionId, bool includeEmptyPositions)
        {
            try
            {
                StockTaking stockTaking = await this.Mediator.Send(new CreateStockTakingForSectionCommand(sectionId, includeEmptyPositions));
                return this.Ok(this.Mapper.Map<StockTakingDTO>(stockTaking));
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

        // GET: api/StockTakings/MoveToBin/1
        [HttpGet("MoveToBin/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTakingDTO>> GetMoveToBin(int id)
        {
            try
            {
                StockTaking entity = await this.Mediator.Send(new MoveStockTakingToBinCommand(id, false));
                return this.Ok(this.Mapper.Map<StockTakingDTO>(entity));
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (EntityMoveToBinException ex)
            {
                return this.Conflict(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/StockTakings/Restore/1
        [HttpGet("Restore/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTakingDTO>> GetRestore(int id)
        {
            try
            {
                StockTaking entity = await this.Mediator.Send(new RestoreStockTakingFromBinCommand(id));
                return this.Ok(this.Mapper.Map<StockTakingDTO>(entity));
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (EntityRestoreFromBinException ex)
            {
                return this.Conflict(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/StockTakings/Deleted/1/20
        [HttpGet("Deleted/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<StockTakingDTO>>> GetDeleted(int page, int itemsPerPage)
        {
            try
            {
                Page<StockTaking> entity = await this.Mediator.Send(new FindDeletedStockTakingsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<StockTakingDTO>>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/StockTakings/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTakingDTO>> Delete(int id)
        {
            try
            {
                StockTaking stockTaking = await this.Mediator.Send(new DeleteStockTakingCommand(id));
                return this.Ok(this.Mapper.Map<StockTakingDTO>(stockTaking));
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
