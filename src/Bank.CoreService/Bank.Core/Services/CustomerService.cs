using AutoMapper;
using Bank.Core.Aggregates;
using Bank.Core.Persistence;
using Bank.Core.Contract.Commands;
using Bank.Core.Contract.Models;
using Bank.Shared;
using Microsoft.Extensions.Logging;

namespace Bank.Core.Services
{
    public class CustomerService
    {
        public ILogger<CustomerService> _logger { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper { get; set; }
        public CustomerService(IUnitOfWork unitOfWork, ILogger<CustomerService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CustomerModel>> CreateCustomer(CreateCustomer request)
        {
            try
            {
                Customer newCustomer = new Customer(Guid.NewGuid(), request.IdentityNumber, request.FirstName, request.FamilyName);
                var result = await _unitOfWork.CustomerRepository.AddAsync(newCustomer);
                await _unitOfWork.SaveChangesAsync();
                var viewModel = _mapper.Map<CustomerModel>(result);
                return ServiceResult<CustomerModel>.SuccessResult(viewModel);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"{nameof(CreateCustomer)}: {request.IdentityNumber} - Message:{ex.Message}");
                return ServiceResult<CustomerModel>.ErrorResult(ex.Message, null);
            }
        }

        public async Task<ServiceResult<CustomerModel>> GetCustomerById(Guid id)
        {
            try
            {
                var result = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
                if(result == null)
                    ServiceResult<CustomerModel>.ErrorResult("Customer not found", null, System.Net.HttpStatusCode.NotFound);

                var viewModel = _mapper.Map<CustomerModel>(result);
                return ServiceResult<CustomerModel>.SuccessResult(viewModel);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"{nameof(GetCustomerById)}: {id} - Message:{ex.Message}");
                return ServiceResult<CustomerModel>.ErrorResult(ex.Message, null);
            }
        }

        public async Task<ServiceResult<List<AccountModel>>> GetCustomerAccountsById(Guid id)
        {
            try
            {
                var result = await _unitOfWork.CustomerRepository.GetWithAllAccountsByIdAsync(id);
                if (result == null)
                    return ServiceResult<List<AccountModel>>.ErrorResult("Customer not found", null, System.Net.HttpStatusCode.NotFound);

                var viewModel = _mapper.Map<List<AccountModel>>(result.Accounts);
                if(!result.Accounts.Any())
                   return ServiceResult<List<AccountModel>>.SuccessResult(viewModel, "No accounts found for customer", System.Net.HttpStatusCode.NoContent);

                return ServiceResult<List<AccountModel>>.SuccessResult(viewModel);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"{nameof(GetCustomerAccountsById)}: {id} - Message:{ex.Message}");
                return ServiceResult<List<AccountModel>>.ErrorResult(ex.Message, null);
            }
        }
    }
}
