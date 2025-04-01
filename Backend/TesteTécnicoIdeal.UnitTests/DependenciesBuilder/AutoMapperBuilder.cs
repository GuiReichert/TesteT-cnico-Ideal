using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using TesteTécnicoIdeal.API.DTO_s;

namespace TesteTécnicoIdeal.UnitTests.DependenciesBuilder
{
    public class AutoMapperBuilder
    {
        public static IMapper Build()
        {
            return new MapperConfiguration(options =>
            {
            options.AddProfile(new AutoMapperProfile());
            }).CreateMapper();
        }
    }
}
