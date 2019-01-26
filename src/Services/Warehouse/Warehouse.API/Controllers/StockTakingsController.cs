using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restmium.ERP.Services.Warehouse.Application.Commands;
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
        public async Task<ActionResult<StockTaking>> Get(int id)
        {
            try
            {
                return await this.Mediator.Send(new FindStockTakingByIdCommand(id));
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

        // Get: api/StockTakings/CreateForWarehouse/5
        [HttpGet("CreateForWarehouse/{id}")]
        public async Task<ActionResult<StockTaking>> CreateForWarehouse(int id)
        {
            try
            {
                return await this.Mediator.Send(new CreateStockTakingForWarehouseCommand(id));
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
        public async Task<ActionResult<StockTaking>> CreateForSection(int id)
        {
            try
            {
                return await this.Mediator.Send(new CreateStockTakingForSectionCommand(id));
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
        public async Task<ActionResult<StockTaking>> Delete(int id)
        {
            try
            {
                return await this.Mediator.Send(new DeleteStockTakingCommand(id));
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
