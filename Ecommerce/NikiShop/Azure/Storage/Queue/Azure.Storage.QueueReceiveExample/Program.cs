using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using System;

namespace Azure.Storage.QueueReceiveExample
{
    internal class Program
    {
        public static string connstring = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

        static void Main(string[] args)
        {
            RetrieveMessage();

            Console.WriteLine("Message received");
            
            Console.ReadLine();
        }


        public static void RetrieveMessage()
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connstring);
                CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();

                CloudQueue cloudQueue = cloudQueueClient.GetQueueReference("ecommerce");

                // FIFO: First In First Out
                CloudQueueMessage queueMessage = cloudQueue.GetMessage();

                Console.WriteLine(queueMessage.AsString);

                // Log4Net
                // log.Info("Borrar cola: " + queueMessage.AsString)
                cloudQueue.DeleteMessage(queueMessage);

                // Try to get a deleted queue
                //CloudQueueMessage queueMessage1 = cloudQueue.GetMessage();
                //Console.WriteLine(queueMessage1.AsString);
                //cloudQueue.DeleteMessage(queueMessage1);
            }
            catch (Exception e)
            {
                // Log4Net
                //log.Info("Error cola: " + e.Message);

                Console.WriteLine("Error cola: " + e.Message);
            }
        }
    }
}
