using Google.Cloud.Storage.V1;

public class GoogleCloudStorageService
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName = "connectify-social-bucket";

    public GoogleCloudStorageService()
    {
        _storageClient = StorageClient.Create();
    }

    public async Task<string> UploadFileAsync(IFormFile file, string userId)
    {
        if (file == null || file.Length == 0)
        {
            throw new Exception("Invalid file");
        }

        string fileName = $"avatar/{userId}_{Guid.NewGuid()}_{file.FileName}";

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var uploadObject = await _storageClient.UploadObjectAsync(
            _bucketName,
            fileName,
            file.ContentType,
            memoryStream
        );

        return $"https://storage.googleapis.com/{_bucketName}/{fileName}";
    }
}