using System;
using System.IO;
using Azure;
using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureBlobTriggerFunction
{
    public class FileDetailsEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string FileName { get; set; }
        public bool isFileUploaded { get; set; }
        public DateTime DateOfUpdation { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }

    public class FileDetailsBlobTrigger
    {
        [FunctionName("FileDetailsBlobTrigger")]
        public void Run([BlobTrigger("carimages/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            var connectionString = "";
            var tableName = "FileDetails";

            var client = new TableClient(connectionString, tableName);
            // Create the table if it doesn't already exist to verify we've successfully authenticated.
            client.CreateIfNotExists();

            AddEntity(client, name, true, DateTime.UtcNow);
        }

        static void AddEntity(TableClient client, string fileName, bool isFileUploaded, DateTime dateOfUpdation)
        {
            FileDetailsEntity fileDetailsEntity = new FileDetailsEntity
            {

                PartitionKey = "FileDetails",
                RowKey = Guid.NewGuid().ToString(),
                FileName = fileName,
                isFileUploaded = isFileUploaded,
                DateOfUpdation = dateOfUpdation

            };
            client.AddEntity(fileDetailsEntity);
        }
    }
}
