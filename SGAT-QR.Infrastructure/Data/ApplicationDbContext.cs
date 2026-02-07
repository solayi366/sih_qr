using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SGAT_QR.Core.Entities;

namespace SGAT_QR.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Equipo> Equipos { get; set; }
    public DbSet<Periferico> Perifericos { get; set; }
    public DbSet<Novedad> Novedades { get; set; }
    public DbSet<TipoEquipo> TiposEquipo { get; set; }
    public DbSet<TipoPeriferico> TiposPeriferico { get; set; }
    public DbSet<Dependencia> Dependencias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeo de tablas principales
        modelBuilder.Entity<Equipo>().ToTable("tab_Equipos");
        modelBuilder.Entity<Periferico>().ToTable("tab_Perifericos");
        modelBuilder.Entity<Novedad>().ToTable("tab_Novedades");
        modelBuilder.Entity<TipoEquipo>().ToTable("cat_TiposEquipo");
        modelBuilder.Entity<TipoPeriferico>().ToTable("cat_TiposPerifericos");
        modelBuilder.Entity<Dependencia>().ToTable("cat_Dependencias");

        // Identity personalizado con prefijos
        modelBuilder.Entity<Usuario>().ToTable("tab_Usuarios");
        modelBuilder.Entity<IdentityRole<int>>().ToTable("tab_Roles");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("rel_UsuarioRoles");
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("tab_UsuarioClaims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("tab_UsuarioLogins");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("tab_RolClaims");
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("tab_UsuarioTokens");
    }
}