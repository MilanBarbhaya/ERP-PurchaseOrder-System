using ERP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Entities
{
    public class PurchaseOrder
    {
        public string Id { get; set; } =
            Guid.NewGuid().ToString();

        public string PoNumber { get; set; } =
            string.Empty;

        public string VendorName { get; set; } =
            string.Empty;

        public string Department { get; set; } =
            string.Empty;

        public List<LineItem> LineItems { get; set; } =
            new();

        public decimal TotalAmount { get; set; }

        public PurchaseOrderStatus Status { get; set; }

        public string CreatedBy { get; set; } =
            string.Empty;

        public DateTime CreatedDate { get; set; }
            = DateTime.UtcNow;
    }
}