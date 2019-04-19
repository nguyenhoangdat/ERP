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
    public class IssueSlipItemsController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public IssueSlipItemsController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        // GET: api/IssueSlipItems/5
        [HttpGet("{issueSlipId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        // GET: api/IssueSlipItems/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDto<IssueSlip.Item>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                return this.Ok(await this.Mediator.Send(new FindIssueSlipItemsOnPageCommand(page, itemsPerPage)));
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
        [ProducesResponseType(404)]
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
