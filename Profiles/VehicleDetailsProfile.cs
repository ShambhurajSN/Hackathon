using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Models.DTO;


namespace FnolApiVersion2.O.Profiles
{
    //Automapper Implementation between VehicleDetailsDto and VehicleDetailsModel
    public class VehicleDetailsProfile : Profile
    {
        public VehicleDetailsProfile()
        {
            CreateMap<VehicleDetailsDto,VehicleDetails>().ReverseMap()
                .ForMember(
                    dest => dest.VehicleRegNumber,
                    opt => opt.MapFrom(src => $"{src.VehicleRegNumber}")
                )
                .ForMember(
                    dest => dest.VehicleMaker,
                    opt => opt.MapFrom(src => $"{src.VehicleMaker}")
                )
                .ForMember(
                    dest => dest.VehicleModel,
                    opt => opt.MapFrom(src => $"{src.VehicleModel}")
                )
                .ForMember(
                    dest => dest.VehicleType,
                    opt => opt.MapFrom(src => $"{src.VehicleType}")
                )
                .ForMember(
                    dest => dest.RCNumber,
                    opt => opt.MapFrom(src => $"{src.RCNumber}")
                );
        }
    }
}