using ERP.Application.Interfaces;
using ERP.Application.PurchaseOrders.Commands.ApprovePurchaseOrder;
using ERP.Application.PurchaseOrders.Commands.CreatePurchaseOrder;
using ERP.Application.PurchaseOrders.Commands.GetPurchaseOrder;
using ERP.Application.PurchaseOrders.Commands.RejectPurchaseOrder;
using ERP.Application.PurchaseOrders.Commands.SubmitPurchaseOrder;
using ERP.Application.PurchaseOrders.Commands.UpdatePurchaseOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [ApiController]
    [Route("api/purchase-orders")]
    public class PurchaseOrdersController
    : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrdersController(
            IMediator mediator,IPurchaseOrderRepository purchaseOrderRepository)
        {
            _mediator = mediator;
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        [Authorize(Roles = "ProcurementOfficer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]
            CreatePurchaseOrderCommand command)
        {
            var id =
                await _mediator.Send(command);

            return CreatedAtAction(
                nameof(Create),
                new { id },
                id);
        }
        [Authorize(Roles = "ProcurementOfficer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody]
            UpdatePurchaseOrderCommand command)
        {
            var updatedCommand = command with { Id = id };

            var result = await _mediator.Send(updatedCommand);

            //return Ok(result);
            return Ok(new
            {
                Id = result
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string? status,string? search)
        {
            var result =
                await _purchaseOrderRepository.GetFilteredAsync(
                    status,
                    search);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(
                new GetPurchaseOrderByIdQuery(id));

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [Authorize(Roles = "ProcurementOfficer")]
        [HttpPost("{id}/submit")]
        public async Task<IActionResult> Submit(string id)
        {
            var result =
                await _mediator.Send(
                    new SubmitPurchaseOrderCommand(id));

            return Ok(result);
        }

        [Authorize(Roles = "FinanceManager")]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> Approve(string id)
        {
            var result =
                await _mediator.Send(
                    new ApprovePurchaseOrderCommand(id));

            return Ok(result);
        }

        [Authorize(Roles = "FinanceManager")]
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(string id)
        {
            var result =
                await _mediator.Send(
                    new RejectPurchaseOrderCommand(id));

            return Ok(result);
        }
    }
}
