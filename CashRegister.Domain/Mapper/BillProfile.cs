using AutoMapper;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Models;

namespace CashRegister.Domain.Mapper
{
    public class BillProfile : Profile
    {
        public BillProfile()
        {
            CreateMap<BillDTO, Bill>().ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalBillPrice))
                                            .ForMember(dest => dest.ProductBills, opt => opt.Ignore());
            CreateMap<Bill, BillDTO>().ForMember(dest => dest.TotalBillPrice, opt => opt.MapFrom(src => src.TotalPrice));
            CreateMap<BillPostPutDTO, Bill>().ForMember(dest => dest.ProductBills, opt => opt.Ignore())
                                            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());
        }
    }
}
