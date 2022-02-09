using RealEstateManager.Models.Requests;
using FluentValidation;

namespace RealEstateManager.Host.Validators
{
    public class ApartmentRequestValidator : AbstractValidator<ApartmentRequest>
    {
        public ApartmentRequestValidator()
        {
            RuleFor(x => x.Location).NotEmpty().NotNull().MinimumLength(2).MaximumLength(15);

            RuleFor(x => x.Price).GreaterThan(0);


        }
    }
}
