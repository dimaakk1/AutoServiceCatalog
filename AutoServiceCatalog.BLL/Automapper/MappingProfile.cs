using AutoMapper;
using AutoServiceCatalog.BLL.DTO;
using AutoServiceCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceCatalog.BLL.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Part, PartDto>().ReverseMap();
            CreateMap<Part, PartCreateDto>().ReverseMap();
            CreateMap<PartCreateDto, Part>();
        }

    }
}
