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
    public class SectionsController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }

        public SectionsController(IMapper mapper, IMediator mediator)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
        }

        // GET: api/Sections/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SectionDTO>> GetSection(int id)
        {
            try
            {
                Section section = await this.Mediator.Send(new FindSectionByIdCommand(id));
                return this.Ok(this.Mapper.Map<SectionDTO>(section));
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
        public async Task<ActionResult<PageDTO<SectionDTO>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                Page<Section> entity = await this.Mediator.Send(new FindSectionsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<SectionDTO>>(entity));
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
        public async Task<ActionResult<SectionDTO>> PostSection(SectionDTO section)
        {
            this.ModelState.Remove("Id");
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                Section entity = await this.Mediator.Send(new CreateSectionCommand(section.Name, section.WarehouseId));
                return this.CreatedAtAction("GetSection", new { id = section.Id }, this.Mapper.Map<SectionDTO>(entity));
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
        public async Task<IActionResult> PutSection(int id, SectionDTO section)
        {
            if (id != section.Id)
            {
                return this.BadRequest();
            }

            try
            {
                await this.Mediator.Send(new UpdateSectionCommand(section.Id, section.Name));
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

        // GET: api/Sections/MoveToBin/1
        [HttpGet("MoveToBin/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SectionDTO>> GetMoveToBin(int id)
        {
            try
            {
                Section entity = await this.Mediator.Send(new MoveSectionToBinCommand(id));
                return this.Ok(this.Mapper.Map<SectionDTO>(entity));
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

        // GET: api/Sections/Restore/1
        [HttpGet("Restore/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<SectionDTO>> GetRestore(int id)
        {
            try
            {
                Section entity = await this.Mediator.Send(new RestoreSectionFromBinCommand(id));
                return this.Ok(this.Mapper.Map<SectionDTO>(entity));
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

        // GET: api/Sections/Deleted/1/20
        [HttpGet("Deleted/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<SectionDTO>>> GetDeleted(int page, int itemsPerPage)
        {
            try
            {
                Page<Section> entity = await this.Mediator.Send(new FindDeletedSectionsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<SectionDTO>>(entity));
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
        public async Task<ActionResult<SectionDTO>> DeleteSection(int id)
        {
            try
            {
                Section section = await this.Mediator.Send(new DeleteSectionCommand(id));
                return this.Ok(this.Mapper.Map<SectionDTO>(section));
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
        public async Task<ActionResult<IEnumerable<SectionDTO>>> GetSectionsInWarehouse(int warehouseId)
        {
            try
            {
                IEnumerable<Section> sections = await this.Mediator.Send(new FindSectionsInWarehouseCommand(warehouseId));
                return this.Ok(this.Mapper.Map<IEnumerable<Section>, IEnumerable<SectionDTO>>(sections));
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
        public async Task<ActionResult<SectionDTO>> GetByPositionId(long positionId)
        {
            try
            {
                Section section = await this.Mediator.Send(new FindSectionByPositionIdCommand(positionId));
                return this.Ok(this.Mapper.Map<SectionDTO>(section));
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
