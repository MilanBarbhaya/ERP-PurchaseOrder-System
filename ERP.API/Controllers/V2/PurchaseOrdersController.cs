using Asp.Versioning;
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

namespace ERP.API.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/purchase-orders")]
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

        
        [HttpGet]
        public async Task<IActionResult> GetAll(string? status,string? search)
        {
            var result =
                await _purchaseOrderRepository.GetFilteredAsync(
                    status,
                    search);

            return Ok(result.Select(c => new
            {
                c.PoNumber,
                c.VendorName,
                c.LineItems,
                c.Status,
                Department = "this is v2 demo test"
            }));
        }

    }
}
