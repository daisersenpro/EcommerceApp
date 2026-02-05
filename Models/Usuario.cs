namespace EcommerceApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; } // "Admin" o "Usuario"
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }

        // Relación: Un usuario puede tener muchas órdenes
        public ICollection<Orden>? Ordenes { get; set; }
    }
}
