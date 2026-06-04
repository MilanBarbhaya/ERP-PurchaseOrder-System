using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using ERP.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.PurchaseOrders.Commands.CreatePurchaseOrder
{
    public class CreatePurchaseOrderHandler
    : IRequestHandler<CreatePurchaseOrderCommand, string>
    {
        private readonly IPurchaseOrderRepository _repository;

        public CreatePurchaseOrderHandler(
            IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(
            CreatePurchaseOrderCommand request,
            CancellationToken cancellationToken)
        {
            var po = new PurchaseOrder
            {
                Id = Guid.NewGuid().ToString(),

                PoNumber =
                    $"PO-{DateTime.UtcNow:yyyyMMddHHmmss}",

                VendorName = request.VendorName,

                Department = request.Department,

                Status = PurchaseOrderStatus.Draft,

                CreatedDate = DateTime.UtcNow,

                LineItems = request.Items.Select(x =>
                    new LineItem
                    {
                        ProductName = x.ProductName,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice
                    }).ToList()
            };

            po.TotalAmount =
                po.LineItems.Sum(x => x.Amount);

            await _repository.CreateAsync(po);

            return po.Id;
        }
    }
}
