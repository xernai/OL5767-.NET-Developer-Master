using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace NikiShop.Ecommerce.WebMvc.Models
{
    [Table("Productos")]
    public partial class Producto
    {
        [Key]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("precio_venta")]
        public decimal PrecioVenta { get; set; }

        [Column("stock_actual")]
        public int StockActual { get; set; }

        [Column("id_categoria")]
        public int? IdCategoria { get; set; }
    }
}
