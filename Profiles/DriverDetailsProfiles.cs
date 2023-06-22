using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Models.DTO;


namespace FnolApiVersion2.O.Profiles
{
    //Automapper Implementation between DriverDetailsDto and DriverDetailsModel
    public class DriverDetailsProfile : Profile
    {
        public DriverDetailsProfile()
        {
            CreateMap<DriverDetailsDto,DriverDetails>().ReverseMap()
                .ForMember(
                    dest => dest.DriverName,
                    opt => opt.MapFrom(src => $"{src.DriverName}")
                )
                .ForMember(
                    dest => dest.DriverLicenseType,
                    opt => opt.MapFrom(src => $"{src.DriverLicenseType}")
                )
                .ForMember(
                    dest => dest.DriverLicenseNumber,
                    opt => opt.MapFrom(src => $"{src.DriverLicenseNumber}")
            );
        }
    }
}