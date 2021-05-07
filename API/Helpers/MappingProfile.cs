using API.Dtos;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
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
                //.ForMember(dest => dest.TransactionDate , m => m.MapFrom(src => src.TransactionDate.Date))
                .ForMember(dest => dest.Category , m => m.Ignore())
                .ForMember(dest => dest.PaidParty, m => m.Ignore());


            CreateMap<InmateDto, Inmate>()
                .ReverseMap();

            CreateMap<Vendor, VendorDto>()
                .ReverseMap();


        }
    }
}
