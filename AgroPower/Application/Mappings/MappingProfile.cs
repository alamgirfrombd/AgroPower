using AutoMapper;
using AgroPower.Domain.Entities;
using AgroPower.DTOs;

namespace AgroPower.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<ProductCategory, ProductCategoryReadDto>();
            CreateMap<ProductCategoryCreateDto, ProductCategory>();

            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductReadDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
