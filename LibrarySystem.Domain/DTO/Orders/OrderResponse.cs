using LibrarySystem.Domain.DTO.OrderItems;
namespace LibrarySystem.Domain.DTO.Orders;
public record OrderResponse(
    int Id,
    decimal TotalAmount,
    string OrderStatus,
    List<OrderItemResponse> OrderItems
    );
    

