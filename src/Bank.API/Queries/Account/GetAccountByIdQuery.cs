using Bank.Core.Contract.Models;
using Bank.Shared;
using MediatR;

namespace Bank.API.Queries.Account
{
    public class GetAccountByIdQuery : IRequest<ServiceResult<AccountModel>>
    {
        public Guid AccountId { get; set; }
    }
}
