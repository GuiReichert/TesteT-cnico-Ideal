using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TesteTécnicoIdeal.WPF.Models;

namespace TesteTécnicoIdeal.WPF.DTO_s
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User_Model>();
            CreateMap<User_Model, UserDTO>();
        }
    }
}
