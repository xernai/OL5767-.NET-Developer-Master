namespace NikiShop.Ecommerce.Wasm.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockActual { get; set; }
        public int? IdCategoria { get; set; }
    }
}
