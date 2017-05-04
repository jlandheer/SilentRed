using FluentValidation;

namespace SilentRed.WebCore.Customers
{
    public class BeginCustomerValidator : AbstractValidator<NewCustomerCommand>
    {
        public BeginCustomerValidator()
        {
            RuleFor(i => i.Email).NotEmpty().EmailAddress();
        }
    }
}
