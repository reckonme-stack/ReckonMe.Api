using AutoMapper;
using ReckonMe.Api.Dtos;
using ReckonMe.Api.Models;

namespace ReckonMe.Api.Configs
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize() 
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterDto, ApplicationUser>();
                cfg.CreateMap<Wallet, WalletDto>()
                    .ForMember(dest => dest.Id, o => o.ResolveUsing(src => src.Id.ToString()));
                cfg.CreateMap<AddWalletDto, Wallet>();
                cfg.CreateMap<EditWalletDto, Wallet>();
                cfg.CreateMap<ExpenseDto, Expense>()
                    .ReverseMap();
            })
            .CreateMapper();
    }
}