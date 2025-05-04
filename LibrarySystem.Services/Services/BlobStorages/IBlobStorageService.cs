namespace LibrarySystem.Services.Services.BlobStorages;
public interface IBlobStorageService
{
    Task<string> UploadFileAsync(string fileName, IFormFile file, string type);
    string? GenerateSasToken(string blobUrl, string type);
    Task<Stream> DownloadFileAsync(string fileName, string type);
    Task<bool> DeleteFileAsync(string fileName, string type);
    

}
