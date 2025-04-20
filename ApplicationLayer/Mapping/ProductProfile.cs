using ApplicationLayer.Dtos;
using ApplicationLayer.Dtos.ProductDTOS;
using AutoMapper;
using DomainLayer.Entities;

namespace ApplicationLayer.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<CreateProductDto, Product>();

            CreateMap<UpdateProductDto, Product>();
            
        }
    }
}
