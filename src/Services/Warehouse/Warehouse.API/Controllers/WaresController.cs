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
    public class WaresController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public WaresController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        // GET: api/Wares/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Ware>> GetWare(int id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindWareByIdCommand(id)));
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

        // GET: api/Wares/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDto<Ware>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindWaresOnPageCommand(page, itemsPerPage)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Wares
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Ware>> PostWare(Ware ware)
        {
            this.ModelState.Remove("Id");
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                ware = await this.Mediator.Send(new CreateWareCommand(ware.ProductId, ware.ProductName));
                return this.CreatedAtAction("GetWare", new { id = ware.Id }, ware);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Wares/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Ware>> PutWare(int id, Ware ware)
        {
            if (id != ware.Id)
            {
                return this.BadRequest();
            }

            try
            {
                ware = await this.Mediator.Send(new UpdateWareCommand(ware.Id, ware.ProductName, ware.Width, ware.Height, ware.Depth, ware.Weight));
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Ware>> DeleteWare(int id)
        {
            try
            {
                Ware ware = await this.Mediator.Send(new DeleteWareCommand(id));
                return this.Ok(ware);
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

        // GET: api/GetWaresInWarehouse/1
        [HttpGet("GetWaresInWarehouse/{warehouseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Ware>>> GetWaresInWarehouse(int warehouseId)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindWaresInWarehouseCommand(warehouseId)));
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

        // GET: api/GetWaresInWarehouse/1
        [HttpGet("GetWaresInSection/{sectionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Ware>>> GetWaresInSection(int sectionId)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindWaresInSectionCommand(sectionId)));
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

        [HttpGet("GetWareAvailability/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<WareAvailabilityDTO>>> GetWareAvailability(int wareId)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new GetWareAvailabilityCommand(wareId)));
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
