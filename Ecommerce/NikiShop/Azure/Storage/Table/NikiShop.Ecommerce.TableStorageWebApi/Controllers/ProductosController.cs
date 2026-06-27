using Azure;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using NikiShop.Ecommerce.TableStorageWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace NikiShop.Ecommerce.TableStorageWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly TableClient _tableClient;
        private readonly BlobContainerClient _blobContainerClient;

        public ProductosController(TableServiceClient tableServiceClient, BlobServiceClient blobServiceClient)
        {
            // Apuntar a la tabla y contenedor correspondientes
            _tableClient = tableServiceClient.GetTableClient("CatalogoProductos");
            _blobContainerClient = blobServiceClient.GetBlobContainerClient("catalogo-productos");
        }

        // 1. GET ALL: Obtener todos los productos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = new List<ProductoEntity>();
            var queryResult = _tableClient.QueryAsync<ProductoEntity>(filter: "");

            await foreach (var producto in queryResult)
            {
                productos.Add(producto);
            }

            return Ok(productos);
        }

        // 2. GET BY ID: Obtener un producto por Categoría (PartitionKey) y Sku (RowKey)
        [HttpGet("{categoria}/{sku}")]
        public async Task<IActionResult> GetById(string categoria, string sku)
        {
            try
            {
                var response = await _tableClient.GetEntityAsync<ProductoEntity>(categoria, sku);
                return Ok(response.Value);
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                return NotFound(new { Mensaje = "Producto no encontrado." });
            }
        }

        // 3. CREATE: Insertar un producto nuevo
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductoDto dto)
        {
            var nuevoProducto = new ProductoEntity
            {
                PartitionKey = dto.Categoria,
                RowKey = dto.Sku,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock,

                // Mapeo de la nueva propiedad
                SkuProveedor = dto.SkuProveedor,

                Activo = dto.Activo,
                ImagenUrl = $"https://127.0.0.1:10000/devstoreaccount1/catalogo-productos/imagenes/{dto.Sku}.jpg"
            };

            await _tableClient.AddEntityAsync(nuevoProducto);
            return CreatedAtAction(nameof(GetById), new { categoria = dto.Categoria, sku = dto.Sku }, nuevoProducto);
        }

        // 4. UPDATE: Actualizar datos de un producto
        [HttpPut("{categoria}/{sku}")]
        public async Task<IActionResult> Update(string categoria, string sku, [FromBody] ProductoDto dto)
        {
            try
            {
                var existente = await _tableClient.GetEntityAsync<ProductoEntity>(categoria, sku);

                var productoActualizado = existente.Value;
                productoActualizado.Nombre = dto.Nombre;
                productoActualizado.Descripcion = dto.Descripcion;
                productoActualizado.Precio = dto.Precio;
                productoActualizado.Stock = dto.Stock;

                // Actualización de la nueva propiedad
                productoActualizado.SkuProveedor = dto.SkuProveedor;

                productoActualizado.Activo = dto.Activo;

                // TableUpdateMode.Replace reemplaza toda la entidad con los nuevos valores
                await _tableClient.UpsertEntityAsync(productoActualizado, TableUpdateMode.Replace);
                return Ok(productoActualizado);
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                return NotFound(new { Mensaje = "El producto que deseas actualizar no existe." });
            }
        }
        
        // 5. DELETE: Eliminar un producto
        [HttpDelete("{categoria}/{sku}")]
        public async Task<IActionResult> Delete(string categoria, string sku)
        {
            try
            {
                await _tableClient.DeleteEntityAsync(categoria, sku);

                // Opcional: Eliminar su imagen de Blob Storage si existe
                var blobClient = _blobContainerClient.GetBlobClient($"imagenes/{sku}.jpg");
                await blobClient.DeleteIfExistsAsync();

                return NoContent();
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                return NotFound();
            }
        }

        // 6. UPLOAD IMAGE: Cargar la imagen física de un producto a Blob Storage
        [HttpPost("{categoria}/{sku}/subir-imagen")]
        public async Task<IActionResult> UploadImage(string categoria, string sku, IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest("Archivo no válido.");

            try
            {
                // 1. Validar que el producto exista en la tabla NoSQL
                var productoResponse = await _tableClient.GetEntityAsync<ProductoEntity>(categoria, sku);

                // 2. Apuntar al destino en el Blob Storage
                var blobClient = _blobContainerClient.GetBlobClient($"imagenes/{sku}.jpg");

                // 3. Subir el stream del archivo directamente al emulador
                using (var stream = archivo.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, overwrite: true);
                }

                return Ok(new { Mensaje = "Imagen subida exitosamente", Url = blobClient.Uri.ToString() });
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                return NotFound(new { Mensaje = "No se puede subir una imagen para un producto inexistente." });
            }
        }
    }
}