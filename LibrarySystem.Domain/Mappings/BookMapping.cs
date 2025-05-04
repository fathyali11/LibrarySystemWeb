using AutoMapper;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.Entities;


namespace LibrarySystem.Domain.Mappings;
public class BookMapping:Profile
{
    public BookMapping()
    {
        CreateMap<CreateBookRequest, Book>()
            .ForMember(dest=>dest.Title, option => option.MapFrom(src=>src.Document.FileName))
            .ForMember(dest=>dest.FileContentType, option => option.MapFrom(src=>src.Document.ContentType))
            .ForMember(dest=>dest.FileExtension, option => option.MapFrom(src=> Path.GetExtension(src.Document.FileName)))
            .ForMember(dest => dest.ImageName, option => option.MapFrom(src => src.Image.FileName))
            .ForMember(dest => dest.ImageContentType, option => option.MapFrom(src => src.Image.ContentType))
            .ForMember(dest => dest.ImageExtension, option => option.MapFrom(src => Path.GetExtension(src.Image.FileName)))

            .ReverseMap();

        CreateMap<BookImageRequest, Book>()
            .ForMember(dest => dest.ImageName, option => option.MapFrom(src => src.Image.FileName))
            .ForMember(dest => dest.ImageContentType, option => option.MapFrom(src => src.Image.ContentType))
            .ForMember(dest => dest.ImageExtension, option => option.MapFrom(src => Path.GetExtension(src.Image.FileName)))
            .ReverseMap();


        CreateMap<BookFileRequest, Book>()
            .ForMember(dest => dest.Title, option => option.MapFrom(src => src.Document.FileName))
            .ForMember(dest => dest.FileContentType, option => option.MapFrom(src => src.Document.ContentType))
            .ForMember(dest => dest.FileExtension, option => option.MapFrom(src => Path.GetExtension(src.Document.FileName)))
            .ReverseMap();


        CreateMap<UpdateBookRequest, Book>()
            .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
            .ForMember(dest => dest.Quantity, option => option.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.PriceForBuy, option => option.MapFrom(src => src.PriceForBuy))
            .ForMember(dest => dest.PriceForBorrow, option => option.MapFrom(src => src.PriceForBorrow))
            .ReverseMap();


        CreateMap<Book, BookResponse>()
            .ForMember(dest => dest.Title, option => option.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
            .ForMember(dest => dest.Quantity, option => option.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.PriceForBuy, option => option.MapFrom(src => src.PriceForBuy))
            .ForMember(dest => dest.PriceForBorrow, option => option.MapFrom(src => src.PriceForBorrow))
            .ForMember(dest => dest.CategoryId, option => option.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.AuthorId, option => option.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.IsActive, option => option.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.PublishedDate, option => option.MapFrom(src => src.PublishedDate))
            .ForMember(dest => dest.id, option => option.MapFrom(src => src.Id))
            .ForMember(dest => dest.FilePath, option => option.MapFrom(src => src.FilePath))
            .ForMember(dest => dest.ImagePath, option => option.MapFrom(src => src.ImagePath));
    }
}
