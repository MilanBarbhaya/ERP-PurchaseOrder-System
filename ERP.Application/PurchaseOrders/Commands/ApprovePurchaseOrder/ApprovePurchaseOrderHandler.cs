using ERP.Application.Interfaces;
using ERP.Application.PurchaseOrders.Commands.RejectPurchaseOrder;
using ERP.Domain.Enums;
using MediatR;
using System.Net.NetworkInformation;

namespace ERP.Application.PurchaseOrders.Commands.ApprovePurchaseOrder
{
    public class ApprovePurchaseOrderHandler
    : IRequestHandler<
        ApprovePurchaseOrderCommand,
        Unit>
    {
        private readonly IPurchaseOrderRepository _repository;

        public ApprovePurchaseOrderHandler(
            IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ApprovePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var po = await _repository.GetByIdAsync(request.Id);

            if (po == null)
                throw new Exception("PO not found");

            // 🔴 BUSINESS RULE 2
            if (po.Status != PurchaseOrderStatus.Submitted)
                throw new Exception("Only Submitted PO can be approved/rejected");

            po.Status = PurchaseOrderStatus.Approved;

            await _repository.UpdateAsync(po);

            return Unit.Value;
        }
    }
}
