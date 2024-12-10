using FluentValidation;
using LibrarySystem.Domain.Abstractions.ConstValues;
using LibrarySystem.Domain.DTO.Books;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.FluentValidations.Books;
public class BookRequestValidator : AbstractValidator<BookRequest>
{
    public BookRequestValidator()
    {

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

        RuleFor(x => x.PriceForBuy)
            .GreaterThan(0).WithMessage("Price for buy must be greater than zero.");

        RuleFor(x => x.PriceForBorrow)
            .GreaterThan(0).WithMessage("Price for borrow must be greater than zero.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category ID must be a positive integer.");

        RuleFor(x => x.AuthorId)
            .GreaterThan(0).WithMessage("Author ID must be a positive integer.");

        RuleFor(x => x.Document)
            .NotEmpty()
            .Must(HasValidateDocumentSize).WithMessage($"File size must not exceed {FileSettings.MaxFileSizeInMB} MB.")
            .Must(HasValidateDocumentType).WithMessage($"File type must be {FileSettings.AllowedDocumentExtensions}")
            .When(x => x.Document is not null);

        RuleFor(x => x.Image)
           .NotEmpty()
           .Must(HasValidateImageSize).WithMessage($"Image size must not exceed {FileSettings.MaxImageSizeInMB} MB.")
           .Must(HasValidateImageType).WithMessage($"Image type must be {FileSettings.AllowedImageExtensions}")
           .When(x => x.Image is not null);

    }
    private bool HasValidateImageSize(IFormFile file)
        => file.Length <= FileSettings.MaxImageSizeInByte;
    private bool HasValidateDocumentSize(IFormFile file)
        => file.Length <= FileSettings.MaxFileSizeInByte;

    private bool HasValidateDocumentType(IFormFile file)
        => HasValidateType(file, FileSettings.AllowedDocumentSignatures);

    private bool HasValidateImageType(IFormFile file)
        => HasValidateType(file, FileSettings.AllowedImageSignatures);
    
    private bool HasValidateType(IFormFile file,List<string> signatures)
    {
        var binaryReader = new BinaryReader(file.OpenReadStream());
        var bytes = binaryReader.ReadBytes(2);

        var fileInHexaDecimal = BitConverter.ToString(bytes);

        foreach (var item in signatures)
            if (string.Equals(item, fileInHexaDecimal, StringComparison.OrdinalIgnoreCase))
                return true;

        return false;

    }
}