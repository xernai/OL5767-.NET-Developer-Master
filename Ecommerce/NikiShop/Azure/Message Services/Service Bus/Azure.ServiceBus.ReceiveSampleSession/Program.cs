using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Azure.ServiceBus.ReceiveSampleSession
{
    internal class Program
    {
        private const string connectionString = "";
        private const string queueName = "ecommerce";
        private const string sessionIdToProcess = "";

        static async Task Main(string[] args)
        {
            ServiceBusClient client = new ServiceBusClient(connectionString);

            // Aceptar la sesión específica (obtener el SessionReceiver)
            ServiceBusSessionReceiver receiver = await client.AcceptSessionAsync(queueName, sessionIdToProcess);

            // Leer mensajes en un bucle
            while (true)
            {
                // Intentar recibir un lote de mensajes
                // Se debe definir una lógica para determinar cuándo termina la sesión (ej. un mensaje final)
                IReadOnlyList<ServiceBusReceivedMessage> messages = await receiver.ReceiveMessagesAsync(maxMessages: 10, maxWaitTime: TimeSpan.FromSeconds(5));

                if (messages.Count == 0)
                {
                    // No hay más mensajes en esta sesión por ahora, se puede salir del bucle
                    break;
                }

                foreach (ServiceBusReceivedMessage message in messages)
                {
                    // Procesar el mensaje
                    Console.WriteLine($"Procesando mensaje con SessionId: {message.SessionId}, Body: {message.Body}");

                    // Completar el mensaje para eliminarlo de la cola
                    await receiver.CompleteMessageAsync(message);
                }
            }

            // Cerrar el receptor de sesión para liberar el bloqueo
            await receiver.CloseAsync();
            await client.DisposeAsync();
        }
    }
}
