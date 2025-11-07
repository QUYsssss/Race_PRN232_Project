using AutoMapper;
using Race_PRN232_Project.DTOs;
using Race_PRN232_Project.Models;

namespace Race_PRN232_Project.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<Race, RaceDTO>();
            CreateMap<SupportTeam, SupportTeamDTO>();
        }
    }
}
