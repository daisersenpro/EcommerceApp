namespace EcommerceApp.Models
{
    public class Orden
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaOrden { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } // "Pendiente", "Completada", "Cancelada"

        // Relaciones
        public Usuario? Usuario { get; set; }
        public ICollection<DetalleOrden>? DetallesOrden { get; set; }
    }
}
