using NServiceBus;

namespace Bank.Core.Contract.Messages
{
    public class GetAccountDetail : IMessage
    {
        public GetAccountDetail(Guid accountId)
        {
            AccountId = accountId;
        }

        public Guid AccountId { get; protected set; }
    }
}
