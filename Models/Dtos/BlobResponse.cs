namespace FileUploadApiAzureBlobStorage.Models.Dtos
{
    public record BlobResponse
        (
        string Name,
        string Url,
        string Message
        );
}
