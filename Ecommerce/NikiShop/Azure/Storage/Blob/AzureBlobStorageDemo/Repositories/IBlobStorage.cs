using AzureBlobStorageDemo.API.Models;

namespace AzureBlobStorageDemo.API.Repositories
{
    public interface IBlobStorage
    {
        Task UploadFileAsync(FileDetails fileDetails);
    }
}
