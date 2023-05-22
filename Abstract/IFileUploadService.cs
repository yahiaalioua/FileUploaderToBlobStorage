using FileUploadApiAzureBlobStorage.Models;
using FileUploadApiAzureBlobStorage.Models.Dtos;
using ZeroSigma.Domain.Common.Results;

namespace FileUploadApiAzureBlobStorage.Abstract
{
    public interface IFileUploadService
    {
        Task<Result<BlobResponse>> DelateAsync(string fileName);
        Task<Blob> DownloadAsync(string fileName);
        Task<Result<List<Blob>>> ListAllAsync();
        Task<Result<BlobResponse>> UploadAsync(IFormFile file);
    }
}