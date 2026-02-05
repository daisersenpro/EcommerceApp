# E-Commerce System - ASP.NET MVC

Sistema de E-commerce desarrollado con **ASP.NET MVC 8.0** y **SQL Server**, con autenticación de usuarios, roles (Admin/Usuario) y gestión de productos.

## 🎯 Características

✅ **Autenticación de Usuarios**
- Registro de nuevos usuarios
- Login con validación de credenciales
- Sesiones seguras
- Logout

✅ **Gestión de Roles**
- **Admin**: Crear, editar y eliminar productos, ver estadísticas
- **Usuario Normal**: Ver productos, realizar compras

✅ **Gestión de Productos**
- Listar productos disponibles
- Ver detalles de productos
- Crear productos (Admin)
- Editar productos (Admin)
- Eliminar productos (Admin - soft delete)

✅ **Dashboard Personalizado**
- Dashboard diferenciado por rol
- Panel Admin con estadísticas (usuarios, productos, órdenes)
- Panel Usuario con historial de órdenes

✅ **Base de Datos**
- SQL Server con Entity Framework Core
- Migraciones automáticas
- 4 tablas con relaciones: Usuarios, Productos, Órdenes, DetallesOrden

## 🛠️ Stack Tecnológico

- **Backend**: ASP.NET Core MVC 8.0
- **Lenguaje**: C#
- **Base de Datos**: SQL Server
- **ORM**: Entity Framework Core 8.0
- **Frontend**: HTML5, CSS3, Razor (vistas .cshtml)

## 📋 Requisitos Previos

- **.NET 8.0 SDK** o superior
- **SQL Server 2019** o superior
- **Visual Studio Code** o **Visual Studio 2022**

## 🚀 Instalación y Setup

### 1. Clonar el repositorio
```bash
git clone https://github.com/daisersenpro/EcommerceApp.git
cd EcommerceApp
```

### 2. Configurar la conexión a BD
Editar `appsettings.json` y actualizar la cadena de conexión:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=SENPROGRAMADOR\\SQLEXPRESS;Database=EcommerceApp;User Id=sa;Password=123456;TrustServerCertificate=true;"
}
```

### 3. Restaurar dependencias
```bash
dotnet restore
```

### 4. Crear/Actualizar la Base de Datos
```bash
dotnet ef database update
```

### 5. Ejecutar la aplicación
```bash
dotnet run
```

La aplicación estará disponible en:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000

## 📁 Estructura del Proyecto

```
EcommerceApp/
├── Controllers/              # Controladores MVC
│   ├── HomeController.cs
│   ├── AccountController.cs  # Login/Registro
│   ├── DashboardController.cs
│   └── ProductController.cs
├── Models/                   # Entidades de Base de Datos
│   ├── Usuario.cs
│   ├── Producto.cs
│   ├── Orden.cs
│   └── DetalleOrden.cs
├── Views/                    # Vistas Razor (.cshtml)
│   ├── Home/
│   ├── Account/
│   ├── Dashboard/
│   └── Product/
├── Data/
│   └── EcommerceDbContext.cs # DbContext de Entity Framework
├── Services/                 # Lógica de negocio (vacío, para futuro)
├── wwwroot/                  # Archivos estáticos
│   ├── css/
│   │   └── style.css
│   └── js/
├── Migrations/               # Migraciones de BD
├── Program.cs                # Configuración principal
├── appsettings.json          # Configuración de la app
└── EcommerceApp.csproj       # Archivo del proyecto

