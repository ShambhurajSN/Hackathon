using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FnolApiVersion2.O.Models.Domains;
using FnolApiVersion2.O.Models.DTO;

namespace FnolApiVersion2.O.Profiles
{
    //Automapper Implementation between UserRegistrationDto and UserModel
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationDto,User>().ReverseMap()
            .ForMember(
                dest => dest.FirstName,
                opt => opt.MapFrom(src => $"{src.FirstName}")
            )
            .ForMember(
                dest => dest.LastName,
                opt => opt.MapFrom(src => $"{src.LastName}")
            )
            .ForMember(
                dest => dest.EmailAddress,
                opt => opt.MapFrom(src => $"{src.EmailAddress}")
            )
            .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => $"{src.UserName}")
            )
            .ForMember(
                dest => dest.Password,
                opt => opt.MapFrom(src => $"{src.Password}")
            );
        }
    }
}