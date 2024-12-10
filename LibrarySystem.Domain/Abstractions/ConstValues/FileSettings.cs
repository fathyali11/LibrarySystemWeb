namespace LibrarySystem.Domain.Abstractions.ConstValues;
public class FileSettings
{
    public const int MaxImageSizeInMB = 10;
    public const int MaxImageSizeInByte = 10 * 1024 * 1024;
    public const int MaxFileSizeInMB = 20;
    public const int MaxFileSizeInByte = 20 * 1024 * 1024;
    public const string AllowedDocumentExtensions = "pdf,doc";
    public const string AllowedImageExtensions = "png,jpeg";
    public static readonly List<string> AllowedImageSignatures = new List<string>
    {
        "FF-D8",    // JPEG
        "89-50",    // PNG
    };
    public static readonly List<string> AllowedDocumentSignatures = new List<string>
    {
        "25-50",    // PDF
        "50-4B"   // DOC,
    };
}
