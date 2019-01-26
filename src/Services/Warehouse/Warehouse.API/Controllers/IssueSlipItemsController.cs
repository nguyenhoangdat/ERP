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
    public class IssueSlipItemsController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public IssueSlipItemsController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        // GET: api/IssueSlipItems/5
        [HttpGet("{issueSlipId}/{wareId}")]
        public async Task<ActionResult<IssueSlip.Item>> GetIssueSlipItem(long issueSlipId, int wareId)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindIssueSlipItemByIssueSlipIdAndWareIdCommand(issueSlipId, wareId)));
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

        // PUT: api/IssueSlipItems/5
        [HttpPut("{issueSlipId}/{wareId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlip.Item>> PutIssueSlipItem(long issueSlipId, int wareId, IssueSlip.Item item)
        {
            if (issueSlipId != item.IssueSlipId || wareId != item.WareId)
            {
                return this.BadRequest();
            }

            try
            {
                item = await this.Mediator.Send(new UpdateIssueSlipItemCommand(item.WareId, item.IssueSlipId, item.PositionId, item.IssuedUnits));
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
    }
}
