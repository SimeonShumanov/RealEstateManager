using RealEstateManager.Models.Requests;
using FluentValidation;

namespace RealEstateManager.Host.Validators
{
    public class warehouseRequestValidator : AbstractValidator<warehouseRequest>
    {
        public warehouseRequestValidator()
        {
            RuleFor(x => x.Location).NotEmpty().NotNull().MinimumLength(2).MaximumLength(15);

            RuleFor(x => x.Price).GreaterThan(0);


        }
    }
}