```

## 🔑 Flujo de Usuarios

### 1. **Usuario No Autenticado**
- Accede a `/Home/Index` (página de inicio)
- Puede ver `/Account/Login` (iniciar sesión)
- Puede ver `/Account/Register` (registrarse)
- Puede ver productos en `/Product/Index` (sin comprar)

### 2. **Usuario Registrado**
- Accede a `/Dashboard/Index` (panel de usuario)
- Ve sus órdenes pasadas
- Puede ver y comprar productos
- Puede hacer logout

### 3. **Usuario Admin**
- Accede a `/Dashboard/Index` (panel admin)
- Ve estadísticas (total usuarios, productos, órdenes)
- Puede crear productos: `/Product/Create`
- Puede editar productos: `/Product/Edit/{id}`
- Puede eliminar productos: `/Product/Delete/{id}`
- Puede gestionar usuarios y órdenes (funcionalidad a implementar)

## 💾 Base de Datos

### Tabla: Usuarios
```sql
Id (PK, int)
Nombre (string)
Email (string, unique)
Contraseña (string)
Rol (string: "Admin" o "Usuario")
FechaRegistro (DateTime)
Activo (bool)
```

### Tabla: Productos
```sql
Id (PK, int)
Nombre (string)
Descripcion (string)
Precio (decimal)
Stock (int)
Categoria (string)
FechaCreacion (DateTime)
Activo (bool)
```

### Tabla: Ordenes
```sql
Id (PK, int)
UsuarioId (FK, int)
FechaOrden (DateTime)
Total (decimal)
Estado (string: "Pendiente", "Completada", "Cancelada")
```

### Tabla: DetallesOrden
```sql
Id (PK, int)
OrdenId (FK, int)
ProductoId (FK, int)
Cantidad (int)
PrecioUnitario (decimal)
Subtotal (decimal)
```

## 🔐 Seguridad (Mejoras Futuras)

⚠️ **Nota Importante**: Este es un proyecto de aprendizaje. Para producción, implementar:

- ✅ Hash de contraseñas (BCrypt, PBKDF2)
- ✅ Validación de entrada (XSS prevention)
- ✅ CSRF tokens
- ✅ HTTPS obligatorio
- ✅ Rate limiting en login
- ✅ Encriptación de datos sensibles
- ✅ Autenticación con JWT tokens (API)

## 📚 Conceptos Clave Aprendidos

### **1. Arquitectura MVC**
- **Models**: Clases que representan datos
- **Controllers**: Lógica de la aplicación y manejo de peticiones
- **Views**: Presentación al usuario (HTML con Razor)

### **2. Entity Framework Core**
- ORM que mapea objetos C# a tablas SQL
- Migraciones automáticas
- Relaciones entre entidades (1:N, N:N)

### **3. Inyección de Dependencias**
- DbContext se inyecta en Controllers automáticamente
- Facilita testing y mantenibilidad

### **4. Sesiones HTTP**
- Mantener usuario logueado durante la sesión
- Datos almacenados en `HttpContext.Session`

### **5. Vistas Razor**
- Mezcla HTML + C# con sintaxis `@`
- `@foreach`, `@if` para lógica en HTML
- `@Model` para pasar datos del Controller a la Vista

## 🧪 Testing Manual

### Crear una cuenta de prueba
1. Ir a `/Account/Register`
2. Rellenar: Nombre, Email, Contraseña
3. Hacer clic en "Registrarse"

### Iniciar sesión como Admin
1. Usar email: `admin@test.com`
2. Contraseña: `admin123`
3. (Nota: Actualmente no hay cuenta admin pre-creada, requiere manualinserción en BD)

### Crear un producto (como Admin)
1. Iniciar sesión
2. Ir a Dashboard → "Crear Nuevo Producto"
3. Rellenar formulario y guardar

## 📝 Próximos Pasos / TODO

- [ ] Crear tabla de Usuarios para Admin
- [ ] Implementar Carrito de Compras
- [ ] Completar flujo de Órdenes
- [ ] Agregar métodos de pago
- [ ] Hashear contraseñas (BCrypt)
- [ ] Validaciones avanzadas
- [ ] API REST para Angular
- [ ] Paginación en listados
- [ ] Búsqueda y filtros
- [ ] Reportes y estadísticas
- [ ] Tests unitarios (xUnit)
- [ ] Logging (Serilog)
- [ ] Deployment en Azure

## 📖 Recursos Útiles

- [Documentación ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Razor Syntax](https://docs.microsoft.com/aspnet/core/mvc/views/razor)
- [C# Documentation](https://docs.microsoft.com/dotnet/csharp)

## 👨‍💻 Autor

Desarrollado como proyecto de aprendizaje para entrevista técnica .NET.

## 📄 Licencia

Este proyecto está bajo licencia MIT.

---

**¡Espero que disfrutes aprendiendo ASP.NET MVC! 🚀**
