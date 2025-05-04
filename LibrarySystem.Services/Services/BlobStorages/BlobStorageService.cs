
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace LibrarySystem.Services.Services.BlobStorages;
public class BlobStorageService(IOptions<BlobSettings> options) : IBlobStorageService
{
    private readonly BlobSettings _blobSettings = options.Value;

    public async Task<string> UploadFileAsync(string fileName, IFormFile file,string type)
    {
        var blobServiceClient = new BlobServiceClient(_blobSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(type == "image" ? _blobSettings.ContainerNameImages : _blobSettings.ContainerNameFiles);
        await containerClient.CreateIfNotExistsAsync();
        var blobClient = containerClient.GetBlobClient(fileName);
        using var fileStream = file.OpenReadStream();
        blobClient.Upload(fileStream, true);
        return blobClient.Uri.ToString();
    }
    public async Task<Stream> DownloadFileAsync(string fileName, string type)
    {
        var blobServiceClient = new BlobServiceClient(_blobSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(type == "image" ? _blobSettings.ContainerNameImages : _blobSettings.ContainerNameFiles);
        var blobClient = containerClient.GetBlobClient(fileName);
        var downloadInfo = await blobClient.DownloadAsync();
        return downloadInfo.Value.Content;
    }
    public async Task<bool> DeleteFileAsync(string fileName, string type)
    {
        var blobServiceClient = new BlobServiceClient(_blobSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(type == "image" ? _blobSettings.ContainerNameImages : _blobSettings.ContainerNameFiles);
        var blobClient = containerClient.GetBlobClient(fileName);
        return await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
    }

    public string? GenerateSasToken(string blobUrl,string type)
    {
        if (string.IsNullOrEmpty(blobUrl))
            return null;

        var containerName = type == "image" ? _blobSettings.ContainerNameImages : _blobSettings.ContainerNameFiles;

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerName,
            BlobName = GetBlobName(blobUrl),
            Resource = "b",
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var credentials = new StorageSharedKeyCredential(_blobSettings.AccountName,_blobSettings.AccountKey);

        var sasToken = sasBuilder.ToSasQueryParameters(credentials).ToString();
        return sasToken;

    }

    private string GetBlobName(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        return uri.Segments.Last().ToString();
    }
}
