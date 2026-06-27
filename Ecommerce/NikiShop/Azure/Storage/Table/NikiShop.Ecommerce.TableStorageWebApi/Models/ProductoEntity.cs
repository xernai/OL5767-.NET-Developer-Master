using Azure;
using Azure.Data.Tables;
using System;

namespace NikiShop.Ecommerce.TableStorageWebApi.Models
{
    public class ProductoEntity : ITableEntity
    {
        // PartitionKey será la Categoría (ej. "Electronica", "Hogar")
        public string PartitionKey { get; set; }
        
        // RowKey será el SKU o ID del producto (ej. "PROD-ELEC-101")
        public string RowKey { get; set; }
        
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        // Propiedades de nuestro dominio
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }

        public string SkuProveedor { get; set; }
        public string ImagenUrl { get; set; }
        public bool Activo { get; set; }
    }

    // DTO para recibir la creación/actualización sin los datos de infraestructura de Azure
    public class ProductoDto
    {
        public string Categoria { get; set; }
        public string Sku { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public string SkuProveedor { get; set; }
        public bool Activo { get; set; }
    }
}