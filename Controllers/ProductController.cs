using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Data;
using EcommerceApp.Models;

namespace EcommerceApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly EcommerceDbContext _context;

        public ProductController(EcommerceDbContext context)
        {
            _context = context;
        }

        // GET: /Product/Index
        public IActionResult Index()
        {
            var productos = _context.Productos
                .Where(p => p.Activo)
                .ToList();

            return View(productos);
        }

        // GET: /Product/Details/5
        public IActionResult Details(int id)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            // Verificar que sea Admin
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Admin")
                return Forbid();

            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Admin")
                return Forbid();

            producto.FechaCreacion = DateTime.Now;
            producto.Activo = true;

            _context.Productos.Add(producto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: /Product/Edit/5
        public IActionResult Edit(int id)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Admin")
                return Forbid();

            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        // POST: /Product/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Producto producto)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Admin")
                return Forbid();

            if (id != producto.Id)
                return NotFound();

            var productoExistente = _context.Productos.FirstOrDefault(p => p.Id == id);

            if (productoExistente != null)
            {
                productoExistente.Nombre = producto.Nombre;
                productoExistente.Descripcion = producto.Descripcion;
                productoExistente.Precio = producto.Precio;
                productoExistente.Stock = producto.Stock;
                productoExistente.Categoria = producto.Categoria;

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: /Product/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var rol = HttpContext.Session.GetString("Rol");
            if (rol != "Admin")
                return Forbid();

            var producto = _context.Productos.FirstOrDefault(p => p.Id == id);

            if (producto != null)
            {
                producto.Activo = false; // Soft delete
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
