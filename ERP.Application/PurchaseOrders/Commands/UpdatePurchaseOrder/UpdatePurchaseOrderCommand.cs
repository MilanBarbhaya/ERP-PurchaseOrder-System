using ERP.Application.DTOs;
using ERP.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.PurchaseOrders.Commands.UpdatePurchaseOrder
{
    public record UpdatePurchaseOrderCommand(
     string Id,
     string VendorName,
     string Department,
     List<LineItemDto> LineItems
 ) : IRequest<string>;
}
