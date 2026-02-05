namespace EcommerceApp.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }

        // Relación: Un producto puede estar en muchos detalles de orden
        public ICollection<DetalleOrden>? DetallesOrden { get; set; }
    }
}
