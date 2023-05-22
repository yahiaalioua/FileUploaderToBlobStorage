using System.Net;

namespace FileUploadApiAzureBlobStorage.DomainErrors.Errors
{
    public static class FileLogicalValidationErrors
    {
        public static readonly CustomProblemDetails EmptyBlobStorageError = new()
        {
            Status = HttpStatusCode.NotFound,
            Title = "Empty blob storage",
            Type = "NotFound",
            Detail = "The container has no files to list",
            Code = "001"
        };
        public static readonly CustomProblemDetails FileNotFound = new()
        {
            Status = HttpStatusCode.NotFound,
            Title = "File Not found",
            Type = "NotFound",
            Detail = "The file is not found, make sure that the file exist in the blob storage",
            Code = "002"
        };
    }
}
