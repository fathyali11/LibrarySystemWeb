//using AutoMapper;
//using LibrarySystem.Domain.DTO.OrderItems;
//using LibrarySystem.Domain.DTO.Orders;
//using LibrarySystem.Domain.Entities;

//namespace LibrarySystem.Domain.Mappings
//{
//    public class OrderMappings:Profile
//    {
//        public OrderMappings()
//        {
//            CreateMap<OrderItem, OrderItemResponse>()
//                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.OrderItemId))
//                .ForMember(dest => dest.BookName, option => option.MapFrom(src => src.Book.Title))
//                .ForMember(dest => dest.BookPrice, option => option.MapFrom(src => src.Price))
//                .ForMember(dest => dest.Quantity, option => option.MapFrom(src => src.Quantity))
//                .ReverseMap();

//            CreateMap<Order, OrderResponse>()
//                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.Id))
//                .ForMember(dest => dest.TotalAmount, option => option.MapFrom(src => src.TotalAmount))
//                .ForMember(dest => dest.OrderStatus, option => option.MapFrom(src => src.OrderStatus))
//                .ForMember(dest => dest.OrderItems, option => option.MapFrom(src => src.OrderItems))
//                .ReverseMap();
//        }
//    }
//}
