using AutoMapper;
using LibrarySystem.Domain.DTO.Categories;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Mappings;
public class CategoryMappings: Profile
{
    public CategoryMappings()
    {
        CreateMap<CategoryRequest, Category>()
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
            .ReverseMap();

        CreateMap<Category, CategoryResponse>()
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
            .ForMember(dest => dest.IsDeleted, option => option.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<Category, CategoryWithBooksResponse>()
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, option => option.MapFrom(src => src.Description))
            .ForMember(dest => dest.IsDeleted, option => option.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
            .ForMember(dest => dest.Books, option => option.MapFrom(src => src.Books!.Select(x=>new
            {
                x.Id,
                x.Title,
                x.Description,
                x.Quantity,
                x.PriceForBuy,
                x.PriceForBorrow,
                x.PublishedDate,
                x.AuthorId

            })))
            .ReverseMap();

    }
}
