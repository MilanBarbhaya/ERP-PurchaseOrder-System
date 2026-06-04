using ERP.Domain.Entities;
using MediatR;

namespace ERP.Application.PurchaseOrders.Commands.GetPurchaseOrder
{
    public record GetPurchaseOrderByIdQuery(string Id)
      : IRequest<PurchaseOrder?>;
}
