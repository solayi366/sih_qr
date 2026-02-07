using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SGAT_QR.Core.Entities;

namespace SGAT_QR.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
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

        // Nombres de tablas requeridos
        modelBuilder.Entity<Equipo>().ToTable("tab_Equipos");
        modelBuilder.Entity<Periferico>().ToTable("tab_Perifericos");
        modelBuilder.Entity<Novedad>().ToTable("tab_Novedades");
        modelBuilder.Entity<TipoEquipo>().ToTable("cat_TiposEquipo");
        modelBuilder.Entity<TipoPeriferico>().ToTable("cat_TiposPerifericos");
        modelBuilder.Entity<Dependencia>().ToTable("cat_Dependencias");

        // Identity
        modelBuilder.Entity<IdentityUser<int>>().ToTable("tab_Usuarios");
        modelBuilder.Entity<IdentityRole<int>>().ToTable("tab_Roles");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("rel_UsuarioRoles");

        // Constraints y tipos
        modelBuilder.Entity<Equipo>(e => {
            e.HasIndex(x => x.Nomenclatura).IsUnique();
            e.Property(x => x.FechaRegistro).HasColumnType("DATETIME2");
        });
    }
}