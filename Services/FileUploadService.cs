using Azure.Storage.Blobs;
using FileUploadApiAzureBlobStorage.Abstract;
using FileUploadApiAzureBlobStorage.DomainErrors.Errors;
using FileUploadApiAzureBlobStorage.Models;
using FileUploadApiAzureBlobStorage.Models.Dtos;
using ZeroSigma.Domain.Common.Results;

namespace FileUploadApiAzureBlobStorage.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly BlobContainerClient _blobStorageContainer;
        private readonly string _containerName = "data";

        public FileUploadService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("BlobStorageConnStr")!;
            var blobServiceClient = new BlobServiceClient(_connectionString);
            _blobStorageContainer = blobServiceClient.GetBlobContainerClient(_containerName);
        }

        public async Task<Result<List<Blob>>> ListAllAsync()
        {
            List<Blob> files = new List<Blob>();

            await foreach (var file in _blobStorageContainer.GetBlobsAsync())
            {
                if (file == null)
                {
                    return new NotFoundResults<List<Blob>>(FileLogicalValidationErrors.EmptyBlobStorageError);
                }
                else
                {
                    var name = file.Name;
                    var url = _blobStorageContainer.Uri;
                    var fullUri = $"{url}/{name}";

                    Blob blob = new()
                    {
                        Name = name,
                        ContentType = file.Properties.ContentType,
                        Size = $"{file.Properties.ContentLength} bytes",
                        Url = fullUri
                    };
                    files.Add(blob);
                }
            }
            return new SuccessResult<List<Blob>>(files);
        }

        public async Task<Result<BlobResponse>> UploadAsync(IFormFile file)
        {
            if (file.Length > 8000000)
                return new InvalidResult<BlobResponse>(FileStructuralValidationErrors.ExceededFileSizeLimitError);
            else
            {
                var client = _blobStorageContainer.GetBlobClient(file.FileName);
                await using (var data = file.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }
                var response = new BlobResponse(file.Name, client.Uri.AbsoluteUri, "file succesfully uploaded");
                return new SuccessResult<BlobResponse>(response);
            }
        }

        public async Task<Blob> DownloadAsync(string fileName)
        {
            BlobClient client = _blobStorageContainer.GetBlobClient(fileName);
            if (!await client.ExistsAsync())
                throw new Exception($"{FileLogicalValidationErrors.FileNotFound.Title}," +
                    $"{FileLogicalValidationErrors.FileNotFound.Detail}");

            else
            {
                var blob = await client.DownloadAsync();
                Stream fileContent = await client.OpenReadAsync();
                var response = new Blob()
                {
                    Content = fileContent,
                    ContentType = blob.Value.Details.ContentType,
                    Name = fileName,
                    Size = $"{blob.Value.Details.ContentLength} bytes",
                    Url = client.Uri.AbsoluteUri
                };
                return response;
            }
        }

        public async Task<Result<BlobResponse>> DelateAsync(string fileName)
        {
            var client = _blobStorageContainer.GetBlobClient(fileName);
            if (!await client.ExistsAsync())
                return new NotFoundResults<BlobResponse>(FileLogicalValidationErrors.FileNotFound);
            else
            {
                await client.DeleteAsync();
                var response = new BlobResponse(fileName, client.Uri.AbsoluteUri, "File succesfully delated from storage");
                return new SuccessResult<BlobResponse>(response);
            }
        }
    }
}
