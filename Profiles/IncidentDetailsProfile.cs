using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Models.DTO;


namespace FnolApiVersion2.O.Profiles
{
    //Automapper Implementation between IncidentDetailsDto and IncidentDetailsModel
    public class IncidentDetailsProfile : Profile
    {
        public IncidentDetailsProfile()
        {
            CreateMap<IncidentDetailsDto,IncidentDetails>().ReverseMap()
                .ForMember(
                    dest => dest.CauseOfIncident,
                    opt => opt.MapFrom(src => $"{src.CauseOfIncident}")
                )
                .ForMember(
                    dest => dest.IncidentDate,
                    opt => opt.MapFrom(src => $"{src.IncidentDate}")
                )
                .ForMember(
                    dest => dest.DamagedParts,
                    opt => opt.MapFrom(src => $"{src.DamagedParts}")
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => $"{src.Description}")
                )
                .ForMember(
                    dest => dest.AddressOfIncident,
                    opt => opt.MapFrom(src => $"{src.AddressOfIncident}")
                )
                .ForMember(
                    dest => dest.Landmark,
                    opt => opt.MapFrom(src => $"{src.Landmark}")
            );
        }
        
    }
}