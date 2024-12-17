using AutoMapper;
using LibrarySystem.Domain.DTO.Carts;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Domain.Mappings;
public class CartMappings:Profile
{
    public CartMappings()
    {
        CreateMap<Cart, CartReponse>()
            .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
            .ForMember(dest => dest.CartItems, option => option.MapFrom(src => src.CartItems))
            .ReverseMap();
    }
}
