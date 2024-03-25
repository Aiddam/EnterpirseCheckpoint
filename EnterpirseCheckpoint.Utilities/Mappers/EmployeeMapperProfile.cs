using AutoMapper;
using EnterpriseCheckpoint.Models.DTOs;
using EnterpriseCheckpoint.Models.Models;

namespace EnterpirseCheckpoint.Utilities.Mappers
{
    public class EmployeeMapperProfile : Profile
    {
        public EmployeeMapperProfile() 
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(ed => ed.Name, config => config.MapFrom(e => e.User.Name))                
                .ForMember(ed => ed.Surname, config => config.MapFrom(e => e.User.Surname))                
                .ReverseMap();
        }
    }
}
