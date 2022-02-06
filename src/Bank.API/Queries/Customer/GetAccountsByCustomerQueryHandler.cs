using Bank.Core.Services;
using Bank.Core.Contract.Models;
using Bank.Shared;
using MediatR;

namespace Bank.API.Queries.Customer
{
    public class GetAccountsByCustomerQueryHandler : IRequestHandler<GetAccountsByCustomerQuery, ServiceResult<List<AccountModel>>>
    {
        private readonly CustomerService _customerService;

        public GetAccountsByCustomerQueryHandler(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<ServiceResult<List<AccountModel>>> Handle(GetAccountsByCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _customerService.GetCustomerAccountsById(request.CustomerId);
        }
    }
}
