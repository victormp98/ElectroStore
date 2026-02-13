# ElectroStore

Aplicacion web ASP.NET Core MVC para tienda de electronicos, con separacion por capas (`Core`, `Infrastructure`, `Web`), Entity Framework Core y MySQL.

## 1. Estado actual verificado

Fecha de verificacion: **13 de febrero de 2026**

- `dotnet build ElectroStore.slnx` compila correctamente (0 errores, 0 warnings).
- La web responde correctamente en `http://localhost:5105` con `HTTP 200`.
- Si MySQL no esta activo, el seed falla en inicio (se registra error) pero la app puede arrancar.

## 2. Arquitectura del proyecto

- `ElectroStore.Core`: entidades e interfaces de repositorio/UnitOfWork.
- `ElectroStore.Infrastructure`: `DbContext`, repositorios EF Core, migraciones y seed.
- `ElectroStore.Web`: capa MVC (controladores, vistas, layouts responsive, TagHelper para imagenes responsivas).

## 3. Requisitos para ejecutar

- Windows 10/11 o Linux/macOS.
- .NET SDK compatible con `net11.0` (actualmente en el repo: preview).
- MySQL Server 8.x levantado en `localhost`.
- Usuario MySQL con permisos sobre base `electrostore`.

## 4. Configuracion local paso a paso

### 4.1 Clonar repositorio

```bash
git clone <URL_DEL_REPO>
cd Electrostore
```

### 4.2 Crear base de datos MySQL

```sql
CREATE DATABASE electrostore CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

### 4.3 Configurar cadena de conexion

Archivo: `ElectroStore.Web/appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;user=root;password=TU_PASSWORD;database=electrostore"
}
```

### 4.4 Restaurar y compilar

```bash
dotnet restore ElectroStore.slnx
dotnet build ElectroStore.slnx
```

### 4.5 Ejecutar migraciones (si aplica en entorno limpio)

Desde la raiz del repo:

```bash
dotnet ef database update --project ElectroStore.Infrastructure --startup-project ElectroStore.Web
```

Si no tienes `dotnet-ef`:

```bash
dotnet tool install --global dotnet-ef
```

### 4.6 Ejecutar aplicacion

```bash
dotnet run --project ElectroStore.Web --launch-profile http
```

URL local por defecto:

- `http://localhost:5105`

## 5. Validacion rapida funcional

1. Abrir `http://localhost:5105`.
2. Confirmar que carga Home (`Views/Home/Index.cshtml`).
3. Confirmar que el layout cambia en dispositivos moviles (`_Layout.Mobile.cshtml`) segun User-Agent.
4. Revisar logs de consola por errores de DB/puerto.

## 6. Manejo de imagenes (muy importante)

## 6.1 Como esta implementado hoy

Entidad `Product`:

- `ImageUrl` guarda la ruta de imagen (ej. `/images/laptop1.jpg`).

Seed inicial:

- Inserta productos con rutas `/images/laptop1.jpg` y `/images/phone1.jpg`.

TagHelper `responsive-image` (`ElectroStore.Web/TagHelpers/ResponsiveImageTagHelper.cs`):

- Toma `src` original.
- Genera automaticamente variantes:
  - mobile: `-mobile` (max-width: 576px)
  - tablet: `-tablet` (max-width: 992px)
- Fallback final: imagen original para desktop.

Ejemplo esperado de archivos para un producto:

- `wwwroot/images/laptop1.jpg` (desktop)
- `wwwroot/images/laptop1-tablet.jpg` (tablet)
- `wwwroot/images/laptop1-mobile.jpg` (mobile)

## 6.2 Convencion recomendada para GitHub

1. Crear carpeta `ElectroStore.Web/wwwroot/images`.
2. Subir siempre 3 versiones por producto (desktop/tablet/mobile).
3. Evitar nombres con multiples puntos (ej. `producto.v2.jpg`) para prevenir reemplazos inesperados en el TagHelper.
4. Mantener rutas relativas web (`/images/...`) en `ImageUrl`.

## 6.3 Como usar el TagHelper en vistas

Asegurar importacion en `ElectroStore.Web/Views/_ViewImports.cshtml`:

```cshtml
@addTagHelper *, ElectroStore.Web
```

Uso:

```cshtml
<responsive-image src="/images/laptop1.jpg" alt="Laptop" class="card-img-top"></responsive-image>
```

## 6.4 Que pasa si no existe la imagen

- El navegador mostrara imagen rota (404).
- No hay fallback automatico de placeholder en el codigo actual.

Recomendacion minima:

- Agregar una imagen default (`/images/placeholder.jpg`) y validacion al renderizar.

## 7. Problemas comunes y solucion

## 7.1 Error de conexion MySQL

Mensaje tipico:

- `Unable to connect to any of the specified MySQL hosts`

Solucion:

1. Verificar que MySQL este iniciado.
2. Validar usuario/password en `appsettings.json`.
3. Confirmar que la base `electrostore` exista.

## 7.2 Puerto en uso (`5105`)

Mensaje tipico:

- `Failed to bind to address ... address already in use`

Solucion:

```powershell
Get-NetTCPConnection -LocalPort 5105 -State Listen
Stop-Process -Id <PID> -Force
```

O ejecutar en otro puerto:

```bash
dotnet run --project ElectroStore.Web --urls http://localhost:5200
```

## 8. Flujo recomendado para agente Victor

1. Clonar repo.
2. Configurar `appsettings.json` con credenciales locales.
3. Ejecutar `dotnet restore` y `dotnet build`.
4. Ejecutar `dotnet ef database update`.
5. Verificar carpeta `wwwroot/images` y archivos de variantes.
6. Ejecutar `dotnet run --project ElectroStore.Web --launch-profile http`.
7. Probar Home y comportamiento mobile/desktop.

## 9. Publicacion en GitHub

## 9.1 Archivos que SI deben subirse

- Codigo fuente (`Core`, `Infrastructure`, `Web`).
- `ElectroStore.slnx`.
- Migraciones en `ElectroStore.Infrastructure/Migrations`.
- `README.md`.
- Recursos estaticos necesarios (`wwwroot/images`, css/js, etc.).

## 9.2 Archivos que NO deben subirse

Agregar/validar `.gitignore` para excluir:

- `bin/`
- `obj/`
- `.vs/`
- secretos o credenciales reales

## 10. Comandos resumen

```bash
# restaurar + compilar
dotnet restore ElectroStore.slnx
dotnet build ElectroStore.slnx

# aplicar migraciones
dotnet ef database update --project ElectroStore.Infrastructure --startup-project ElectroStore.Web

# correr app
dotnet run --project ElectroStore.Web --launch-profile http
```
