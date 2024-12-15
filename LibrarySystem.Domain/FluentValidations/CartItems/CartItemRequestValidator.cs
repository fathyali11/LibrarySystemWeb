using FluentValidation;
using LibrarySystem.Domain.Abstractions.ConstValues;
using LibrarySystem.Domain.DTO.CartItems;

namespace LibrarySystem.Domain.FluentValidations.CartItems;
public class CartItemRequestValidator:AbstractValidator<CartItemRequest>
{
    public CartItemRequestValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("BookId is required")
            .GreaterThan(0).WithMessage("BookId must be greater than zero");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("BookId is required")
            .Must(IsValidateType).WithMessage($"Type must be {OrderTypes.Borrow} or {OrderTypes.Buy}");

        RuleFor(x=>x.Quantity)
            .NotEmpty().WithMessage("Quantity is required")
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .GreaterThan(0).WithMessage("CategoryId must be greater than zero");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("AuthorId is required")
            .GreaterThan(0).WithMessage("AuthorId must be greater than zero");
    }

    private bool IsValidateType(string type)
        => string.Equals(type, OrderTypes.Borrow, StringComparison.OrdinalIgnoreCase) ||
          string.Equals(type, OrderTypes.Buy, StringComparison.OrdinalIgnoreCase);
}
