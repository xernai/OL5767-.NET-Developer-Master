using Azure;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace AzureBlobTriggerFunction_1
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
        [Function(nameof(FileDetailsBlobTrigger))]
        public async Task Run([BlobTrigger("carimages/{name}", Connection = "BlobTriggerConnection")] Stream myBlob, string name)
        {
            //log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

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


