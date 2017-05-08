using FluentValidation;
using FluentValidation.Internal;

namespace Studiekring.Business.Customers.Commands
{
    public class BeginCustomerValidator : AbstractValidator<NewCustomerCommand>
    {
        public BeginCustomerValidator()
        {
            RuleFor(i => i.Email).NotEmpty().EmailAddress();
        RuleForEach(command => )}
    }
}
