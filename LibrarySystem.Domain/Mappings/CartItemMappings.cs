using AutoMapper;
using LibrarySystem.Domain.DTO.CartItems;
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
    }
}
