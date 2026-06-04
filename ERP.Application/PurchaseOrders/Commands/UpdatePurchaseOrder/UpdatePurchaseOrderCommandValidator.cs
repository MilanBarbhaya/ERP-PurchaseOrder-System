using FluentValidation;

namespace ERP.Application.PurchaseOrders.Commands.UpdatePurchaseOrder
{
   

    public class UpdatePurchaseOrderCommandValidator
        : AbstractValidator<UpdatePurchaseOrderCommand>
    {
        public UpdatePurchaseOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.VendorName)
                .NotEmpty()
                .WithMessage("Vendor Name is required");

            RuleFor(x => x.Department)
                .NotEmpty()
                .WithMessage("Department is required");

            RuleFor(x => x.LineItems)
                .NotEmpty()
                .WithMessage("At least one item is required");
        }
    }
}
