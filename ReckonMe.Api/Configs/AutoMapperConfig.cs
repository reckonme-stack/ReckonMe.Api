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
            })
            .CreateMapper();
    }
}