# Historial de Chat - Proyecto NikiShop

## 1. Creación del Proyecto
**Prompt:** crea una webapi llamada NikiShop.Ecommerce.WebApi en una solucion llamada NikiShop con la version 5.0 de .net
**Respuesta:** Se creó la solución `NikiShop.slnx` y el proyecto `NikiShop.Ecommerce.WebApi` con el target framework `net5.0`.

## 2. Instalación de Dependencias
**Prompt:** Agrega todos los paquetes de nuget necesarios para conectarte a la base de datos ecommerce de esta server: nikishop.database.windows.net con el usuario AdminUno y password Auladiser*123456
**Respuesta:** Se instalaron los paquetes `Microsoft.EntityFrameworkCore.SqlServer` y `Microsoft.EntityFrameworkCore.Tools` en su versión 5.0.17.

## 3. Configuración de Base de Datos
**Prompt:** si (Confirmación para configurar DbContext y Connection String)
**Respuesta:** 
- Se agregó la cadena de conexión en `appsettings.json`.
- Se creó `Data/ApplicationDbContext.cs`.
- Se registró el contexto en `Startup.cs`.

## 4. Scaffolding del Modelo
**Prompt:** usa dotnet ef dbcontext scaffold para crear el modelo producto de la tabla productos
**Respuesta:** Se generó el modelo `Producto.cs` en la carpeta `Models` y se vinculó al `ApplicationDbContext`.

## 5. Creación del Controlador
**Prompt:** de momento genera el controlador para leer solo los productos
**Respuesta:** Se creó `ProductosController.cs` con métodos GET para listar productos y obtener uno por ID.

## 6. Apertura en Visual Studio
**Prompt:** abre la solucion con vs 2026
**Respuesta:** Se ejecutó el comando para abrir `NikiShop.slnx` en el IDE.

## 7. Ejecución y Swagger
**Prompt:** ejecuta el proyecto para verlo con swagger
**Respuesta:** Se inició el proyecto en segundo plano y se proporcionó la URL `https://localhost:5001/swagger`.

## 8. Corrección de Clave Primaria
**Prompt:** arregla este error: The entity type 'Producto' requires a primary key to be defined.
**Respuesta:** Se añadieron los atributos `[Key]` y `[Table("Productos")]` al modelo `Producto.cs`.

## 9. Mapeo de Columnas (Snake Case)
**Prompt:** corrige este error Microsoft.Data.SqlClient.SqlException ... Invalid column name 'IdProducto' ... en la tabla los nombres son estos: id_producto, nombre, precio_venta, stock_actual, id_categoria
**Respuesta:** Se añadieron atributos `[Column]` para mapear las propiedades PascalCase a las columnas snake_case de la base de datos.

## 10. Implementación de Paginación
**Prompt:** introduce paginacion en el endpoint para traerse los primeros 20
**Respuesta:** Se modificó el método `GetProductos` para incluir `.Take(20)` en la consulta LINQ.

## 11. Visualización de Queries SQL
**Prompt:** agrega la opcion para ver la salida del query generado por EF Core
**Respuesta:** Se configuró el nivel de log `Microsoft.EntityFrameworkCore.Database.Command` en `Information` dentro de `appsettings.Development.json` para mostrar el SQL generado en la consola.

## 12. Creación del Proyecto MVC
**Prompt:** okay, transforma todo la webapi ahora a un proyecto de mvc (pero en otro proyecto llamado NikiShop.Ecommerce.WebMvc)
**Respuesta:** 
- Se revirtieron los cambios de MVC en el proyecto Web API.
- Se creó un nuevo proyecto `NikiShop.Ecommerce.WebMvc` (.NET 5.0).
- Se configuró el acceso a base de datos (EF Core) y se copiaron los modelos.
- Se implementó un `ProductosController` y una vista `Index` para listar los productos en formato HTML.

## 13. Creación de Aplicación React
**Prompt:** ahora genera una app en react.js que se conecte a la webapi y regrese los productos
**Respuesta:** 
- Se habilitó **CORS** en el proyecto Web API para permitir peticiones desde el frontend.
- Se creó una nueva aplicación React utilizando **Vite** en la carpeta `NikiShop.Ecommerce.React`.
- Se implementó un componente principal (`App.jsx`) que consume el endpoint paginado de la API (`/api/productos?page=1&pageSize=20`) usando `fetch`.
- Se diseñó una interfaz con una tabla estilizada para mostrar la lista de productos.

---

## ¿Qué es Entity Framework Core (EF Core)?

Entity Framework Core es un mapeador de objetos y bases de datos (**ORM - Object-Relational Mapper**) moderno, ligero, extensible y multiplataforma para .NET.

### Funciones Principales:
- **Productividad:** Permite a los desarrolladores trabajar con bases de datos utilizando objetos de .NET, eliminando la necesidad de escribir la mayor parte del código de acceso a datos (SQL) manualmente.
- **LINQ (Language Integrated Query):** Permite realizar consultas a la base de datos utilizando sintaxis de C#, lo que proporciona validación en tiempo de compilación y tipado fuerte.
- **Seguimiento de Cambios (Change Tracking):** Detecta automáticamente las modificaciones realizadas en los objetos para persistirlas en la base de datos de forma eficiente.
- **Migraciones:** Facilita la evolución del esquema de la base de datos a medida que el modelo de datos de la aplicación cambia.
- **Multiplataforma:** Compatible con SQL Server, Azure SQL, SQLite, PostgreSQL, MySQL y muchos otros motores de bases de datos.

En este proyecto, hemos utilizado EF Core para mapear la tabla `Productos` de Azure SQL a la clase `Producto` de C#, permitiendo listar y consultar productos de forma sencilla y segura.
