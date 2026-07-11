using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.Storage;
using System;

namespace Azure.Storage.QueueSendExample
{
    internal class Program
    {
        public static string connstring = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

        static void Main(string[] args)
        {
            AddMessage();
        }

        public static void AddMessage()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connstring);
            CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference("ecommerce");

            var message = "Collar Mattingale K100 para mi perro";
            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            cloudQueue.AddMessage(queueMessage);

            Console.WriteLine("Message sent");
        }
    }
}
