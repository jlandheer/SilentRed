using FluentValidation;
using SilentRed.WebCore.Customers.Commands;

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
