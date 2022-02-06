using AutoMapper;
using Bank.Shared;
using MediatR;
using NServiceBus;

namespace Bank.API.Commands
{
    public class CreateCustomerAccountCommandHandler : IRequestHandler<CreateCustomerAccountCommand, ServiceResult<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IMessageSession _messageSession;
        private readonly IConfiguration _config;

        public CreateCustomerAccountCommandHandler(IMapper mapper, IMessageSession messageSession, IConfiguration config)
        {
            _mapper = mapper;
            _config = config;
            _messageSession = messageSession;
        }

        public async Task<ServiceResult<bool>> Handle(CreateCustomerAccountCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<Core.Contract.Commands.CreateCustomerAccount>(request);

            await _messageSession.Send(_config.GetValue<string>("BankCoreWorkerService:AccountServiceEndpoint"), command);

            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
