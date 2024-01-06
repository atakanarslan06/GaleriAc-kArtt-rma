using AutoMapper;
using BusinessLayer.Dtos;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            CreateMap<RegisterRequestDTO, ApplicationUser>().ReverseMap();
        }
    }
}
