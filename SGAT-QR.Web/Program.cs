using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SGAT_QR.Core.Entities;
using SGAT_QR.Core.Interfaces;
using SGAT_QR.Infrastructure.Data;
using SGAT_QR.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de la Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(connectionString));

// 2. Configuración de Seguridad e Identity
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 3. UI, Controladores y Servicios de la Aplicación
builder.Services.AddControllersWithViews();
builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registro de servicios de negocio
builder.Services.AddScoped<IEquipoService, EquipoService>();

var app = builder.Build();

// 4. Inicialización de Datos (Seed Data)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<Usuario>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    
    // Ejecución de la siembra de datos iniciales
    DbInitializer.Initialize(context, userManager, roleManager).GetAwaiter().GetResult();
}

// 5. Pipeline de Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// El orden de estos tres es crítico en .NET 8
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

// Mapeo de rutas para controladores y componentes Blazor
app.MapControllers();
app.MapRazorComponents<SGAT_QR.Web.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();