using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibrarySystem.Domain.DTO.Reviews;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Mappings;
public class ReviewMappings:Profile
{
    public ReviewMappings()
    {
        CreateMap<ReviewRequest,Review>()
            .ForMember(dest=>dest.Comment,options=>options.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Rating, options => options.MapFrom(src => src.Rating))
            .ForMember(dest => dest.BookId, options => options.MapFrom(src => src.BookId));

        CreateMap<Review, ReviewResponse>()
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.Comment, options => options.MapFrom(src => src.Comment))
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Rating, options => options.MapFrom(src => src.Rating))
            .ForMember(dest => dest.BookId, options => options.MapFrom(src => src.BookId));
    }
}
