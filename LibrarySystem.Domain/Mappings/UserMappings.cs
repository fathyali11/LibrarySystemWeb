﻿using AutoMapper;
using LibrarySystem.Domain.DTO.ApplicationUsers;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Mappings
{
    public class UserMappings:Profile
    {
        public UserMappings()
        {
            CreateMap<RegistersRequest, ApplicationUser>()
                .ForMember(dest => dest.FirstName, option => option.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, option => option.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Address, option => option.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, option => option.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, option => option.MapFrom(src => src.UserName))
                .ReverseMap();

            CreateMap<ApplicationUser, AuthResponse>()
                .ForMember(dest => dest.FirstName, option => option.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, option => option.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Address, option => option.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, option => option.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, option => option.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<AccountUserRequest, ApplicationUser>()
                .ForMember(dest => dest.FirstName, option => option.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, option => option.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Address, option => option.MapFrom(src => src.Address))
                .ReverseMap();

            CreateMap<ApplicationUser,AccountUserResponse>()
                .ForMember(dest => dest.FirstName, option => option.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, option => option.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Address, option => option.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, option => option.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, option => option.MapFrom(src => src.UserName))
                .ReverseMap();

        }
    }
}
