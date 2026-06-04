using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.PurchaseOrders.Commands.RejectPurchaseOrder
{
    public record RejectPurchaseOrderCommand(
    string Id)
    : IRequest<Unit>;
}
