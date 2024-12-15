using FluentValidation;
using LibrarySystem.Domain.Abstractions.ConstValues;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.FluentValidations.GeneralValiations;
public class ImageValidator:AbstractValidator<IFormFile>
{
    public ImageValidator()
    {
        RuleFor(x => x)
            .Must(x => x.Length <= FileSettings.MaxImageSizeInByte).WithMessage($"Image size must not exceed {FileSettings.MaxImageSizeInMB} MB.")
            .Must(HasValidateType).WithMessage($"Image type must be {FileSettings.AllowedImageExtensions}")
            .When(x => x is not null);
    }
    private bool HasValidateType(IFormFile file)
    {
        var binaryReader=new BinaryReader(file.OpenReadStream());
        var bytes = binaryReader.ReadBytes(2);
        var signatureInHexa=BitConverter.ToString(bytes);

        foreach(var signature in FileSettings.AllowedImageSignatures) 
            if(string.Equals(signature,signatureInHexa,StringComparison.OrdinalIgnoreCase))
                return true;

        return false;
    }
}
