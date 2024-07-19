using AutoMapper;
using FixedDemo.Application.User.Commands;

namespace FixedDemo.Application.User.Mapping
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Domain.Entities.User, Core.Dtos.User.UserDto>();
            CreateMap<RegisterUserCommand, Domain.Entities.User>()
                .ForMember(m => m.PasswordSalt, opt => opt.Ignore())
                .ForMember(m => m.PasswordHash, opt => opt.Ignore());
        }
    }
}
