using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Azure.ServiceBus.SendSample
{
    internal class Program
    {
        private static IQueueClient queueClient;
        private const string ServiceBusConnectionString = "Endpoint=sb://nikishop.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/e4hcdVO69eMQLTizaCgKoD7Twsu3lxIM+ASbL0FdVA=";
        private const string QueueName = "ecommerce";

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName, ReceiveMode.PeekLock);

            await SendMessagesToQueue(10);

            // Close the client after the ReceiveMessages method has exited. 
            await queueClient.CloseAsync();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        // Creates a Queue client and sends 10 messages to the queue. 
        private static async Task SendMessagesToQueue(int numMessagesToSend)
        {
            //var messageId = Guid.NewGuid();
            for (var i = 0; i < numMessagesToSend; i++)
            {
                try
                {
                    // Create a new brokered message to send to the queue 
                    var message = new Message(Encoding.UTF8.GetBytes($"Message {i}"));


                    //message.SessionId = messageId.ToString();
                    // Write the body of the message to the console 
                    Console.WriteLine($"Sending message: {Encoding.UTF8.GetString(message.Body)}");

                    // Send the message to the queue 
                    await queueClient.SendAsync(message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
                }

                // Delay by 10 milliseconds so that the console can keep up 
                await Task.Delay(10);
            }

            Console.WriteLine($"{numMessagesToSend} messages sent.");
        }
    }
}
