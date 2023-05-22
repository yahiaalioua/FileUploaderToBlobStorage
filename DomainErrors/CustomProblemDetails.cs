using System.Net;

namespace FileUploadApiAzureBlobStorage.DomainErrors
{
    public class CustomProblemDetails
    {
        public HttpStatusCode Status { get; set; }
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public string Code { get; set; } = null!;

    }
}
