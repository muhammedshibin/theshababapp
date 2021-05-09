using API.Dtos;
using AutoMapper;
using Core.CoreDtos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransactionDetail, TransactionDto>()
                .ForMember(dest => dest.PaidParty, m => m.MapFrom(src => src.PaidParty.Name))
                .ForMember(dest => dest.Category, m => m.MapFrom(src => src.Category.Name));

            CreateMap<TransactionDto, TransactionDetail>()
                .ForMember(dest => dest.Category , m => m.Ignore())
                .ForMember(dest => dest.PaidParty, m => m.Ignore());


            CreateMap<InmateDto, Inmate>()
                .ReverseMap();

            CreateMap<Inmate, InmateToReturnDto>();

            CreateMap<Vendor, VendorDto>()
                .ReverseMap();

            CreateMap<InmateBill, BillDto>()
                .ForMember(dest => dest.Month , opt => opt.MapFrom(src => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(src.Month)))
                .ForMember(dest => dest.PaymentStatus , opt => opt.MapFrom(src => src.PaymentStatus.GetHashCode()))
                .ForMember(dest => dest.Inmate, opt => opt.MapFrom(src => src.Inmate.FullName));

            CreateMap<BillDetail, BillDetailDto>();  

        }
    }
}
