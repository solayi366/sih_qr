using Microsoft.AspNetCore.Identity;
using SGAT_QR.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace SGAT_QR.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        // Asegurar que la BD tenga las últimas migraciones
        context.Database.Migrate();

        // 1. Sembrar Catálogos (Si no existen)
        if (!context.TiposEquipo.Any())
        {
            context.TiposEquipo.AddRange(
                new TipoEquipo { Nombre = "Portátil" },
                new TipoEquipo { Nombre = "Escritorio" },
                new TipoEquipo { Nombre = "Servidor" }
            );
        }

        if (!context.Dependencias.Any())
        {
            context.Dependencias.AddRange(
                new Dependencia { Nombre = "Tecnología de la Información" },
                new Dependencia { Nombre = "Recursos Humanos" },
                new Dependencia { Nombre = "Contabilidad" }
            );
        }

        await context.SaveChangesAsync();

        // 2. Sembrar Roles
        string[] roles = { "Administrador", "Usuario_Final" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
            }
        }

        // 3. Sembrar Administrador
        var adminEmail = "admin@sgat.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new Usuario
            {
                UserName = adminEmail,
                Email = adminEmail,
                NombreCompleto = "Administrador del Sistema",
                Rol = "Administrador",
                Activo = true,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, "Sgat2026*");
            await userManager.AddToRoleAsync(adminUser, "Administrador");
        }

        // 4. Sembrar Equipos de Prueba (SOLO SI LA TABLA ESTÁ VACÍA)
        if (!context.Equipos.Any())
        {
            var tipo = await context.TiposEquipo.FirstAsync();
            var dep = await context.Dependencias.FirstAsync();

            context.Equipos.AddRange(
                new Equipo
                {
                    Nomenclatura = "LP-001",
                    TipoEquipoId = tipo.Id,
                    Marca = "Dell",
                    Modelo = "Latitude 5420",
                    Serial = "ABC123XYZ",
                    DependenciaId = dep.Id,
                    Estado = "Activo",
                    UsuarioAsignado = "Juan Pérez",
                    UsuarioRegistroId = adminUser.Id,
                    FechaRegistro = DateTime.Now
                },
                new Equipo
                {
                    Nomenclatura = "PC-005",
                    TipoEquipoId = tipo.Id,
                    Marca = "HP",
                    Modelo = "EliteDesk 800",
                    Serial = "HP987654",
                    DependenciaId = dep.Id,
                    Estado = "Activo",
                    UsuarioAsignado = "Soporte TI",
                    UsuarioRegistroId = adminUser.Id,
                    FechaRegistro = DateTime.Now
                }
            );
            await context.SaveChangesAsync();
        }
    }
}