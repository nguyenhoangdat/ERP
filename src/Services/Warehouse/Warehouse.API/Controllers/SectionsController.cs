using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public class SectionsController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public SectionsController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        // GET: api/Sections/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Section>> GetSection(int id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindSectionByIdCommand(id)));
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

        // GET: api/Sections/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<Section>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindSectionsOnPageCommand(page, itemsPerPage)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Sections
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Section>> PostSection(Section section)
        {
            this.ModelState.Remove("Id");
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                section = await this.Mediator.Send(new CreateSectionCommand(section.Name, section.WarehouseId));
                return this.CreatedAtAction("GetSection", new { id = section.Id }, section);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Sections/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Section>> PutSection(int id, Section section)
        {
            if (id != section.Id)
            {
                return this.BadRequest();
            }

            try
            {
                section = await this.Mediator.Send(new UpdateSectionCommand(section.Id, section.Name));
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
        public async Task<ActionResult<Section>> DeleteSection(int id)
        {
            try
            {
                Section section = await this.Mediator.Send(new DeleteSectionCommand(id));
                return this.Ok(section);
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

        // GET: api/Sections/5
        [HttpGet("SectionsInWarehouse/{warehouseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Section>> GetSectionsInWarehouse(int warehouseId)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindSectionsInWarehouseCommand(warehouseId)));
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

        [HttpGet("GetByPositionId/{positionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Section>> GetByPositionId(long positionId)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindSectionByPositionIdCommand(positionId)));
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
