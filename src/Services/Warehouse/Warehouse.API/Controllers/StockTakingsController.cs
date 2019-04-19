using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTakingsController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public StockTakingsController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        // GET: api/StockTakings/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTaking>> Get(int id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindStockTakingByIdCommand(id)));
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
        public async Task<ActionResult<PageDto<StockTaking>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindStockTakingsOnPageCommand(page, itemsPerPage)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get: api/StockTakings/CreateForWarehouse/5
        [HttpGet("CreateForWarehouse/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTaking>> CreateForWarehouse(int id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new CreateStockTakingForWarehouseCommand(id)));
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

        // Get: api/StockTakings/CreateForSection/5
        [HttpGet("CreateForSection/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTaking>> CreateForSection(int id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new CreateStockTakingForSectionCommand(id)));
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

        // DELETE: api/StockTakings/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTaking>> Delete(int id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new DeleteStockTakingCommand(id)));
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
