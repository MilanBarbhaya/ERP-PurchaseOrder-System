using ERP.Domain.Entities;
using MediatR;

namespace ERP.Application.PurchaseOrders.Commands.GetPurchaseOrder
{
    public record GetPurchaseOrdersQuery()
    : IRequest<List<PurchaseOrder>>;
}
