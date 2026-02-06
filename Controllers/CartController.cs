using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Data;
using EcommerceApp.Models;
using System.Text.Json;

namespace EcommerceApp.Controllers
{
    public class CartController : Controller
    {
        private readonly EcommerceDbContext _context;
        private const string CarritoSessionKey = "Carrito";

        public CartController(EcommerceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene el carrito desde la sesión. Si no existe, retorna lista vacía.
        /// </summary>
        private List<CarritoItem> ObtenerCarrito()
        {
            var carritoJson = HttpContext.Session.GetString(CarritoSessionKey);
            
            if (string.IsNullOrEmpty(carritoJson))
                return new List<CarritoItem>();

            return JsonSerializer.Deserialize<List<CarritoItem>>(carritoJson) ?? new List<CarritoItem>();
        }

        /// <summary>
        /// Guarda el carrito en la sesión.
        /// </summary>
        private void GuardarCarrito(List<CarritoItem> carrito)
        {
            var carritoJson = JsonSerializer.Serialize(carrito);
            HttpContext.Session.SetString(CarritoSessionKey, carritoJson);
        }

        // POST: /Cart/AddToCart
        [HttpPost]
        public IActionResult AddToCart(int productoId, int cantidad)
        {
            // Verificar que el usuario esté logueado
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(usuarioId))
                return RedirectToAction("Login", "Account");

            // Obtener el producto de la BD
            var producto = _context.Productos.FirstOrDefault(p => p.Id == productoId && p.Activo);
            
            if (producto == null)
                return NotFound("Producto no encontrado");

            // Validar cantidad
            if (cantidad <= 0 || cantidad > producto.Stock)
                return BadRequest("Cantidad inválida");

            // Obtener carrito actual
            var carrito = ObtenerCarrito();

            // Buscar si el producto ya está en el carrito
            var itemEnCarrito = carrito.FirstOrDefault(c => c.ProductoId == productoId);

            if (itemEnCarrito != null)
            {
                // Si ya existe, aumentar cantidad
                itemEnCarrito.Cantidad += cantidad;
            }
            else
            {
                // Si no existe, agregarlo
                carrito.Add(new CarritoItem
                {
                    ProductoId = productoId,
                    NombreProducto = producto.Nombre,
                    Cantidad = cantidad,
                    Precio = producto.Precio
                });
            }

            // Guardar carrito en sesión
            GuardarCarrito(carrito);

            return RedirectToAction("Index");
        }

        // GET: /Cart/Index
        public IActionResult Index()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(usuarioId))
                return RedirectToAction("Login", "Account");

            var carrito = ObtenerCarrito();
            return View(carrito);
        }

        // POST: /Cart/RemoveFromCart
        [HttpPost]
        public IActionResult RemoveFromCart(int productoId)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.FirstOrDefault(c => c.ProductoId == productoId);

            if (item != null)
                carrito.Remove(item);

            GuardarCarrito(carrito);
            return RedirectToAction("Index");
        }

        // POST: /Cart/UpdateQuantity
        [HttpPost]
        public IActionResult UpdateQuantity(int productoId, int cantidad)
        {
            if (cantidad <= 0)
                return BadRequest("Cantidad inválida");

            var carrito = ObtenerCarrito();
            var item = carrito.FirstOrDefault(c => c.ProductoId == productoId);

            if (item != null)
                item.Cantidad = cantidad;

            GuardarCarrito(carrito);
            return RedirectToAction("Index");
        }

        // GET: /Cart/Checkout
        public IActionResult Checkout()
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(usuarioId))
                return RedirectToAction("Login", "Account");

            var carrito = ObtenerCarrito();

            if (carrito.Count == 0)
            {
                ViewBag.Error = "El carrito está vacío";
                return RedirectToAction("Index");
            }

            return View(carrito);
        }

        // POST: /Cart/Checkout
        [HttpPost]
        public IActionResult CheckoutConfirm()
        {
            var usuarioIdStr = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(usuarioIdStr) || !int.TryParse(usuarioIdStr, out int usuarioId))
                return RedirectToAction("Login", "Account");

            var carrito = ObtenerCarrito();

            if (carrito.Count == 0)
                return RedirectToAction("Index");

            // Calcular total
            decimal total = carrito.Sum(c => c.Subtotal);

            // Crear orden
            var orden = new Orden
            {
                UsuarioId = usuarioId,
                FechaOrden = DateTime.Now,
                Total = total,
                Estado = "Pendiente"
            };

            _context.Ordenes.Add(orden);
            _context.SaveChanges(); // Guardar para obtener el ID

            // Crear detalles de la orden
            foreach (var item in carrito)
            {
                var detalle = new DetalleOrden
                {
                    OrdenId = orden.Id,
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.Precio,
                    Subtotal = item.Subtotal
                };

                _context.DetallesOrden.Add(detalle);
            }

            _context.SaveChanges();

            // Limpiar el carrito
            HttpContext.Session.Remove(CarritoSessionKey);

            ViewBag.Success = $"¡Compra realizada! Tu orden #{orden.Id} ha sido creada.";
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
