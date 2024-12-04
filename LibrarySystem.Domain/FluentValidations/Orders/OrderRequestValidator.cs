using FluentValidation;
using LibrarySystem.Domain.DTO.Orders;

namespace LibrarySystem.Domain.FluentValidations.Orders
{
    public class OrderRequestValidator:AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            RuleForEach(x => x.Books)
                .NotEmpty()
                .ChildRules(books =>
                {
                    books.RuleFor(x => x.BookId)
                    .NotEmpty().WithMessage("Title is required.")
                    .GreaterThan(0).WithMessage("Book ID must be a positive integer.");

                    books.RuleFor(x => x.Type)
                    .Must(type => type == "borrow" || type == "purchase");

                    books.RuleFor(x => x.Price)
                        .GreaterThan(0).WithMessage("Price must be a positive integer.");

                    books.RuleFor(x => x.CategoryId)
                        .GreaterThan(0).WithMessage("Category ID must be a positive integer.");

                    books.RuleFor(x => x.AuthorId)
                        .GreaterThan(0).WithMessage("Author ID must be a positive integer.");
                });
        }
    }
}
