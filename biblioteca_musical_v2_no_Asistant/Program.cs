using biblioteca_musical_v2_no_Asistant.Models;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Leer la cadena de conexión desde el archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MiConexion");
Console.WriteLine($"[DEBUG] Connection String: {connectionString}");

//Registrar EF Core y el DbContext
builder.Services.AddDbContext<BibliotecaMusicalContext>(options =>
    options.UseSqlServer(connectionString)
);

// Agregar servicios al contenedor. Registra el MVC con vistas.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura el http request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // El default HSTS (HTTP Strict Transport Security) es 30 días. Te puede interesar cambiarlo para escenarios de producción, consulta https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ruta defeault para los controladores y vistas.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
