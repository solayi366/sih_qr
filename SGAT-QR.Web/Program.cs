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

// 1. Conexión a Base de Datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// 2. Configuración de Identidad (Identity)
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 3. Servicios de Interfaz y MudBlazor
builder.Services.AddControllersWithViews();
builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 4. Registro de Servicios de Capa de Infraestructura
builder.Services.AddScoped<IEquipoService, EquipoService>();
builder.Services.AddScoped<IPerifericoService, PerifericoService>();
builder.Services.AddScoped<INovedadService, NovedadService>();

var app = builder.Build();

// 5. Inicialización de Datos (Seed)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<Usuario>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    DbInitializer.Initialize(context, userManager, roleManager).GetAwaiter().GetResult();
}

// 6. Pipeline de Solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); // Importante: Routing antes de Auth
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 
app.MapRazorComponents<SGAT_QR.Web.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();