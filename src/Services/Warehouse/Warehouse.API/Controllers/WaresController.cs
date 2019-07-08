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
    public class WaresController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }

        public WaresController(IMapper mapper, IMediator mediator)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
        }

        // GET: api/Wares/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WareDTO>> GetWare(int id)
        {
            try
            {
                Ware ware = await this.Mediator.Send(new FindWareByIdCommand(id));
                return this.Ok(this.Mapper.Map<WareDTO>(ware));
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
        public async Task<ActionResult<PageDTO<WareDTO>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                Page<Ware> entity = await this.Mediator.Send(new FindWaresOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<WareDTO>>(entity));
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
        public async Task<ActionResult<WareDTO>> PostWare(WareDTO ware)
        {
            this.ModelState.Remove("Id");
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                Ware entity = await this.Mediator.Send(new CreateWareCommand(ware.ProductId, ware.ProductName));
                return this.CreatedAtAction("GetWare", new { id = ware.Id }, this.Mapper.Map<WareDTO>(entity));
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
        public async Task<IActionResult> PutWare(int id, WareDTO ware)
        {
            if (id != ware.Id)
            {
                return this.BadRequest();
            }

            try
            {
                await this.Mediator.Send(new UpdateWareCommand(ware.Id, ware.ProductName, ware.Width, ware.Height, ware.Depth, ware.Weight));
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

        // GET: api/Wares/MoveToBin/1
        [HttpGet("MoveToBin/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WareDTO>> GetMoveToBin(int id)
        {
            try
            {
                Ware entity = await this.Mediator.Send(new MoveWareToBinCommand(id));
                return this.Ok(this.Mapper.Map<WareDTO>(entity));
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

        // GET: api/Wares/Restore/1
        [HttpGet("Restore/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WareDTO>> GetRestore(int id)
        {
            try
            {
                Ware entity = await this.Mediator.Send(new RestoreWareFromBinCommand(id));
                return this.Ok(this.Mapper.Map<WareDTO>(entity));
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

        // GET: api/Wares/Deleted/1/20
        [HttpGet("Deleted/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<WareDTO>>> GetDeleted(int page, int itemsPerPage)
        {
            try
            {
                Page<Ware> entity = await this.Mediator.Send(new FindDeletedWaresOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<WareDTO>>(entity));
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
        public async Task<ActionResult<WareDTO>> DeleteWare(int id)
        {
            try
            {
                Ware ware = await this.Mediator.Send(new DeleteWareCommand(id));
                return this.Ok(this.Mapper.Map<WareDTO>(ware));
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
        public async Task<ActionResult<IEnumerable<WareDTO>>> GetWaresInWarehouse(int warehouseId)
        {
            try
            {
                IEnumerable<Ware> wares = await this.Mediator.Send(new FindWaresInWarehouseCommand(warehouseId));
                return this.Ok(this.Mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares));
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
        public async Task<ActionResult<IEnumerable<WareDTO>>> GetWaresInSection(int sectionId)
        {
            try
            {
                IEnumerable<Ware> wares = await this.Mediator.Send(new FindWaresInSectionCommand(sectionId));
                return this.Ok(this.Mapper.Map<IEnumerable<Ware>, IEnumerable<WareDTO>>(wares));
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

        [HttpGet("GetWareAvailabilityInWarehouse/{wareId}/{warehouseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WareAvailabilityInWarehouseDTO>> GetWareAvailabilityInWarehouse(int wareId, int warehouseId)
        {
            try
            {
                WareAvailabilityInWarehouse availabilityInWarehouse = await this.Mediator.Send(new GetWareAvailabilityInWarehouseCommand(wareId, warehouseId));
                return this.Ok(this.Mapper.Map<WareAvailabilityInWarehouse, WareAvailabilityInWarehouseDTO>(availabilityInWarehouse));
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

        [HttpGet("GetWareAvailabilityInSection/{wareId}/{sectionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<WareAvailabilityInSectionDTO>> GetWareAvailabilityInSection(int wareId, int sectionId)
        {
            try
            {
                WareAvailabilityInSection wareAvailabilityInSection = await this.Mediator.Send(new GetWareAvailabilityInSectionCommand(wareId, sectionId));
                return this.Ok(this.Mapper.Map<WareAvailabilityInSection, WareAvailabilityInSectionDTO>(wareAvailabilityInSection));
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
