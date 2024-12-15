using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using LibrarySystem.Domain.Abstractions.ConstValues;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.FluentValidations.GeneralValiations;
public class DocumentValidator:AbstractValidator<IFormFile>
{
    public DocumentValidator()
    {
        RuleFor(x=>x)
            .Must(x=>x.Length<=FileSettings.MaxFileSizeInByte).WithMessage($"File size must not exceed {FileSettings.MaxFileSizeInMB} MB.")
            .Must(HasValidateType).WithMessage($"File type must be {FileSettings.AllowedDocumentExtensions}")
            .When(x => x is not null);
    }
    private bool HasValidateType(IFormFile file)
    {
        var binaryReader=new BinaryReader(file.OpenReadStream());
        var bytes=binaryReader.ReadBytes(2);
        var signatureInHexa = BitConverter.ToString(bytes);

        foreach (var item in FileSettings.AllowedDocumentSignatures)
            if (string.Equals(item, signatureInHexa, StringComparison.OrdinalIgnoreCase))
                return true;

        return false;
    }
}
