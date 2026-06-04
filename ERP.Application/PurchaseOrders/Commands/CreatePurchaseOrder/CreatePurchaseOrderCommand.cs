using ERP.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.PurchaseOrders.Commands.CreatePurchaseOrder
{
    public record CreatePurchaseOrderCommand(
     string VendorName,
     string Department,
     List<LineItemDto> Items
 ) : IRequest<string>;
}
