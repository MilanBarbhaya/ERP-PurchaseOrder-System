using ERP.Application.Interfaces;
using ERP.Application.PurchaseOrders.Commands.UpdatePurchaseOrder;
using ERP.Domain.Entities;
using ERP.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.PurchaseOrders.Commands.CreatePurchaseOrder
{
    public class UpdatePurchaseOrderHandler
    : IRequestHandler<UpdatePurchaseOrderCommand, string>
    {
        private readonly IPurchaseOrderRepository _repository;

        public UpdatePurchaseOrderHandler(
            IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var po = await _repository.GetByIdAsync(request.Id);

            if (po == null)
                throw new Exception("PO not found");

            // 🔴 BUSINESS RULE 1
            if (po.Status != PurchaseOrderStatus.Draft)
                throw new Exception("Only Draft PO can be edited");

            // update fields
            po.VendorName = request.VendorName;
            po.Department = request.Department;
            po.LineItems = request.LineItems.Select(x =>
                     new LineItem
                     {
                         ProductName = x.ProductName,
                         Quantity = x.Quantity,
                         UnitPrice = x.UnitPrice
                     }).ToList();

            await _repository.UpdateAsync(po);

            return po.Id;
        }
    }
}
