using AutoMapper;
using LibrarySystem.Domain.DTO.CartItems;
using LibrarySystem.Domain.DTO.Items;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Mappings;
public class CartItemMappings:Profile
{
    public CartItemMappings()
    {
        CreateMap<CartItemRequest, CartItem>()
            .ForMember(dest => dest.BookId, option => option.MapFrom(src => src.BookId))
            .ForMember(dest => dest.OrderType, option => option.MapFrom(src => src.Type))
            .ForMember(dest => dest.Quantity, option => option.MapFrom(src => src.Quantity))
            .ReverseMap();

        CreateMap<CartItem, ItemResponse>()
            .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
            .ForMember(dest => dest.Type, option => option.MapFrom(src => src.OrderType))
            .ForMember(dest => dest.Quantity, option => option.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.Price, option => option.MapFrom(src => src.Price))
            .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => src.Quantity*src.Price))
            .ForMember(dest => dest.ImageUrl, option => option.MapFrom(src => src.Book.ImagePath))
            .ReverseMap();
    }
}
