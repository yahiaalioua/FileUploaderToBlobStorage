using FileUploadApiAzureBlobStorage.Abstract;
using FileUploadApiAzureBlobStorage.Models;
using FileUploadApiAzureBlobStorage.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeroSigma.Domain.Common.Results;

namespace FileUploadApiAzureBlobStorage.Controllers
{
    [Route("api/v1/files")]
    [ApiController]
    public class FilesUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileService;

        public FilesUploadController(IFileUploadService fileService)=>
            _fileService = fileService;

        [HttpGet]
        public async Task<IActionResult> ListFiles()
        {
            return this.FromResult<List<Blob>>(await _fileService.ListAllAsync());
        }

        [HttpGet]
        [Route("Download")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var response = await _fileService.DownloadAsync(fileName);
            return File(response.Content,response.ContentType,response.Name);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            return this.FromResult<BlobResponse>(await _fileService.UploadAsync(file));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFile(string file)
        {
            return this.FromResult<BlobResponse>(await _fileService.DelateAsync(file));
        }

    }
}
