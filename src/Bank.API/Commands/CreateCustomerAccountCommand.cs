using Bank.Shared;
using FluentValidation;
using MediatR;

namespace Bank.API.Commands
{
    public record CreateCustomerAccountCommand : IRequest<ServiceResult<bool>>
    {
        public Guid CustomerId { get; set; }
        public string IBAN { get; set; }
        public string Name { get; set; }
        public CurrencyType Currency { get; set; }
        public decimal Balance { get; set; }
    }

    public class CreateCustomerAccountCommandValidator : AbstractValidator<CreateCustomerAccountCommand>
    {
        public CreateCustomerAccountCommandValidator()
        {
            RuleFor(c => c.IBAN).NotEmpty().MaximumLength(32).WithMessage("IBAN cannot be empty and max length 32");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(c => c.Currency).NotNull().WithMessage("Currency could be GALLEON, SICKLET or KNUT");
            RuleFor(c => c.Balance).GreaterThan(0).WithMessage("Balance cannot be negative");
        }
    }
}
