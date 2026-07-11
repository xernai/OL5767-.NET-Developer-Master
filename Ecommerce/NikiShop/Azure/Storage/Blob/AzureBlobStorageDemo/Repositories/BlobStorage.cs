using Azure.Storage.Blobs;
using AzureBlobStorageDemo.API.Models;

namespace AzureBlobStorageDemo.API.Repositories
{
    public class BlobStorage : IBlobStorage
    {
        private readonly BlobServiceClient _blobService;
        private readonly IConfiguration _configuration;
        public BlobStorage(BlobServiceClient blobService, IConfiguration configuration)
        {
            _blobService = blobService;
            _configuration = configuration;
        }

        public async Task UploadFileAsync(FileDetails fileDetails)
        {
            //get the blob container
            var blobStorageContainerName = _blobService.GetBlobContainerClient(_configuration.GetValue<string>("BlobContainer"));

            //get the blob client
            var blobStorageClient = blobStorageContainerName.GetBlobClient(fileDetails.file.FileName);

            //read file stream
            var streamContent = fileDetails.file.OpenReadStream();

            //upload file
            await blobStorageClient.UploadAsync(streamContent);
        }
    }
}
