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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddIdentity<Usuario, IdentityRole<int>>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddMudServices();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddScoped<IEquipoService, EquipoService>();
builder.Services.AddScoped<IPerifericoService, PerifericoService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var userManager = services.GetRequiredService<UserManager<Usuario>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    DbInitializer.Initialize(context, userManager, roleManager).GetAwaiter().GetResult();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 
app.MapRazorComponents<SGAT_QR.Web.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();