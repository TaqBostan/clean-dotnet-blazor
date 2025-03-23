using CleanDotnetBlazor.Domain.Entities;
using CleanDotnetBlazor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDotnetBlazor.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerBriefDto>();
            CreateMap<Customer, CustomerDto>();
        }
    }
}
