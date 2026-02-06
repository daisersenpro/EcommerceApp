namespace EcommerceApp.Models
{
    /// <summary>
    /// Representa un item en el carrito de compras.
    /// Este modelo NO se guarda en BD, solo en sesión.
    /// </summary>
    public class CarritoItem
    {
        public int ProductoId { get; set; }
        public string? NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        /// <summary>
        /// Calcula el subtotal: Cantidad × Precio
        /// </summary>
        public decimal Subtotal => Cantidad * Precio;
    }
}
