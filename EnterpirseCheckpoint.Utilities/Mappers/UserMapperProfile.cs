
using AutoMapper;
using EnterpriseCheckpoint.Models.Models;

namespace EnterpirseCheckpoint.Utilities.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
