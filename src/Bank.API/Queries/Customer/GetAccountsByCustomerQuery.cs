using Bank.Core.Contract.Models;
using Bank.Shared;
using MediatR;

namespace Bank.API.Queries.Customer
{
    public class GetAccountsByCustomerQuery : IRequest<ServiceResult<List<AccountModel>>>
    {
        public Guid CustomerId { get; set; }
    }
}
