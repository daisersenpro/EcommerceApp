namespace EcommerceApp.Models
{
    public class DetalleOrden
    {
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // Relaciones
        public Orden? Orden { get; set; }
        public Producto? Producto { get; set; }
    }
}
