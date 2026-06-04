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
    public class GetPurchaseOrdersHandler
      : IRequestHandler<GetPurchaseOrdersQuery, List<PurchaseOrder>>
    {
        private readonly IPurchaseOrderRepository _repository;

        public GetPurchaseOrdersHandler(
            IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PurchaseOrder>> Handle(
            GetPurchaseOrdersQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
