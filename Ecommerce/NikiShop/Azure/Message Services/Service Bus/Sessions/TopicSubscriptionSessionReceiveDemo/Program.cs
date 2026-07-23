using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;



string connectionString = "";
string topicName = "ecommerce";
string subscriptionName = "S4";

var client = new ServiceBusClient(connectionString);

try
{
    // 1. Acepta la próxima sesión disponible que tenga mensajes
    // Esto bloquea la sesión exclusivamente para este consumidor
    ServiceBusSessionReceiver receiver = await client.AcceptNextSessionAsync(topicName, subscriptionName);
    Console.WriteLine($"Conectado exitosamente a la Sesión: {receiver.SessionId}");

    // 2. Lee los mensajes de esta sesión en bucle
    while (true)
    {
        // Espera un mensaje por un máximo de 5 segundos
        ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(1));

        if (message != null)
        {
            Console.WriteLine($"Procesando: {message.Body}");

            // 3. Confirma que el mensaje fue procesado con éxito
            await receiver.CompleteMessageAsync(message);
        }
        else
        {
            // Si pasan 5 segundos sin mensajes nuevos, asumimos que la sesión terminó
            Console.WriteLine($"No hay más mensajes en la sesión {receiver.SessionId}. Cerrando...");
            break;
        }
    }

    // 4. Libera el bloqueo de la sesión para que otros puedan usarla si entran más mensajes
    await receiver.CloseAsync();
}
catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.ServiceTimeout)
{
    Console.WriteLine("No hay sesiones disponibles con mensajes para procesar en este momento.");
}

