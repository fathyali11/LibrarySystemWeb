using FluentValidation;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.FluentValidations.GeneralValiations;

namespace LibrarySystem.Domain.FluentValidations.Books;
public class BookImageRequestValidator:AbstractValidator<BookImageRequest>
{
    public BookImageRequestValidator()
    {
        RuleFor(x=>x.Image)
            .SetValidator(new ImageValidator());
    }
}
