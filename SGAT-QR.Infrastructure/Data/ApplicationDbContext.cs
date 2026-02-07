using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGAT_QR.Core.Entities;

namespace SGAT_QR.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Tablas Principales
    public DbSet<Equipo> Equipos { get; set; }
    public DbSet<Periferico> Perifericos { get; set; }
    public DbSet<Novedad> Novedades { get; set; }

    // Catálogos
    public DbSet<TipoEquipo> TiposEquipo { get; set; }
    public DbSet<TipoPeriferico> TiposPerifericos { get; set; } 
    public DbSet<Dependencia> Dependencias { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Nomenclatura Profesional
        builder.Entity<Equipo>().ToTable("tab_Equipos");
        builder.Entity<Periferico>().ToTable("tab_Perifericos");
        builder.Entity<Novedad>().ToTable("tab_Novedades");
        builder.Entity<Usuario>().ToTable("tab_Usuarios");

        builder.Entity<TipoEquipo>().ToTable("cat_TiposEquipo");
        builder.Entity<TipoPeriferico>().ToTable("cat_TiposPerifericos");
        builder.Entity<Dependencia>().ToTable("cat_Dependencias");

        // Configuraciones de Identity para seguir la nomenclatura
        builder.Entity<IdentityRole<int>>().ToTable("tab_Roles");
        builder.Entity<IdentityUserRole<int>>().ToTable("rel_UsuarioRoles");
        builder.Entity<IdentityUserClaim<int>>().ToTable("tab_UsuarioClaims");
        builder.Entity<IdentityUserLogin<int>>().ToTable("tab_UsuarioLogins");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("tab_RolClaims");
        builder.Entity<IdentityUserToken<int>>().ToTable("tab_UsuarioTokens");
    }
}