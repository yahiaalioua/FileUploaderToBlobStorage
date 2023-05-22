using System.Net;

namespace FileUploadApiAzureBlobStorage.DomainErrors.Errors
{
    public static class FileStructuralValidationErrors
    {
        public static readonly CustomProblemDetails ExceededFileSizeLimitError = new()
        {
            Status = HttpStatusCode.BadRequest,
            Title = "File size limit exceeded",
            Type = "Invalid file size",
            Detail = "The maximum allowed file size can´t exceed 8 MB",
            Code = "010"
        };
    }
}
