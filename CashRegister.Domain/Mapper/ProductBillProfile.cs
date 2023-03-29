using AutoMapper;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Models;

namespace CashRegister.Domain.Mapper
{
    public class ProductBillProfile : Profile
    {
        public ProductBillProfile()
        {
            CreateMap<ProductBillDTO, ProductBill>().ForMember(dest => dest.Product, opt => opt.Ignore())
                                            .ForMember(dest => dest.Bill, opt => opt.Ignore());
        }
    }
}
