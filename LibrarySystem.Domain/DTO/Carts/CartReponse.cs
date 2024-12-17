using LibrarySystem.Domain.DTO.CartItems;

namespace LibrarySystem.Domain.DTO.Carts;
public record CartReponse(
    int Id,
    List<CartItemResponse> CartItems
    );
