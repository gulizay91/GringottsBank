using AutoMapper;
using Bank.Core.Services;
using Bank.Core.Contract.Models;
using Bank.Shared;
using MediatR;
using NServiceBus;

namespace Bank.API.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ServiceResult<bool>>
    {
        private readonly CustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IMessageSession _messageSession;
        private readonly IConfiguration _config;

        public CreateCustomerCommandHandler(IMessageSession messageSession, CustomerService customerService, IMapper mapper, IConfiguration config)
        {
            _customerService = customerService;
            _mapper = mapper;
            _messageSession = messageSession;
            _config = config;
        }

        public async Task<ServiceResult<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // sync with appservice
            var command = _mapper.Map<Core.Contract.Commands.CreateCustomer>(request);
            //return await _customerService.CreateCustomer(command);

            // Send the command
            await _messageSession.Send(_config.GetValue<string>("BankCoreWorkerService:CustomerServiceEndpoint"), command)
                .ConfigureAwait(false);
            return ServiceResult<bool>.SuccessResult(true, "Request has been received.");
        }
    }
}
