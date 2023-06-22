using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Models.DTO;


namespace FnolApiVersion2.O.Profiles
{
    //Automapper Implementation between FnolDetailsDto and FnolDetailsModel
    public class FnolDetailsProfile : Profile
    {
        public FnolDetailsProfile()
        {
            CreateMap<FnolDetailsDto,FnolDetails>().ReverseMap()
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
        }
        
    }
}