using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restmium.ERP.Services.Warehouse.API.Models;
using Restmium.ERP.Services.Warehouse.API.Models.Application;
using Restmium.ERP.Services.Warehouse.API.Models.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Restmium.ERP.Services.Warehouse.Infrastructure.Database;
using System;
using System.Linq;
using System.Threading.Tasks;
using Entities = Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }
        protected DatabaseContext DatabaseContext { get; } //TODO: Remove as soon as the problem with multiple warehouses is solved

        public WarehousesController(IMapper mapper, IMediator mediator, DatabaseContext context)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
            this.DatabaseContext = context;
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WarehouseDTO>> GetWarehouse(int id)
        {
            try
            {
                Entities.Warehouse warehouse = await this.Mediator.Send(new FindWarehouseByIdCommand(id));
                return this.Ok(this.Mapper.Map<WarehouseDTO>(warehouse));
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

        // GET: api/Warehouses/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<WarehouseDTO>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                Page<Entities.Warehouse> entity = await this.Mediator.Send(new FindWarehousesOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<WarehouseDTO>>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Warehouses
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)] //TODO: Warehouse RESTRICTION
        [ProducesResponseType(500)]
        public async Task<ActionResult<WarehouseDTO>> PostWarehouse(WarehouseDTO warehouse)
        {
            if (this.DatabaseContext.Warehouses.Count() > 0)
            {
                return this.StatusCode(403); //TODO: Warehouse RESTRICTION
            }

            this.ModelState.Remove("Id");
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                Entities.Warehouse warehouseEntity = this.Mapper.Map<WarehouseDTO, Entities.Warehouse>(warehouse);
                warehouseEntity = await this.Mediator.Send(new CreateWarehouseCommand(warehouseEntity));

                warehouse = this.Mapper.Map<Entities.Warehouse, WarehouseDTO>(warehouseEntity);
                return this.CreatedAtAction("GetWarehouse", new { id = warehouse.Id }, warehouse);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Warehouses/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutWarehouse(int id, WarehouseDTO warehouse)
        {
            if (id != warehouse.Id)
            {
                return this.BadRequest();
            }

            try
            {
                Entities.Warehouse warehouseEntity = this.Mapper.Map<WarehouseDTO, Entities.Warehouse>(warehouse);
                await this.Mediator.Send(new UpdateWarehouseCommand(warehouseEntity));
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

        // DELETE: api/Warehouses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WarehouseDTO>> DeleteWarehouse(int id)
        {
            try
            {
                Entities.Warehouse warehouse = await this.Mediator.Send(new DeleteWarehouseCommand(id));
                return this.Ok(this.Mapper.Map<WarehouseDTO>(warehouse));
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

        // GET: api/Warehouses/WarehouseCurrentCapacity/3
        [HttpGet("WarehouseCurrentCapacity/{warehouseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WarehouseCapacityDTO>> GetWarehouseCurrentCapacity(int warehouseId)
        {
            try
            {
                WarehouseCapacity capacity = await this.Mediator.Send(new GetWarehouseCurrentCapacityCommand(warehouseId));
                return this.Ok(this.Mapper.Map<WarehouseCapacityDTO>(capacity));
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

        // GET: api/Warehouses/GetByPositionId/3
        [HttpGet("GetByPositionId/{positionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WarehouseDTO>> GetByPositionId(long positionId)
        {
            try
            {
                Entities.Warehouse warehouse = await this.Mediator.Send(new FindWarehouseByPositionIdCommand(positionId));

                return this.Ok(this.Mapper.Map<WarehouseDTO>(warehouse));
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
