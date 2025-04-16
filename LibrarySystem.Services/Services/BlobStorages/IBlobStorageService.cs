namespace LibrarySystem.Services.Services.BlobStorages;
public interface IBlobStorageService
{
    Task<string> UploadFileAsync(string fileName, IFormFile file, string type);
    //Task DeleteFileAsync(string fileName);
    //Task<Stream> DownloadFileAsync(string fileName);
    //Task<List<string>> ListFilesAsync();
}
