using AutoMapper;
using AgroPower.Domain.Entities;
using AgroPower.DTOs;

namespace AgroPower.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ProductCategory এবং DTO-এর মধ্যে ম্যাপিং
            CreateMap<ProductCategory, ProductCategoryReadDto>();
            CreateMap<ProductCategoryCreateDto, ProductCategory>();

            // ProductCreateDto -> Product এবং Product -> ProductReadDto-এর ম্যাপিং
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductReadDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

      

            // Product -> ProductUpdateDto এবং ProductUpdateDto -> Product-এর ম্যাপিং
            CreateMap<Product, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // কেবল নন-নাল মানকে কপি করবে
        }
    }
}
