using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;
using Entities = Restmium.ERP.Services.Warehouse.Domain.Entities;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public WarehousesController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Entities.Warehouse>> GetWarehouse(int id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindWarehouseByIdCommand(id)));
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

        // POST: api/Warehouses
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Entities.Warehouse>> PostWarehouse(Entities.Warehouse warehouse)
        {
            this.ModelState.Remove("Id");
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                warehouse = await this.Mediator.Send(new CreateWarehouseCommand(warehouse));
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
        public async Task<ActionResult<Entities.Warehouse>> PutWarehouse(int id, Entities.Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return this.BadRequest();
            }

            try
            {
                warehouse = await this.Mediator.Send(new UpdateWarehouseCommand(warehouse));
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
        public async Task<ActionResult<Entities.Warehouse>> DeleteWarehouse(int id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new DeleteWarehouseCommand(id)));
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
                return this.Ok(await this.Mediator.Send(new GetWarehouseCurrentCapacityCommand(warehouseId)));
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
