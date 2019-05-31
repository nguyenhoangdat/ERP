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
    public class IssueSlipItemsController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }

        public IssueSlipItemsController(IMapper mapper, IMediator mediator)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
        }

        // GET: api/IssueSlipItems/5
        [HttpGet("{issueSlipId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO.ItemDTO>> GetIssueSlipItem(long issueSlipId, int wareId)
        {
            try
            {
                IssueSlip.Item item = await this.Mediator.Send(new FindIssueSlipItemByIssueSlipIdAndWareIdCommand(issueSlipId, wareId));
                return this.Ok(this.Mapper.Map<IssueSlipDTO.ItemDTO>(item));
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

        // GET: api/IssueSlipItems/All/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<IssueSlipDTO.ItemDTO>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                Page<IssueSlip.Item> entity = await this.Mediator.Send(new FindIssueSlipItemsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<IssueSlipDTO.ItemDTO>>(entity));
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
        public async Task<IActionResult> PutIssueSlipItem(long issueSlipId, int wareId, IssueSlipDTO.ItemDTO item)
        {
            if (issueSlipId != item.IssueSlipId || wareId != item.WareId)
            {
                return this.BadRequest();
            }

            try
            {
                await this.Mediator.Send(new UpdateIssueSlipItemCommand(item.WareId, item.IssueSlipId, item.PositionId, item.IssuedUnits));
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

        // GET: api/IssueSlipItems/MoveToBin/1/20
        [HttpGet("MoveToBin/{issueSlipId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO.ItemDTO>> GetMoveToBin(long issueSlipId, int wareId)
        {
            try
            {
                IssueSlip.Item entity = await this.Mediator.Send(new MoveIssueSlipItemToBinCommand(wareId, issueSlipId));
                return this.Ok(this.Mapper.Map<IssueSlipDTO.ItemDTO>(entity));
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

        // GET: api/IssueSlipItems/Deleted/1/20
        [HttpGet("Deleted/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<IssueSlipDTO.ItemDTO>>> GetDeleted(int page, int itemsPerPage)
        {
            try
            {
                Page<IssueSlip.Item> entity = await this.Mediator.Send(new FindDeletedIssueSlipItemsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<IssueSlipDTO.ItemDTO>>(entity));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/IssueSlipItems/Restore/1/20
        [HttpGet("Restore/{issueSlipId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO.ItemDTO>> GetRestore(long issueSlipId, int wareId)
        {
            try
            {
                IssueSlip.Item entity = await this.Mediator.Send(new RestoreIssueSlipItemFromBinCommand(wareId, issueSlipId));
                return this.Ok(this.Mapper.Map<IssueSlipDTO.ItemDTO>(entity));
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
