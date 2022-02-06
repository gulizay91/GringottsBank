using AutoMapper;
using Bank.Core.Aggregates;
using Bank.Core.Exceptions;
using Bank.Core.Persistence;
using Bank.Core.Contract.Commands;
using Bank.Core.Contract.Models;
using Bank.Shared;
using Microsoft.Extensions.Logging;

namespace Bank.Core.Services
{
    public class AccountService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork, ILogger<AccountService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResult<AccountModel>> CreateCustomerAccount(CreateCustomerAccount request)
        {
            try
            {
                Customer customer = await _unitOfWork.CustomerRepository.GetWithAllAccountsByIdAsync(request.CustomerId);
                Account newAccount = new Account(Guid.NewGuid(), request.CustomerId, request.IBAN, request.Name, request.Currency, request.Balance);
                customer.AddNewAccount(newAccount);
                var result = _unitOfWork.CustomerRepository.Update(customer);
                await _unitOfWork.SaveChangesAsync();
                
                _logger?.LogInformation($"New Customer Account saved.");

                var viewModel = _mapper.Map<AccountModel>(newAccount);
                return ServiceResult<AccountModel>.SuccessResult(viewModel);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"{nameof(CreateCustomerAccount)} - Customer: {request.CustomerId} - Message:{ex.Message}");
                return ServiceResult<AccountModel>.ErrorResult(ex.Message, null);
            }
        }

        public async Task<ServiceResult<AccountModel>> UpdateAccountBalance(UpdateAccountBalance request)
        {
            try
            {
                Guard.Against.NegativeOrZero(request.Amount, nameof(request.Amount));
                Account account = await _unitOfWork.AccountRepository.GetByIdAsync(request.AccountId);
                decimal newBalance = account.Balance;
                if(request.ProcessType == ProcessType.WITHDRAW_MONEY)
                    newBalance += request.Amount;
                else
                    newBalance -= request.Amount;
                
                account.UpdateBalance(newBalance);
                var result = _unitOfWork.AccountRepository.Update(account);
                _unitOfWork.SaveChanges();
                
                _logger?.LogInformation($"{request.AccountId} - Account balance updated.");
                
                var viewModel = _mapper.Map<AccountModel>(result);
                return ServiceResult<AccountModel>.SuccessResult(viewModel);
            }
            catch (UoFUpdateConcurrencyException ex)
            {
                _logger?.LogError($"{nameof(UpdateAccountBalance)} optimistic concurrency - Account: {request.AccountId} - Message:{ex.Message}");
                return ServiceResult<AccountModel>.ErrorResult(ex.Message, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"{nameof(UpdateAccountBalance)} - Account: {request.AccountId} - Message:{ex.Message}");
                return ServiceResult<AccountModel>.ErrorResult(ex.Message, null);
            }
        }

        public async Task<ServiceResult<AccountModel>> GetAccount(Guid id)
        {
            try
            {
                Account account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
                if (account == null)
                    return ServiceResult<AccountModel>.ErrorResult("Account not found", null, System.Net.HttpStatusCode.NotFound);

                var viewModel = _mapper.Map<AccountModel>(account);
                return ServiceResult<AccountModel>.SuccessResult(viewModel);
            }
            catch (Exception ex)
            {
                _logger?.LogError($"{nameof(GetAccount)} - id: {id} - Message:{ex.Message}");
                return ServiceResult<AccountModel>.ErrorResult(ex.Message, null);
            }
        }
    }
}
