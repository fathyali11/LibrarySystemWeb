using AutoMapper;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.Entities;


namespace LibrarySystem.Domain.Mappings;
public class BookMapping:Profile
{
    public BookMapping()
    {
        CreateMap<BookRequest, Book>()
            .ForMember(dest => dest.Title, option => option.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
            .ForMember(dest => dest.Quantity, option => option.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.PriceForBuy, option => option.MapFrom(src => src.PriceForBuy))
            .ForMember(dest => dest.PriceForBorrow, option => option.MapFrom(src => src.PriceForBorrow))
            .ForMember(dest => dest.CategoryId, option => option.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.AuthorId, option => option.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.IsAvailable, option => option.Ignore())
            .ForMember(dest => dest.PublishedDate, option => option.Ignore())
            .ForMember(dest => dest.Id, option => option.Ignore())
            .ReverseMap();

        CreateMap<Book, BookResponse>()
            .ForMember(dest => dest.Title, option => option.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
            .ForMember(dest => dest.Quantity, option => option.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.PriceForBuy, option => option.MapFrom(src => src.PriceForBuy))
            .ForMember(dest => dest.PriceForBorrow, option => option.MapFrom(src => src.PriceForBorrow))
            .ForMember(dest => dest.CategoryId, option => option.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.AuthorId, option => option.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.IsAvailable, option => option.Ignore())
            .ForMember(dest => dest.PublishedDate, option => option.Ignore())
            .ForMember(dest => dest.id, option => option.MapFrom(src => src.Id));
    }
}
