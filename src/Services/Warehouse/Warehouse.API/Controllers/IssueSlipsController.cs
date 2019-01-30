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
    public class IssueSlipsController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public IssueSlipsController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        // GET: api/IssueSlips/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlip>> GetIssueSlip(long id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindIssueSlipByIdCommand(id)));
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

        // PUT: api/IssueSlips/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Section>> PutIssueSlip(long id, IssueSlip issueSlip)
        {
            if (id != issueSlip.Id)
            {
                return this.BadRequest();
            }

            try
            {
                List<UpdateIssueSlipCommand.UpdateIssueSlipCommandModel.Item> items = new List<UpdateIssueSlipCommand.UpdateIssueSlipCommandModel.Item>();
                foreach (IssueSlip.Item item in issueSlip.Items)
                {
                    items.Add(new UpdateIssueSlipCommand.UpdateIssueSlipCommandModel.Item(item.IssueSlipId, item.WareId, item.PositionId, item.RequestedUnits, item.IssuedUnits));
                }

                issueSlip = await this.Mediator.Send(new UpdateIssueSlipCommand(issueSlip.Id, issueSlip.OrderId, issueSlip.UtcDispatchDate, issueSlip.UtcDeliveryDate, items));
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

        // DELETE: api/IssueSlips/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlip>> DeleteIssueSlip(long id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new DeleteIssueSlipCommand(id)));
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

        // GET: api/IssueSlips/NextIssueSlipToProcessInSection/1
        [HttpGet("NextIssueSlipToProcessInSection/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlip>> GetNextToBeProcessedInSection(int id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindIssueSlipToProcessInSectionCommand(id)));
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

        // GET: api/IssueSlips/IssueSlipsForOrderWithId/5
        [HttpGet("IssueSlipsForOrderWithId/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<IssueSlip>>> GetIssueSlipsForOrderWithId(long id)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindIssueSlipsByOrderIdCommand(id)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/IssueSlips/IssueSlipsForOrderWithIdInWarehouse/5
        [HttpGet("IssueSlipsForOrderWithIdInWarehouse/{id}/{warehouseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<IssueSlip>>> GetIssueSlipsForOrderWithIdInWarehouse(long id, int warehouseId)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindIssueSlipsByOrderIdAndWarehouseIdCommand(id, warehouseId)));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
