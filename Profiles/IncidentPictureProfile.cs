using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Models.DTO;


namespace FnolApiVersion2.O.Profiles
{
    //Automapper Implementation between IncidentPictureDto and IncidentPictureModel
    public class IncidentPictureProfile : Profile
    {
        public IncidentPictureProfile()
        {
            CreateMap<IncidentPictureDto,IncidentPicture>().ReverseMap()
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