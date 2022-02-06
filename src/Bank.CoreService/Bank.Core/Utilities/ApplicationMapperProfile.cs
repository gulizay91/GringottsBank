using AutoMapper;

namespace Bank.Core.Utilities
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            CreateMap<Aggregates.Account, Contract.Models.AccountModel>();
            CreateMap<Aggregates.Customer, Contract.Models.CustomerModel>()
                .ForMember(dest => dest.CustomerAccounts, opt => opt.MapFrom(src => src.Accounts));
        }
    }
}
