using FluentValidation;
using LibrarySystem.Domain.Abstractions.ConstValues;
using LibrarySystem.Domain.DTO.Books;
using LibrarySystem.Domain.FluentValidations.GeneralValiations;

namespace LibrarySystem.Domain.FluentValidations.Books;
public class BookFileRequestValidator:AbstractValidator<BookFileRequest>
{
    public BookFileRequestValidator()
    {
        RuleFor(x => x.Document)
            .SetValidator(new DocumentValidator());
    }
}
