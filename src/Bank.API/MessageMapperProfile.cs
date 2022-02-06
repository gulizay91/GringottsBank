using AutoMapper;
using Bank.API.Commands;
using Bank.API.Queries.Account;

namespace Bank.API
{
    public class MessageMapperProfile : Profile
    {
        public MessageMapperProfile()
        {
            CreateMap<CreateCustomerAccountCommand, Core.Contract.Commands.CreateCustomerAccount>();
            CreateMap<CreateCustomerCommand, Core.Contract.Commands.CreateCustomer>();
            CreateMap<UpdateAccountBalanceCommand, Core.Contract.Commands.UpdateAccountBalance>();

            CreateMap<GetAccountByIdQuery, Core.Contract.Messages.GetAccountDetail>();
        }
    }
}
