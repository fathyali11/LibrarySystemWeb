﻿using LibrarySystem.Domain.DTO.CartItems;
using LibrarySystem.Domain.DTO.Carts;

namespace LibrarySystem.Services.Services.CartItems;
public interface ICartItemServices:ICartItemRepository
{
    Task<OneOf<CartIdResponse, Error>> AddOrderToCartAsync(string userId,CartItemRequest request,CancellationToken cancellationToken=default);
    Task<OneOf<bool,Error>> PlusAsync(string userId,int id,CancellationToken cancellationToken=default);
    Task<OneOf<bool,Error>> MinusAsync(string userId,int id,CancellationToken cancellationToken=default);
    Task<OneOf<bool,Error>> RemoveAsync(string userId,int id,CancellationToken cancellationToken=default);
    

}
