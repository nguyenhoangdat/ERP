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
    public class ReceiptsController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public ReceiptsController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        // GET: api/Receipts/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Receipt>> GetReceipt(long id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindReceiptByIdCommand(id)));
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

        // GET: api/Receipts/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<Receipt>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindReceiptsOnPageCommand(page, itemsPerPage)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Receipts/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Section>> PutReceipt(long id, Receipt receipt)
        {
            if (id != receipt.Id)
            {
                return this.BadRequest();
            }

            try
            {
                List<UpdateReceiptCommand.UpdateReceiptCommandModel.Item> items = new List<UpdateReceiptCommand.UpdateReceiptCommandModel.Item>();
                foreach (Receipt.Item item in receipt.Items)
                {
                    items.Add(new UpdateReceiptCommand.UpdateReceiptCommandModel.Item(item.WareId, item.PositionId, item.ReceiptId, item.CountOrdered, item.CountReceived, item.UtcProcessed, item.EmployeeId));
                }

                receipt = await this.Mediator.Send(new UpdateReceiptCommand(receipt.Id, receipt.UtcExpected, receipt.UtcReceived, items));
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

        // DELETE: api/Receipts/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Receipt>> DeleteReceipt(long id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new DeleteReceiptCommand(id)));
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
