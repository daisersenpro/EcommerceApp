using Microsoft.EntityFrameworkCore;
using EcommerceApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la aplicación
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Agregar Entity Framework y conexión a SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configurar el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Crear datos de prueba si no existen
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();
    
    // Verificar si ya hay productos
    if (context.Productos != null && !context.Productos.Any())
    {
        Console.WriteLine("📦 Creando productos de prueba...");
        
        var productos = new[]
        {
            new EcommerceApp.Models.Producto
            {
                Nombre = "Laptop Dell XPS 15",
                Descripcion = "Laptop de alto rendimiento con procesador Intel i7, 16GB RAM, 512GB SSD",
                Precio = 1299.99M,
                Stock = 10,
                Categoria = "Electrónica",
                FechaCreacion = DateTime.Now,
                Activo = true
            },
            new EcommerceApp.Models.Producto
            {
                Nombre = "Mouse Logitech MX Master 3",
                Descripcion = "Mouse inalámbrico ergonómico con precisión de 4000 DPI",
                Precio = 99.99M,
                Stock = 25,
                Categoria = "Accesorios",
                FechaCreacion = DateTime.Now,
                Activo = true
            },
            new EcommerceApp.Models.Producto
            {
                Nombre = "Teclado Mecánico Keychron K2",
                Descripcion = "Teclado mecánico inalámbrico con switches Blue, RGB backlight",
                Precio = 89.99M,
                Stock = 15,
                Categoria = "Accesorios",
                FechaCreacion = DateTime.Now,
                Activo = true
            },
            new EcommerceApp.Models.Producto
            {
                Nombre = "Monitor LG UltraWide 34\"",
                Descripcion = "Monitor curvo ultrawide 3440x1440, 144Hz, IPS",
                Precio = 599.99M,
                Stock = 8,
                Categoria = "Electrónica",
                FechaCreacion = DateTime.Now,
                Activo = true
            },
            new EcommerceApp.Models.Producto
            {
                Nombre = "Webcam Logitech C920",
                Descripcion = "Cámara web Full HD 1080p con micrófonos estéreo",
                Precio = 79.99M,
                Stock = 20,
                Categoria = "Accesorios",
                FechaCreacion = DateTime.Now,
                Activo = true
            },
            new EcommerceApp.Models.Producto
            {
                Nombre = "Auriculares Sony WH-1000XM4",
                Descripcion = "Auriculares inalámbricos con cancelación de ruido activa",
                Precio = 349.99M,
                Stock = 12,
                Categoria = "Audio",
                FechaCreacion = DateTime.Now,
                Activo = true
            },
            new EcommerceApp.Models.Producto
            {
                Nombre = "SSD Samsung 970 EVO 1TB",
                Descripcion = "Disco sólido NVMe M.2 con velocidad de lectura 3500 MB/s",
                Precio = 149.99M,
                Stock = 30,
                Categoria = "Componentes",
                FechaCreacion = DateTime.Now,
                Activo = true
            },
            new EcommerceApp.Models.Producto
            {
                Nombre = "Silla Gaming DXRacer",
                Descripcion = "Silla ergonómica para gaming con reclinación 180°",
                Precio = 299.99M,
                Stock = 5,
                Categoria = "Muebles",
                FechaCreacion = DateTime.Now,
                Activo = true
            }
        };
        
        context.Productos.AddRange(productos);
        context.SaveChanges();
        
        Console.WriteLine($"✅ {productos.Length} productos creados exitosamente!");
    }
    else
    {
        Console.WriteLine("✓ La base de datos ya contiene productos.");
    }
    
    // Crear usuarios de prueba si no existen
    if (context.Usuarios != null && !context.Usuarios.Any())
    {
        Console.WriteLine("👥 Creando usuarios de prueba...");
        
        var usuarios = new[]
        {
            new EcommerceApp.Models.Usuario
            {
                Nombre = "Administrador",
                Email = "admin@ecommerce.com",
                Contraseña = "admin123",
                Rol = "Admin",
                FechaRegistro = DateTime.Now,
                Activo = true
            },
            new EcommerceApp.Models.Usuario
            {
                Nombre = "Usuario Demo",
                Email = "user@ecommerce.com",
                Contraseña = "user123",
                Rol = "Usuario",
                FechaRegistro = DateTime.Now,
                Activo = true
            }
        };
        
        context.Usuarios.AddRange(usuarios);
        context.SaveChanges();
        
        Console.WriteLine($"✅ {usuarios.Length} usuarios creados exitosamente!");
        Console.WriteLine("\n📝 Credenciales de prueba:");
        Console.WriteLine("   ADMIN  → Email: admin@ecommerce.com | Password: admin123");
        Console.WriteLine("   USUARIO → Email: user@ecommerce.com  | Password: user123\n");
    }
    else
    {
        Console.WriteLine("✓ La base de datos ya contiene usuarios.");
    }
}

app.Run();
