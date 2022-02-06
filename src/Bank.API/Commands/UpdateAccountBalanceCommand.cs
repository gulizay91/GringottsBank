using Bank.Shared;
using FluentValidation;
using MediatR;

namespace Bank.API.Commands
{
    public record UpdateAccountBalanceCommand : IRequest<ServiceResult<bool>>
    {
        public Guid AccountId { get; set; }
        public ProcessType ProcessType { get; set; }
        public decimal Amount { get; set; }
    }

    public class UpdateAccountBalanceCommandValidator : AbstractValidator<UpdateAccountBalanceCommand>
    {
        public UpdateAccountBalanceCommandValidator()
        {
            RuleFor(c => c.AccountId).NotEmpty().WithMessage("Account cannot be empty");
            RuleFor(c => c.ProcessType).NotNull().WithMessage("ProcessType could be WITHDRAW_MONEY(0) or WITHDRAWAL(1)");
            RuleFor(c => c.Amount).GreaterThan(0).WithMessage("Amount cannot be zero or negative");
        }
    }
}
