using FluentValidation;
namespace ERP.Application.PurchaseOrders.Commands.CreatePurchaseOrder
{

    public class CreatePurchaseOrderCommandValidator
        : AbstractValidator<CreatePurchaseOrderCommand>
    {
        public CreatePurchaseOrderCommandValidator()
        {
            RuleFor(x => x.VendorName)
                .NotEmpty()
                .WithMessage("Vendor Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Department)
                .NotEmpty()
                .WithMessage("Department is required");

            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("At least one item is required");

            RuleForEach(x => x.Items)
                .ChildRules(item =>
                {
                    item.RuleFor(i => i.ProductName)
                        .NotEmpty()
                        .WithMessage("Product Name is required");

                    item.RuleFor(i => i.Quantity)
                        .GreaterThan(0)
                        .WithMessage("Quantity must be greater than zero");

                    item.RuleFor(i => i.UnitPrice)
                        .GreaterThan(0)
                        .WithMessage("Unit Price must be greater than zero");
                });
        }
    }
}
