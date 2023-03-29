using AutoMapper;
using CashRegister.Application.DTO;
using CashRegister.Domain.Models;

namespace CashRegister.Domain.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDTO, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                                            .ForMember(dest => dest.ProductBills, opt => opt.Ignore()); 
            CreateMap<Product, ProductDTO>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name));
                                            
        }
    }
}
