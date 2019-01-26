using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restmium.ERP.Services.Warehouse.Application.Commands;
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
        public async Task<ActionResult<Ware>> GetWare(int id)
        {
            try
            {
                return await this.Mediator.Send(new FindWareByIdCommand(id));
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

        // POST: api/Wares
        [HttpPost]
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
        public async Task<ActionResult<Ware>> DeleteWare(int id)
        {
            try
            {
                Ware ware = await this.Mediator.Send(new DeleteWareCommand(id));
                return ware;
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
