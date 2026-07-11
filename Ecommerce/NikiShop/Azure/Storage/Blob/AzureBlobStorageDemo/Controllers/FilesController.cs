using AzureBlobStorageDemo.API.Models;
using AzureBlobStorageDemo.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobStorageDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IBlobStorage _blobService;

        public FilesController(IBlobStorage blobService) // BlobStorage blobService = new BlobStorage()
        {
            _blobService = blobService;
        }

        /// <summary>
        /// upload file
        /// </summary>
        /// <param name="fileDetail"></param>
        /// <returns></returns>
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileDetails fileDetail)
        {
            if (fileDetail.file != null)
            {
                await _blobService.UploadFileAsync(fileDetail);
            }
            return Ok();
        }
    }
}
