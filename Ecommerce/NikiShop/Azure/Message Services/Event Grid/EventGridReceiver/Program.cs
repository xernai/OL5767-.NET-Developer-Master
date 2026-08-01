using Azure.Messaging.EventGrid;
using Azure.Messaging.EventGrid.SystemEvents;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Endpoint donde Event Grid enviará los eventos mediante HTTP POST
app.MapPost("/api/events", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    string requestBody = await reader.ReadToEndAsync();

    // Parsear los eventos recibidos
    EventGridEvent[] events = EventGridEvent.ParseMany(BinaryData.FromString(requestBody));

    foreach (EventGridEvent egEvent in events)
    {
        // 1. VALIDACIÓN INICIAL (Handshake de Azure Event Grid)
        // Azure envía un evento especial tipo 'SubscriptionValidationEvent' para verificar que este endpoint es válido.
        if (egEvent.EventType == "Microsoft.EventGrid.SubscriptionValidationEvent")
        {
            var eventData = egEvent.Data.ToObjectFromJson<SubscriptionValidationEventData>();

            Console.WriteLine($"[HANDSHAKE] Solicitud de validación recibida. Código: {eventData.ValidationCode}");

            // Responder a Azure con el código de confirmación
            var responseData = new SubscriptionValidationResponse
            {
                ValidationResponse = eventData.ValidationCode
            };

            return Results.Ok(responseData);
        }

        // 2. PROCESAR EVENTOS PERSONALIZADOS (Ej: Evento de Moto)
        Console.WriteLine("\n================ EVENTO RECIBIDO ================");
        Console.WriteLine($"Sujeto (Subject): {egEvent.Subject}");
        Console.WriteLine($"Tipo de Evento:   {egEvent.EventType}");
        Console.WriteLine($"Hora de Evento:   {egEvent.EventTime}");
        Console.WriteLine($"Datos en Raw:     {egEvent.Data}");
        Console.WriteLine("=================================================\n");
    }

    return Results.Ok();
});

Console.WriteLine("Servidor de escucha iniciado en http://localhost:5000...");
app.Run("http://localhost:5000");