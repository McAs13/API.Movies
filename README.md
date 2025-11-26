# API.Movies

API REST construida con ASP.NET Core 8 para gestionar categorías y películas. Implementa CRUD completo para ambos recursos, usa Entity Framework Core con SQL Server y expone documentación interactiva vía Swagger en entornos de desarrollo.

## Requisitos previos
- .NET 8 SDK.
- Instancia de SQL Server accesible para la cadena de conexión `SqlConnection` definida en `appsettings.json` (`Server=.;Database=APIMovies;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true`).

## Configuración y ejecución
1. Restaurar dependencias: `dotnet restore`.
2. Ajustar la cadena de conexión en `appsettings.json` o `appsettings.Development.json` según tu entorno.
3. Crear la base de datos con las migraciones incluidas: `dotnet ef database update --project API.Movies`.
4. Ejecutar la API: `dotnet run --project API.Movies`.

Swagger UI se habilita automáticamente cuando el entorno es `Development`.

## Endpoints principales
### Categorías (`/api/Categories`)
| Método | Ruta | Cuerpo esperado |
| --- | --- | --- |
| GET | `/api/Categories` | — |
| GET | `/api/Categories/{id}` | — |
| POST | `/api/Categories` | `{ "name": "string" }` (obligatorio, máximo 100 caracteres) |
| PUT | `/api/Categories/{id}` | `{ "name": "string" }` (obligatorio, máximo 100 caracteres) |
| DELETE | `/api/Categories/{id}` | — |

### Películas (`/api/Movies`)
| Método | Ruta | Cuerpo esperado |
| --- | --- | --- |
| GET | `/api/Movies` | — |
| GET | `/api/Movies/{id}` | — |
| POST | `/api/Movies` | `{ "name": "string", "duration": int, "description": "string", "clasification": "string" }` (nombre máx. 100, clasificación máx. 10) |
| PUT | `/api/Movies/{id}` | `{ "name": "string", "duration": int, "description": "string", "clasification": "string" }` (mismos requisitos que POST) |
| DELETE | `/api/Movies/{id}` | — |

## Modelos y validaciones
- **Category**: `Name` requerido (máx. 100). Hereda auditoría básica (`Id`, `CreatedDate`, `ModifiedDate`).
- **Movie**: `Name` requerido (máx. 100), `Duration` requerido, `Description` requerida (máx. 1000), `Clasification` requerida (máx. 10). También incluye campos de auditoría.

Los DTOs (`CategoryDto`, `CategoryCreateUpdateDto`, `MovieDto`, `MovieCreateUpdateDto`) replican estas reglas de validación en las solicitudes y respuestas.

## Arquitectura
- **Controladores**: `CategoriesController` y `MoviesController` exponen los endpoints REST y gestionan las respuestas HTTP.
- **Servicios**: `CategoryService` y `MovieService` aplican reglas de negocio como validación de duplicados y mapeo entre DTOs y entidades.
- **Repositorios**: `CategoryRepository` y `MovieRepository` encapsulan el acceso a datos usando Entity Framework Core.
- **AutoMapper**: Perfil `MoviesMapper/Mappers.cs` define los mapeos entre entidades y DTOs.
- **Persistencia**: `ApplicationDbContext` configura los `DbSet` de `Categories` y `Movies`, con migraciones incluidas en `Migrations/`.

## Mantenimiento
- Para agregar nuevos campos, actualiza los modelos y DTOs correspondientes, ajusta el perfil de AutoMapper y crea una nueva migración (`dotnet ef migrations add <Nombre>`), seguida de `dotnet ef database update`.
