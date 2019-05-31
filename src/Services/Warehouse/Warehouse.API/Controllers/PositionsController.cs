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
    public class PositionsController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }

        public PositionsController(IMapper mapper, IMediator mediator)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PositionDTO>> GetPosition(long id)
        {
            try
            {
                Position position = await this.Mediator.Send(new FindPositionByIdCommand(id));
                return this.Ok(this.Mapper.Map<PositionDTO>(position));
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

        // GET: api/Positions/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<PositionDTO>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                Page<Position> entity = await this.Mediator.Send(new FindPositionsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<PositionDTO>>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Positions
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PositionDTO>> PostPosition(PositionDTO position)
        {
            this.ModelState.Remove("Id");
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                Position entity = await this.Mediator.Send(new CreatePositionCommand(position.Name, position.Width, position.Height, position.Depth, position.MaxWeight, position.SectionId));
                return this.CreatedAtAction("GetPosition", new { id = position.Id }, this.Mapper.Map<Position, PositionDTO>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Positions/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutPosition(long id, PositionDTO position)
        {
            if (id != position.Id)
            {
                return this.BadRequest();
            }

            try
            {
                await this.Mediator.Send(new UpdatePositionCommand(position.Id, position.Name, position.Width, position.Height, position.Depth, position.MaxWeight, position.ReservedUnits));
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

        // GET: api/Positions/MoveToBin/1
        [HttpGet("MoveToBin/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PositionDTO>> GetMoveToBin(long id)
        {
            try
            {
                Position entity = await this.Mediator.Send(new MovePositionToBinCommand(id));
                return this.Ok(this.Mapper.Map<PositionDTO>(entity));
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

        // GET: api/Positions/Restore/1
        [HttpGet("Restore/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PositionDTO>> GetRestore(long id)
        {
            try
            {
                Position entity = await this.Mediator.Send(new RestorePositionFromBinCommand(id));
                return this.Ok(this.Mapper.Map<PositionDTO>(entity));
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

        // GET: api/Positions/Deleted/1/20
        [HttpGet("Deleted/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<PositionDTO>>> GetDeleted(int page, int itemsPerPage)
        {
            try
            {
                Page<Position> entity = await this.Mediator.Send(new FindDeletedPositionsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<PositionDTO>>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PositionDTO>> DeletePosition(long id)
        {
            try
            {
                Position position = await this.Mediator.Send(new DeletePositionCommand(id));
                return this.Ok(this.Mapper.Map<PositionDTO>(position));
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

        // GET: api/Positions/
        [HttpGet("Relocate/{fromId}/{toId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PositionDTO>> Relocate(long fromId, long toId)
        {
            try
            {
                Position position = await this.Mediator.Send(new RelocatePositionCommand(fromId, toId));
                return this.Ok(this.Mapper.Map<Position, PositionDTO>(position));
            }
            catch (PositionEmptyException ex)
            {
                return this.BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (PositionWareConflictException ex)
            {
                return this.Conflict(ex.Message);
            }
            catch (PositionLoadCapacityException ex)
            {
                return this.Conflict(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
