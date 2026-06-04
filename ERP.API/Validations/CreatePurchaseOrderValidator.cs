using ERP.Application.PurchaseOrders.Commands.CreatePurchaseOrder;
using FluentValidation;

namespace ERP.API.Validations
{
    public class CreatePurchaseOrderValidator
     : AbstractValidator<CreatePurchaseOrderCommand>
    {
        public CreatePurchaseOrderValidator()
        {
            RuleFor(x => x.VendorName)
                .NotEmpty();

            RuleFor(x => x.Department)
                .NotEmpty();

            RuleFor(x => x.Items)
                .NotEmpty();

            RuleForEach(x => x.Items)
                .ChildRules(item =>
                {
                    item.RuleFor(i => i.ProductName)
                        .NotEmpty();

                    item.RuleFor(i => i.Quantity)
                        .GreaterThan(0);

                    item.RuleFor(i => i.UnitPrice)
                        .GreaterThan(0);
                });
        }
    }
}
