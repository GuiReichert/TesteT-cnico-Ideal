using AutoMapper;
using TesteTécnicoIdeal.API.Models;

namespace TesteTécnicoIdeal.API.DTO_s
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>();
        }
    }
}
