using ERP.Application.Interfaces;
using ERP.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.PurchaseOrders.Commands.SubmitPurchaseOrder
{
    public class SubmitPurchaseOrderHandler
    : IRequestHandler<
        SubmitPurchaseOrderCommand,
        bool>
    {
        private readonly IPurchaseOrderRepository _repository;

        public SubmitPurchaseOrderHandler(
            IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            SubmitPurchaseOrderCommand request,
            CancellationToken cancellationToken)
        {
            var po =
                await _repository.GetByIdAsync(
                    request.Id);

            if (po == null)
                return false;

            if (po.Status != PurchaseOrderStatus.Draft)
                return false;

            po.Status =
                PurchaseOrderStatus.Submitted;

            await _repository.UpdateAsync(po);

            return true;
        }
    }

}