using Microsoft.Azure.Cosmos;
using System.Collections.Concurrent;

// "https://localhost:8081";

// 1. Configurar las credenciales (Reemplaza con tus datos de Azure o usa los del Emulador local)
string cosmosEndpoint = "https://nikishop-cosmos-nosql.documents.azure.com:443/";
string cosmosApiKey = ""; 

string databaseId = "TelemetriaDB";
string containerId = "Dispositivos";
string partitionKeyPath = "/ubicacion"; // La propiedad que usaremos como Partition Key

Console.WriteLine("Iniciando cliente de Azure Cosmos DB...");

// 2. Inicializar el cliente de Cosmos
// (Opcional: Si usas el emulador local, añadimos la opción de ignorar la validación estricta de SSL por el certificado autofirmado)
CosmosClientOptions opciones = new()
{
    HttpClientFactory = () => new HttpClient(new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    })
};

using CosmosClient cliente = new(cosmosEndpoint, cosmosApiKey, opciones);

try
{
    // 3. Crear la Base de Datos si no existe
    Console.WriteLine($"Creando base de datos: {databaseId}...");
    Database database = await cliente.CreateDatabaseIfNotExistsAsync(databaseId);

    // 4. Crear el Contenedor si no existe (definiendo la clave de partición)
    Console.WriteLine($"Creando contenedor: {containerId} con Partition Key: {partitionKeyPath}...");
    Container contenedor = await database.CreateContainerIfNotExistsAsync(containerId, partitionKeyPath);

    // 5. Definir el objeto/documento que queremos guardar
    // Cosmos DB guarda los objetos de C# serializándolos automáticamente a JSON.
    // IMPORTANTE: El objeto DEBE tener una propiedad en minúsculas llamada "id" (string).
    var nuevoDispositivo = new ItemDispositivo
    {
        id = "disp-101",
        Nombre = "Sensor de Temperatura A",
        Ubicacion = "Planta-Norte", // Este valor debe coincidir con la Partition Key (/ubicacion)
        Temperatura = 24.5,
        FechaRegistro = DateTime.UtcNow
    };

    // 6. Insertar (o actualizar si ya existe) el elemento en Cosmos DB
    Console.WriteLine($"Insertando elemento con id: {nuevoDispositivo.id}...");
    ItemResponse<ItemDispositivo> respuestaInsercion = await contenedor.UpsertItemAsync(
        item: nuevoDispositivo,
        partitionKey: new PartitionKey(nuevoDispositivo.Ubicacion)
    );
    Console.WriteLine($"¡Elemento guardado! Costo de la operación: {respuestaInsercion.RequestCharge} RUs.");

    // 7. Leer el elemento de forma eficiente (Point Read)
    // Al pasar el ID y la Partition Key exacta, Cosmos DB va directo al registro sin buscar en todo el clúster.
    Console.WriteLine("\nRealizando lectura directa (Point Read)...");
    ItemResponse<ItemDispositivo> respuestaLectura = await contenedor.ReadItemAsync<ItemDispositivo>(
        id: "disp-101",
        partitionKey: new PartitionKey("Planta-Norte")
    );

    ItemDispositivo dispositivoGuardado = respuestaLectura.Resource;
    Console.WriteLine($"Encontrado: {dispositivoGuardado.Nombre} | Temp: {dispositivoGuardado.Temperatura}°C | Ubicación: {dispositivoGuardado.Ubicacion}");

}
catch (CosmosException ex)
{
    Console.WriteLine($"Error de Cosmos DB (Código {ex.StatusCode}): {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error general: {ex.Message}");
}

Console.WriteLine("\nProceso finalizado. Presiona cualquier tecla para salir.");
Console.ReadKey();


// 8. Definir el modelo de datos (Clase de C#)
public class ItemDispositivo
{
    // Requerido por Cosmos DB (debe ser string e "id" en minúsculas)
    public string id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;

    // Esta propiedad mapea exactamente con nuestra clave de partición /ubicacion
    public string Ubicacion { get; set; } = string.Empty;
    public double Temperatura { get; set; }
    public DateTime FechaRegistro { get; set; }
}
