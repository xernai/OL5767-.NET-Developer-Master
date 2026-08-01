using System;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.EventGrid;

namespace EventGridSender
{
    class Program
    {
        // Sustituye con el Endpoint y Access Key de tu Topic personalizado en Azure Portal
        private const string topicEndpoint = "";
        private const string topicKey = "";

        static async Task Main(string[] args)
        {
            // Validar argumento recibido por consola
            if (args.Length == 0)
            {
                Console.WriteLine("Por favor, proporciona un argumento. Ejemplo: dotnet run -- motoComprada");
                return;
            }

            string parametro = args[0];

            // Verificar si el parámetro es "motoComprada"
            if (string.Equals(parametro, "motoComprada", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Parámetro 'motoComprada' detectado. Disparando evento...");

                Uri endpoint = new Uri(topicEndpoint);
                AzureKeyCredential credential = new AzureKeyCredential(topicKey);
                EventGridPublisherClient client = new EventGridPublisherClient(endpoint, credential);

                // Construcción del evento
                var myEvent = new EventGridEvent(
                    subject: "motos/nueva-alta",
                    eventType: "Com.MiEmpresa.MotoCreada",
                    dataVersion: "1.0",
                    data: new
                    {
                        Marca = "Ducati",
                        Modelo = "Monster",
                        Anio = 2024
                    }
                );

                try
                {
                    Console.WriteLine("Enviando evento a Azure Event Grid...");
                    await client.SendEventAsync(myEvent);
                    Console.WriteLine("¡Evento enviado con éxito!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al enviar el evento: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"El parámetro '{parametro}' no dispara eventos. Solo se acepta 'motoComprada'.");
            }
        }
    }
}