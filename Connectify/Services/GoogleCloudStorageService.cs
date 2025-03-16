using Google.Cloud.Storage.V1;

public class GoogleCloudStorageService
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName = "connectify-social-bucket";

    public GoogleCloudStorageService()
    {
        _storageClient = StorageClient.Create();
    }

    public async Task<Google.Apis.Storage.v1.Data.Object> UploadFileAsync(IFormFile file, string userId)
    {
        if (file == null || file.Length == 0)
        {
            throw new Exception("Invalid file");
        }

        string fileName = $"avatar/{userId}_{Guid.NewGuid()}_{file.FileName}";

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var storage = await _storageClient.UploadObjectAsync(
            _bucketName,
            fileName,
            file.ContentType,
            memoryStream
        );

        return storage;
    }

    public async Task<Google.Apis.Storage.v1.Data.Object> UploadFileToGCS(IFormFile file, string userId)
    {
        if (file == null || file.Length == 0)
        {
            throw new Exception("Invalid file");
        }

        string fileName = $"postmedia/{userId}_{Guid.NewGuid()}_{file.FileName}";

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var storage = await _storageClient.UploadObjectAsync(
            _bucketName,
            fileName,
            file.ContentType,
            memoryStream
        );

        return storage;
    }

}