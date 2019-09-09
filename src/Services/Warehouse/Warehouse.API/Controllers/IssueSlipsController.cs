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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueSlipsController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IMediator Mediator { get; }

        public IssueSlipsController(IMapper mapper, IMediator mediator)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
        }

        // GET: api/IssueSlips/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO>> GetIssueSlip(long id)
        {
            try
            {
                IssueSlip issueSlip = await this.Mediator.Send(new FindIssueSlipByIdCommand(id));
                return this.Ok(this.Mapper.Map<IssueSlipDTO>(issueSlip));
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

        // GET: api/IssueSlips/All/1/20
        [HttpGet("All/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<IssueSlipDTO>>> GetAll(int page, int itemsPerPage)
        {
            try
            {
                Page<IssueSlip> entity = await this.Mediator.Send(new FindIssueSlipsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<IssueSlipDTO>>(entity));
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
        public async Task<IActionResult> PutIssueSlip(long id, IssueSlipDTO issueSlip)
        {
            if (id != issueSlip.Id)
            {
                return this.BadRequest();
            }

            try
            {
                await this.Mediator.Send(new UpdateIssueSlipCommand(issueSlip.Id, issueSlip.UtcDispatchDate, issueSlip.UtcDeliveryDate));
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

        // GET: api/IssueSlips/MoveToBin/1
        [HttpGet("MoveToBin/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO>> GetMoveToBin(long id)
        {
            try
            {
                IssueSlip entity = await this.Mediator.Send(new MoveIssueSlipToBinCommand(id, false));
                return this.Ok(this.Mapper.Map<IssueSlipDTO>(entity));
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

        // GET: api/IssueSlips/Restore/1
        [HttpGet("Restore/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO>> GetRestore(long id)
        {
            try
            {
                IssueSlip entity = await this.Mediator.Send(new RestoreIssueSlipFromBinCommand(id));
                return this.Ok(this.Mapper.Map<IssueSlipDTO>(entity));
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

        // GET: api/IssueSlips/Deleted/1/20
        [HttpGet("Deleted/{page}/{itemsPerPage}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PageDTO<IssueSlipDTO>>> GetDeleted(int page, int itemsPerPage)
        {
            try
            {
                Page<IssueSlip> entity = await this.Mediator.Send(new FindDeletedIssueSlipsOnPageCommand(page, itemsPerPage));
                return this.Ok(this.Mapper.Map<PageDTO<IssueSlipDTO>>(entity));
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
        public async Task<ActionResult<IssueSlipDTO>> DeleteIssueSlip(long id)
        {
            try
            {
                IssueSlip issueSlip = await this.Mediator.Send(new DeleteIssueSlipCommand(id));
                return this.Ok(this.Mapper.Map<IssueSlipDTO>(issueSlip));
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
        [HttpGet("NextIssueSlipToProcessInSection/{sectionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IssueSlipDTO>> GetNextToBeProcessedInSection(int sectionId)
        {
            try
            {
                IssueSlip issueSlip = await this.Mediator.Send(new FindIssueSlipToProcessInSectionCommand(sectionId));
                return this.Ok(this.Mapper.Map<IssueSlipDTO>(issueSlip));
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
        [HttpGet("IssueSlipsForOrderWithId/{orderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<IssueSlipDTO>>> GetIssueSlipsForOrderWithId(long orderId)
        {
            try
            {
                IEnumerable<IssueSlip> issueSlips = await this.Mediator.Send(new FindIssueSlipsByOrderIdCommand(orderId));
                return this.Ok(this.Mapper.Map<IEnumerable<IssueSlip>, IEnumerable<IssueSlipDTO>>(issueSlips));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/IssueSlips/IssueSlipsForOrderWithIdInWarehouse/5
        [HttpGet("IssueSlipsForOrderWithIdInWarehouse/{orderId}/{warehouseId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<IssueSlipDTO>>> GetIssueSlipsForOrderWithIdInWarehouse(long orderId, int warehouseId)
        {
            try
            {
                IEnumerable<IssueSlip> issueSlips = await this.Mediator.Send(new FindIssueSlipsByOrderIdAndWarehouseIdCommand(orderId, warehouseId));
                return this.Ok(this.Mapper.Map<IEnumerable<IssueSlip>, IEnumerable<IssueSlipDTO>>(issueSlips));
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
