using AutoMapper;
using Bank.Shared;
using MediatR;
using NServiceBus;

namespace Bank.API.Commands
{
    public class UpdateAccountBalanceCommandHandler : IRequestHandler<UpdateAccountBalanceCommand, ServiceResult<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IMessageSession _messageSession;
        private readonly IConfiguration _config;

        public UpdateAccountBalanceCommandHandler(IMapper mapper, IMessageSession messageSession, IConfiguration config)
        {
            _mapper = mapper;
            _config = config;
            _messageSession = messageSession;
        }

        public async Task<ServiceResult<bool>> Handle(UpdateAccountBalanceCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<Core.Contract.Commands.UpdateAccountBalance>(request);

            await _messageSession.Send(_config.GetValue<string>("BankCoreWorkerService:AccountServiceEndpoint"), command);

            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
