using Bank.Shared;
using FluentValidation;
using MediatR;

namespace Bank.API.Commands
{
    public record CreateCustomerCommand : IRequest<ServiceResult<bool>>
    {
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string IdentityNumber { get; set; }

    }

    public class CustomerCreateCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CustomerCreateCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("FirstName cannot be empty");
            RuleFor(c => c.FamilyName).NotEmpty().WithMessage("FamilyName cannot be empty");
            RuleFor(c => c.IdentityNumber).NotEmpty().MaximumLength(11).WithMessage("IdentityNumber cannot be empty and max length 11");
        }
    }
}
