using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restmium.ERP.Services.Warehouse.Application.Commands;
using Restmium.ERP.Services.Warehouse.Application.Models;
using Restmium.ERP.Services.Warehouse.Domain.Entities;
using Restmium.ERP.Services.Warehouse.Domain.Exceptions;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public PositionsController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        // GET: api/Positions/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Position>> GetPosition(long id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindPositionByIdCommand(id)));;
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
        public async Task<ActionResult<PageDto<Position>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindPositionsOnPageCommand(page, itemsPerPage)));
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
        public async Task<ActionResult<Position>> PostPosition(Position position)
        {
            this.ModelState.Remove("Id");
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            try
            {
                position = await this.Mediator.Send(new CreatePositionCommand(position.Name, position.Width, position.Height, position.Depth, position.MaxWeight, position.SectionId, position.Rating));
                return this.CreatedAtAction("GetPosition", new { id = position.Id }, position);
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
        public async Task<IActionResult> PutPosition(long id, Position position)
        {
            if (id != position.Id)
            {
                return this.BadRequest();
            }

            try
            {
                position = await this.Mediator.Send(new UpdatePositionCommand(position.Id, position.Name, position.Width, position.Height, position.Depth, position.MaxWeight, position.Rating, position.ReservedUnits));
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

        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Position>> DeletePosition(long id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new DeletePositionCommand(id)));
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
        public async Task<ActionResult<Position>> Relocate(long fromId, long toId)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new RelocatePositionCommand(fromId, toId)));
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
