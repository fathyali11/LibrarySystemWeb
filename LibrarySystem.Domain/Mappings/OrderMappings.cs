using AutoMapper;
using LibrarySystem.Domain.DTO.Items;
using LibrarySystem.Domain.DTO.Orders;
using LibrarySystem.Domain.Entities;
namespace LibrarySystem.Domain.Mappings
{
    public class OrderMappings : Profile
    {
        public OrderMappings()
        {
            CreateMap<Cart, Order>()
                .ForMember(dest => dest.Id, option => option.Ignore())
                .ForMember(dest => dest.CartId, option => option.MapFrom(src=>src.Id))
                .ForMember(dest => dest.UserId, option => option.MapFrom(src => src.UserId))
                .ForMember(dest => dest.TotalAmount, option => option.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.FirstName, option => option.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, option => option.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Address, option => option.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.Email, option => option.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Phone, option => option.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.OrderItems, option => option.MapFrom(src => src.CartItems));

            CreateMap<OrderItem, CartItem>()
               .ForMember(dest => dest.BookId, option => option.MapFrom(src => src.BookId))
               .ForMember(dest => dest.Quantity, option => option.MapFrom(src => src.Quantity))
               .ForMember(dest => dest.Price, option => option.MapFrom(src => src.Price))
               .ForMember(dest => dest.OrderType, option => option.MapFrom(src => src.OrderType))
               .ForMember(dest => dest.Id, option => option.Ignore())
               .ReverseMap();


            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.CartItems, option => option.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.FirstName, option => option.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, option => option.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Address, option => option.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, option => option.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, option => option.MapFrom(src => src.Phone));

           
            CreateMap<OrderItem, ItemResponse>()
            .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
            .ForMember(dest => dest.Type, option => option.MapFrom(src => src.OrderType))
            .ForMember(dest => dest.Quantity, option => option.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Name, option => option.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.Price, option => option.MapFrom(src => src.Price))
            .ForMember(dest => dest.TotalPrice, option => option.MapFrom(src => src.Quantity * src.Price))
            .ForMember(dest => dest.ImageUrl, option => option.MapFrom(src => src.Book.ImagePath));

        }
    }
}
