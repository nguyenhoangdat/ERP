using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restmium.ERP.Services.Warehouse.Application.Commands;
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
        public async Task<ActionResult<Position>> GetPosition(long id)
        {
            try
            {
                Position position = await this.Mediator.Send(new FindPositionByIdCommand(id));
                return position;
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

        // POST: api/Positions
        [HttpPost]
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
        public async Task<ActionResult<Position>> DeletePosition(long id)
        {
            try
            {
                Position position = await this.Mediator.Send(new DeletePositionCommand(id));
                return position;
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
        public async Task<ActionResult<Position>> Relocate(long fromId, long toId)
        {
            try
            {
                Position position = await this.Mediator.Send(new RelocatePositionCommand(fromId, toId));
                return position;
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (PositionEmptyException ex)
            {
                return this.BadRequest(ex.Message);
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
