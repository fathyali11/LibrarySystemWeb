using AutoMapper;
using LibrarySystem.Domain.DTO.Author;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Mappings;
public class AuthorMappings:Profile
{
    public AuthorMappings()
    {
        CreateMap<AuthorRequest, Author>()
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
            .ForMember(dest => dest.Biography, option => option.MapFrom(src => src.Biography))
            .ReverseMap();

        CreateMap<Author, AuthorResponse>()
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
            .ForMember(dest => dest.Biography, option => option.MapFrom(src => src.Biography))
            .ForMember(dest => dest.IsDeleted, option => option.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id));

        CreateMap<Author, AuthorWithBooksResponse>()
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
            .ForMember(dest => dest.Biography, option => option.MapFrom(src => src.Biography))
            .ForMember(dest => dest.IsDeleted, option => option.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
            .ForMember(dest => dest.Books, option => option.MapFrom(src => src.Books.Select(x=>new {
                x.Id,
                x.Title,
                x.Description,
                x.Quantity,
                x.PriceForBuy,
                x.PriceForBorrow,
                x.PublishedDate,
                x.IsAvailable,
                x.CategoryId
            })));

    }
}
