using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Data;
using EcommerceApp.Models;

namespace EcommerceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly EcommerceDbContext _context;

        // Inyección de dependencia: recibimos la BD
        public AccountController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string email, string contraseña)
        {
            // Buscar usuario en la BD
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Contraseña == contraseña);

            if (usuario == null)
            {
                ViewBag.Error = "Email o contraseña incorrectos";
                return View();
            }

            // Guardar datos en sesión
            HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
            HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);
            HttpContext.Session.SetString("Rol", usuario.Rol);

            // Redirigir al dashboard
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(string nombre, string email, string contraseña, string confirmar)
        {
            // Validar que las contraseñas coincidan
            if (contraseña != confirmar)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            // Validar que el email no exista
            if (_context.Usuarios.Any(u => u.Email == email))
            {
                ViewBag.Error = "El email ya está registrado";
                return View();
            }

            // Crear nuevo usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Email = email,
                Contraseña = contraseña,
                Rol = "Usuario", // Por defecto es usuario normal
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            ViewBag.Success = "¡Registro exitoso! Por favor inicia sesión.";
            return RedirectToAction("Login");
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
