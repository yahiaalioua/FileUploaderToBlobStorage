namespace FileUploadApiAzureBlobStorage.Models
{
    public class Blob
    {
        public string Name { get; set; } = null!;
        public string Url { get; set; }= null!;
        public string ContentType { get; set; } = null!;
        public string Size { get; set; }=null!;
        public Stream? Content { get; set; }
    }
}
