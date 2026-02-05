using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Data;
using EcommerceApp.Models;

namespace EcommerceApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly EcommerceDbContext _context;

        public DashboardController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: /Dashboard/Index
        public IActionResult Index()
        {
            // Verificar si el usuario está logueado
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            var rol = HttpContext.Session.GetString("Rol");
            var nombre = HttpContext.Session.GetString("NombreUsuario");

            if (string.IsNullOrEmpty(usuarioId))
            {
                return RedirectToAction("Login", "Account");
            }

            // Pasar datos a la vista
            ViewBag.NombreUsuario = nombre;
            ViewBag.Rol = rol;
            ViewBag.UsuarioId = usuarioId;

            // Si es Admin, mostrar estadísticas
            if (rol == "Admin")
            {
                var totalUsuarios = _context.Usuarios.Count();
                var totalProductos = _context.Productos.Count();
                var totalOrdenes = _context.Ordenes.Count();

                ViewBag.TotalUsuarios = totalUsuarios;
                ViewBag.TotalProductos = totalProductos;
                ViewBag.TotalOrdenes = totalOrdenes;

                return View("IndexAdmin");
            }
            else
            {
                // Usuario normal: mostrar sus órdenes
                int id = int.Parse(usuarioId);
                var ordenes = _context.Ordenes
                    .Where(o => o.UsuarioId == id)
                    .ToList();

                ViewBag.Ordenes = ordenes;
                return View("IndexUsuario");
            }
        }

        // Acción para Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
