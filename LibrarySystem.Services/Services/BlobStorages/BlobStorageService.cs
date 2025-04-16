
using Azure.Storage.Blobs;

namespace LibrarySystem.Services.Services.BlobStorages;
public class BlobStorageService(IOptions<BlobSettings> options) : IBlobStorageService
{
    private readonly BlobSettings _blobSettings = options.Value;

    public async Task<string> UploadFileAsync(string fileName, IFormFile file,string type)
    {
        // Create a BlobServiceClient object which allows you to interact with the blob service
        var blobServiceClient = new BlobServiceClient(_blobSettings.ConnectionString);
        // Create a container client
        var containerClient = blobServiceClient.GetBlobContainerClient(type == "image" ? _blobSettings.ContainerNameImages : _blobSettings.ContainerNameFiles);
        // Create the container if it doesn't already exist
        await containerClient.CreateIfNotExistsAsync();
        // Get a reference to a blob
        var blobClient = containerClient.GetBlobClient(fileName);
        // Upload the file
        using var fileStream = file.OpenReadStream();
        blobClient.Upload(fileStream, true);
        return blobClient.Uri.ToString();
    }
    //public Task DeleteFileAsync(string fileName)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<Stream> DownloadFileAsync(string fileName)
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<List<string>> ListFilesAsync()
    //{
    //    throw new NotImplementedException();
    //}

    
}
