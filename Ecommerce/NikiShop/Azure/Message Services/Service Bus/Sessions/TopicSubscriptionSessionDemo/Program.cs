using Azure.Messaging.ServiceBus;

string connectionString = "";
string topicName = "ecommerce";

//var client = new ServiceBusClient(connectionString);
//var sender = client.CreateSender(topicName);

//var message = new ServiceBusMessage("Mensaje 1 del pedido")
//{
//    SessionId = "Pedido-12345" // ID de la sesión
//};

//await sender.SendMessageAsync(message);

// 1. Crear el cliente y el remitente
await using var client = new ServiceBusClient(connectionString);
ServiceBusSender sender = client.CreateSender(topicName);

// 2. Crear una lista de mensajes mixtos (diferentes sesiones y en desorden)
var mensajes = new List<ServiceBusMessage>
{
    // Los mensajes de la misma sesión van intercalados con otras sesiones
    new ServiceBusMessage("Paso 1: Crear carrito")     { SessionId = "Usuario-A", MessageId = "Usuario-A-1" },
    new ServiceBusMessage("Paso 1: Crear carrito")     { SessionId = "Usuario-B", MessageId = "Usuario-B-1" },

    new ServiceBusMessage("Paso 2: Agregar producto")  { SessionId = "Usuario-A", MessageId = "Usuario-A-2" },
    new ServiceBusMessage("Paso 1: Crear carrito")     { SessionId = "Usuario-C", MessageId = "Usuario-C-1" },

    new ServiceBusMessage("Paso 2: Agregar producto")  { SessionId = "Usuario-B", MessageId = "Usuario-B-2" },
    new ServiceBusMessage("Paso 3: Pagar orden")       { SessionId = "Usuario-A", MessageId = "Usuario-A-3" },

    new ServiceBusMessage("Paso 2: Agregar producto")  { SessionId = "Usuario-C", MessageId = "Usuario-C-2" },
    new ServiceBusMessage("Paso 3: Pagar orden")       { SessionId = "Usuario-B", MessageId = "Usuario-B-3" },
    new ServiceBusMessage("Paso 3: Pagar orden")       { SessionId = "Usuario-C", MessageId = "Usuario-C-3" }
};

Console.WriteLine("Enviando lote de mensajes intercalados...");

// 3. Crear el lote (Batch) para optimizar el envío en una sola petición
using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

foreach (var mensaje in mensajes)
{
    // Intentar agregar el mensaje al lote actual
    if (!messageBatch.TryAddMessage(mensaje))
    {
        throw new Exception($"El mensaje es demasiado grande para caber en el lote.");
    }
}

try
{
    // 4. Enviar el lote al Topic
    await sender.SendMessagesAsync(messageBatch);
    Console.WriteLine("¡Todos los mensajes enviados con éxito!");
}
catch (Exception ex)
{
    Console.WriteLine($"Error al enviar: {ex.Message}");
}
