using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.PurchaseOrders.Commands.GetPurchaseOrder
{
    public class GetPurchaseOrderByIdHandler
    : IRequestHandler<GetPurchaseOrderByIdQuery, PurchaseOrder?>
    {
        private readonly IPurchaseOrderRepository _repository;

        public GetPurchaseOrderByIdHandler(
            IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<PurchaseOrder?> Handle(
            GetPurchaseOrderByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
