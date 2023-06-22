using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Models.DTO;

namespace FnolApiVersion2.O.Profiles
{
    //Automapper between CasedetailsDto and FnolDetails
    public class CaseDetailsProfile : Profile
    {
        public CaseDetailsProfile()
        {
            CreateMap<CaseDetailsDto,FnolDetails>().ReverseMap()
                .ForMember(
                    dest => dest.PolicyID,
                    opt => opt.MapFrom(src => $"{src.PolicyID}")
                )
                .ForMember(
                    dest => dest.UserID,
                    opt => opt.MapFrom(src => $"{src.UserID}")
                )
                .ForMember(
                    dest => dest.FnolID,
                    opt => opt.MapFrom(src => $"{src.FnolID}")
                )
                .ForMember(
                    dest => dest.ReportedDate,
                    opt => opt.MapFrom(src => $"{src.ReportedDate}")
                )
                .ForMember(
                    dest => dest.CaseStatus,
                    opt => opt.MapFrom(src => $"{src.ActiveStatus}")
                )
                .ForMember(
                    dest => dest.ReporterName,
                    opt => opt.MapFrom(src => $"{src.ReporterName}")
                );
        }
        
    }
}