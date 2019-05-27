using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restmium.ERP.Services.Warehouse.API.Models.Application;
using Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities;
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

        // Get: api/StockTakings/CreateForWarehouse/5
        [HttpGet("CreateForWarehouse/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTakingDTO>> CreateForWarehouse(int id)
        {
            try
            {
                StockTaking stockTaking = await this.Mediator.Send(new CreateStockTakingForWarehouseCommand(id));
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

        // Get: api/StockTakings/CreateForSection/5
        [HttpGet("CreateForSection/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StockTakingDTO>> CreateForSection(int id)
        {
            try
            {
                StockTaking stockTaking = await this.Mediator.Send(new CreateStockTakingForSectionCommand(id));
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
