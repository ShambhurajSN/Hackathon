using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Models.DTO;


namespace FnolApiVersion2.O.Profiles
{
    //Automapper Implementation between FnolDetailsDto and FnolDto
    public class FnolProfile : Profile
    {
        public FnolProfile()
        {
            CreateMap<FnolDto,FnolDetailsDto>()
                .ForMember(
                    dest => dest.PolicyID,
                    opt => opt.MapFrom(src => $"{src.PolicyID}")
                )
                .ForMember(
                    dest => dest.ReporterName,
                    opt => opt.MapFrom(src => $"{src.ReporterName}")
                )
                .ForMember(
                    dest => dest.ReportedDate,
                    opt => opt.MapFrom(src => $"{src.ReportedDate}")
                );
            CreateMap<FnolDto,IncidentDetailsDto>()
                .ForMember(
                    dest => dest.CauseOfIncident,
                    opt => opt.MapFrom(src => $"{src.CauseOfIncident}")
                )
                .ForMember(
                    dest => dest.DamagedParts,
                    opt => opt.MapFrom(src => $"{src.DamagedParts}")
                )
                .ForMember(
                    dest => dest.AddressOfIncident,
                    opt => opt.MapFrom(src => $"{src.AddressOfIncident}")
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => $"{src.Description}")
                )
                .ForMember(
                    dest => dest.IncidentDate,
                    opt => opt.MapFrom(src => $"{src.IncidentDate}")
                )
                .ForMember(
                    dest => dest.Landmark,
                    opt => opt.MapFrom(src => $"{src.Landmark}")
                );
            CreateMap<FnolDto,DriverDetailsDto>()
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
            CreateMap<FnolDto,VehicleDetailsDto>()
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
            CreateMap<FnolDto,IncidentPictureDto>()
                .ForMember(
                    dest => dest.FrontImage,
                    opt => opt.MapFrom(src => $"{src.FrontImage}")
                )
                .ForMember(
                    dest => dest.BackImage,
                    opt => opt.MapFrom(src => $"{src.BackImage}")
                )
                .ForMember(
                    dest => dest.LeftImage,
                    opt => opt.MapFrom(src => $"{src.LeftImage}")
                )
                .ForMember(
                    dest => dest.RightImage,
                    opt => opt.MapFrom(src => $"{src.RightImage}")
                );
        }
    }
}