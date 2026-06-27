using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Data.Tables;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public static class LocalStorageInitializer
{
    private const string ConnectionString = "UseDevelopmentStorage=true";
    public static async Task InicializarYAlimentarEstructuraLocalAsync()
    {
        Console.WriteLine("Iniciando aprovisionamiento de Azure Storage Local con Productos...");

        await InicializarBlobsAsync();
        await InicializarQueuesAsync();
        await InitializeTablesAsync();

        Console.WriteLine("¡Estructura local y catálogo de productos creados con éxito!");
    }

    private static async Task InicializarBlobsAsync()
    {
        var blobServiceClient = new BlobServiceClient(ConnectionString);

        // 1. Contenedor: Catálogo de Productos (Imágenes de los productos creados)
        var catalogoClient = blobServiceClient.GetBlobContainerClient("catalogo-productos");
        await catalogoClient.CreateIfNotExistsAsync();

        // Almacenamos placeholders para las imágenes de los nuevos productos
        string[] imagenesProductos = { "imagenes/PROD-ELEC-101.jpg", "imagenes/PROD-HOGAR-202.jpg", "imagenes/PROD-ELEC-102.jpg" };
        foreach (var rutaImagen in imagenesProductos)
        {
            var blobImagen = catalogoClient.GetBlobClient(rutaImagen);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes($"FALSO_BINARIO_DE_{rutaImagen.ToUpper()}"));
            await blobImagen.UploadAsync(stream, overwrite: true);
        }

        // 2. Contenedor: Documentación (Fichas técnicas de productos)
        var docClient = blobServiceClient.GetBlobContainerClient("documentacion-clientes");
        await docClient.CreateIfNotExistsAsync();

        var blobPdf = docClient.GetBlobClient("fichas-tecnicas/PROD-ELEC-101.pdf");
        using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("Ficha Técnica: Laptop Gamer Core i7. Consumo: 180W.")))
        {
            await blobPdf.UploadAsync(stream, overwrite: true);
        }

        // 3. Contenedor: Respaldos
        var respaldosClient = blobServiceClient.GetBlobContainerClient("respaldos-sistema");
        await respaldosClient.CreateIfNotExistsAsync();
    }

    private static async Task InicializarQueuesAsync()
    {
        var queueServiceClient = new QueueServiceClient(ConnectionString);

        // Cola de Pedidos por procesar (Incluye la referencia de los productos comprados)
        var pedidosQueue = queueServiceClient.GetQueueClient("pedidos-por-procesar");
        await pedidosQueue.CreateIfNotExistsAsync();

        var pedidoEjemplo = new
        {
            PedidoId = 1045,
            Fecha = DateTime.UtcNow,
            ClienteId = "USR-9921",
            Total = 26200.00,
            Items = new[] { "PROD-ELEC-101", "PROD-HOGAR-202" } // Referencia a los RowKeys de la tabla de productos
        };

        string jsonPedido = JsonSerializer.Serialize(pedidoEjemplo);
        await pedidosQueue.SendMessageAsync(jsonPedido);

        // Cola de Correos
        var correosQueue = queueServiceClient.GetQueueClient("correos-por-enviar");
        await correosQueue.CreateIfNotExistsAsync();
    }

    private static async Task InitializeTablesAsync()
    {
        var tableServiceClient = new TableServiceClient(ConnectionString);

        // 1. NUEVA TABLA: Catálogo de Productos
        var productosTable = tableServiceClient.GetTableClient("CatalogoProductos");
        await productosTable.CreateIfNotExistsAsync();

        // Producto 1: Electrónica
        TableEntity prod1 = new TableEntity("Electronica", "PROD-ELEC-101")
        {
            { "Nombre", "Laptop Gamer Pro 15\"" },
            { "Descripcion", "Core i7, 16GB RAM, 512GB SSD" },
            { "Precio", 24999.00 },
            { "Stock", 15 },
            { "SkuProveedor", "PROV-INTEL-882" },
            { "ImagenUrl", "https://127.0.0.1:10000/devstoreaccount1/catalogo-productos/imagenes/PROD-ELEC-101.jpg" },
            { "Activo", true }
        };

        // Producto 2: Electrónica
        TableEntity prod2 = new TableEntity("Electronica", "PROD-ELEC-102")
        {
            { "Nombre", "Mouse Ergonómico Inalámbrico" },
            { "Descripcion", "Mouse recargable con ajuste de DPI" },
            { "Precio", 450.00 },
            { "Stock", 120 },
            { "SkuProveedor", "PROV-LOGI-331" },
            { "ImagenUrl", "https://127.0.0.1:10000/devstoreaccount1/catalogo-productos/imagenes/PROD-ELEC-102.jpg" },
            { "Activo", true }
        };

        // Producto 3: Hogar
        TableEntity prod3 = new TableEntity("Hogar", "PROD-HOGAR-202")
        {
            { "Nombre", "Cafetera de Goteo Programable" },
            { "Descripcion", "Capacidad para 12 tazas con filtro permanente" },
            { "Precio", 1200.00 },
            { "Stock", 45 },
            { "SkuProveedor", "PROV-OSTER-112" },
            { "ImagenUrl", "https://127.0.0.1:10000/devstoreaccount1/catalogo-productos/imagenes/PROD-HOGAR-202.jpg" },
            { "Activo", true }
        };

        // Insertar los productos en la tabla
        await productosTable.UpsertEntityAsync(prod1);
        await productosTable.UpsertEntityAsync(prod2);
        await productosTable.UpsertEntityAsync(prod3);


        // 2. Tabla de Logs de Auditoría
        var logsTable = tableServiceClient.GetTableClient("LogsAuditoria");
        await logsTable.CreateIfNotExistsAsync();

        TableEntity log1 = new TableEntity("Inventario", Guid.NewGuid().ToString())
        {
            { "Usuario", "sistema_inventarios" },
            { "Accion", "Actualización de stock por carga inicial de productos" },
            { "Detalles", "Se insertaron 3 productos nuevos en el catálogo." },
            { "Exitoso", true }
        };
        await logsTable.AddEntityAsync(log1);

        // 3. Tabla de Telemetría
        var telemetriaTable = tableServiceClient.GetTableClient("TelemetriaSitio");
        await telemetriaTable.CreateIfNotExistsAsync();
    }
}