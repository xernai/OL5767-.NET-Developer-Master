using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Azure.Storage.Blob.AuladiserService
{
    public class AzureBlobStorageService
    {
        // local connection
        //"UseDevelopmentStorage=true";

        // remote connection
        //
        private string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
        private BlobServiceClient blobServiceClient;

        public AzureBlobStorageService()
        {
            blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task CreateContainerAsync(string containerName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
        }

        public async Task<IDictionary<string, string>> GetBlobMetadataAsync(string containerName, string blobName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            BlobProperties properties = await blobClient.GetPropertiesAsync();
            return properties.Metadata;
        }

        public async Task UploadBlobAsync(string containerName, string blobName, string filePath)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using FileStream fs = File.OpenRead(filePath);
            await blobClient.UploadAsync(fs, true);
        }

        public async Task DownloadBlobAsync(string containerName, string blobName, string downloadPath)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

            using FileStream fs = File.OpenWrite(downloadPath);
            await blobDownloadInfo.Content.CopyToAsync(fs);
        }

        public async Task<List<string>> ListBlobsAsync(string containerName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobs = new List<string>();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                blobs.Add(blobItem.Name);
            }

            return blobs;
        }

        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }

        public async Task DeleteContainerAsync(string containerName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.DeleteIfExistsAsync();
        }
    }

    public class Program
    {
        static async Task Main(string[] args)
        {
            AzureBlobStorageService azureBlobStorageService = new AzureBlobStorageService();

            // await azureBlobStorageService.CreateContainerAsync("leon");

            // await azureBlobStorageService.CreateContainerAsync("ecommerce");

            // await azureBlobStorageService.UploadBlobAsync("ecommerce", "Generative AI.txt", @"C:\Users\Xabier\Downloads\Generative AI.txt");

            var result = await azureBlobStorageService.ListBlobsAsync("carimages");

            if (result == null || result.Count == 0)
            {
                Console.WriteLine("No blobs found in container 'carimages'.");
            }
            else
            {
                Console.WriteLine($"Found {result.Count} blob(s) in container 'carimages':");
                foreach (var name in result)
                {
                    Console.WriteLine($"- {name}");
                }
            }

        }
    }
}
