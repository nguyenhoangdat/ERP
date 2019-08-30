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

        // GET: api/IssueSlipItems/5/1
        [HttpGet("{issueSlipId}/{positionId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO.ItemDTO>> GetIssueSlipItem(long issueSlipId, long positionId, int wareId)
        {
            try
            {
                IssueSlip.Item item = await this.Mediator.Send(new FindIssueSlipItemByIdCommand(issueSlipId, positionId, wareId));
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

        // GET: api/IssueSlipItems/MoveToBin/1/7/20
        [HttpGet("MoveToBin/{issueSlipId}/{positionId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO.ItemDTO>> GetMoveToBin(long issueSlipId, long? positionId, int wareId)
        {
            try
            {
                IssueSlip.Item entity = await this.Mediator.Send(new MoveIssueSlipItemToBinCommand(issueSlipId, positionId, wareId, false));
                return this.Ok(this.Mapper.Map<IssueSlipDTO.ItemDTO>(entity));
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (EntityMoveToBinException ex)
            {
                return this.Conflict(ex.Message);
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
        [HttpGet("Restore/{issueSlipId}/{positionId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO.ItemDTO>> GetRestore(long issueSlipId, long positionId, int wareId)
        {
            try
            {
                IssueSlip.Item entity = await this.Mediator.Send(new RestoreIssueSlipItemFromBinCommand(issueSlipId, positionId, wareId));
                return this.Ok(this.Mapper.Map<IssueSlipDTO.ItemDTO>(entity));
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (EntityRestoreFromBinException ex)
            {
                return this.Conflict(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/IssueSlipItems/GetNextToBeProcessedInSectionByIssueSlipId/1/40
        [HttpGet("GetNextToBeProcessedInSectionByIssueSlipId/{sectionId}/{issueSlipId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO.ItemDTO>> GetNextToBeProcessedInSectionByIssueSlipId(int sectionId, long issueSlipId)
        {
            try
            {
                IssueSlip.Item entity = await this.Mediator.Send(new FindIssueSlipItemToProcessInSectionByIssueSlipIdCommand(sectionId, issueSlipId));
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

        // GET: api/IssueSlipItems/AssignIssueSlipItemToPosition/1/7/20
        [HttpGet("AssignIssueSlipItemToPosition/{issueSlipId}/{positionId}/{wareId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO.ItemDTO>> GetAssignIssueSlipItemToPosition(long issueSlipId, long positionId, int wareId)
        {
            try
            {
                IssueSlip.Item entity = await this.Mediator.Send(new AssignIssueSlipItemToPositionCommand(issueSlipId, positionId, wareId));
                return this.Ok(this.Mapper.Map<IssueSlipDTO.ItemDTO>(entity));
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (PositionAlreadyAssignedException ex)
            {
                return this.Conflict(ex.Message);
            }
            catch (FullyAssignedException ex)
            {
                return this.Conflict(ex.Message);
            }
            catch (PositionWareConflictException ex)
            {
                return this.Conflict(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/IssueSlipItems/IssueUnitsForIssueSlipItem/1/7/20/3
        [HttpGet("IssueUnitsForIssueSlipItem/{issueSlipId}/{positionId}/{wareId}/{count}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO.ItemDTO>> GetIssueUnitsForIssueSlipItem(long issueSlipId, long positionId, int wareId, int count)
        {
            try
            {
                IssueSlip.Item entity = await this.Mediator.Send(new IssueUnitsForIssueSlipItemCommand(issueSlipId, wareId, positionId, count));
                return this.Ok(this.Mapper.Map<IssueSlipDTO.ItemDTO>(entity));
            }
            catch (EntityNotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
            catch (IssueSlipItemPositionAvailableUnitsException ex)
            {
                return this.Conflict(ex.Message);
            }
            catch (UnitsExceededException ex)
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
