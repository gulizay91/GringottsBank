using AutoMapper;
using Bank.Core.Services;
using Bank.Core.Contract.Models;
using Bank.Shared;
using MediatR;
using NServiceBus;

namespace Bank.API.Queries.Account
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, ServiceResult<AccountModel>>
    {
        private readonly AccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IMessageSession _messageSession;
        private readonly IConfiguration _config;
        public GetAccountByIdQueryHandler(AccountService accountService, IMessageSession messageSession, IConfiguration config, IMapper mapper)
        {
            _accountService = accountService;
            _config = config;
            _messageSession = messageSession;
            _mapper = mapper;
        }

        public async Task<ServiceResult<AccountModel>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            
            //return await _accountService.GetAccount(request.Id);
            // send callbackmessage
            var sendOptions = new SendOptions();
            sendOptions.SetDestination(_config.GetValue<string>("BankCoreWorkerService:CallbacksReceiverEndpoint"));

            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromSeconds(30));

            var message = _mapper.Map<Core.Contract.Messages.GetAccountDetail>(request);
            return await _messageSession.Request<ServiceResult<AccountModel>>(message, sendOptions, source.Token)
                .ConfigureAwait(false);
        }
    }
}
