using FluentValidation;

namespace Studiekring.Business.Customers.Commands
{
    public class NewCustomerValidator : AbstractValidator<NewCustomerCommand>
    {
        public NewCustomerValidator()
        {
            RuleFor(i => i.Email).NotEmpty().EmailAddress();
        }
    }
}
