using ERP.Application.Interfaces;
using ERP.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.PurchaseOrders.Commands.RejectPurchaseOrder
{
    public class RejectPurchaseOrderHandler
    : IRequestHandler<
        RejectPurchaseOrderCommand,
        Unit>
    {
        private readonly IPurchaseOrderRepository _repository;

        public RejectPurchaseOrderHandler(
            IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RejectPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var po = await _repository.GetByIdAsync(request.Id);

            if (po == null)
                throw new Exception("PO not found");

            if (po.Status != PurchaseOrderStatus.Submitted)
                throw new Exception("Only Submitted PO can be approved/rejected");

            po.Status = PurchaseOrderStatus.Rejected;

            await _repository.UpdateAsync(po);

            return Unit.Value;
        }
    }
}
