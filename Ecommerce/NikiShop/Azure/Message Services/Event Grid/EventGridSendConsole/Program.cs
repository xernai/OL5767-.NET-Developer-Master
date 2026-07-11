using Azure;
using Azure.Messaging.EventGrid;
using System;
using System.Threading.Tasks;

namespace EventGridSendConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Event Grid Topics
            string topicEndpoint = "https://egt-leon.centralus-1.eventgrid.azure.net/api/events";
            string topicKey = "";

            EventGridPublisherClient client = new EventGridPublisherClient(new Uri(topicEndpoint), new AzureKeyCredential(topicKey));
            await client.SendEventAsync(new EventGridEvent(
                "Advertencia de temperaturas muy bajas",
                "Warning",
                "1.0",
                "Tomar medidas contra el frío, sobre todo con la gente."
            ));
            Console.WriteLine("Event has been published.");
        }
    }
}
