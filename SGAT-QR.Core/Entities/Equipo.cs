using System.ComponentModel.DataAnnotations;

namespace SGAT_QR.Core.Entities;

public class Equipo {
    public int Id { get; set; }
    [Required] [StringLength(50)] public string Nomenclatura { get; set; } = string.Empty;
    public int TipoEquipoId { get; set; }
    public TipoEquipo? TipoEquipo { get; set; }
    [Required] [StringLength(100)] public string Marca { get; set; } = string.Empty;
    [Required] [StringLength(100)] public string Modelo { get; set; } = string.Empty;
    [Required] [StringLength(100)] public string Serial { get; set; } = string.Empty;
    public string? SerialEnvia { get; set; }
    public string? SistemaOperativo { get; set; }
    public string? VersionSO { get; set; }
    public string? IP { get; set; }
    public string? MAC { get; set; }
    public string? Procesador { get; set; }
    public string? RAM { get; set; }
    public string? Disco { get; set; }
    public string? UsuarioAsignado { get; set; }
    public int DependenciaId { get; set; }
    public Dependencia? Dependencia { get; set; }
    public string? Ubicacion { get; set; }
    public DateTime? FechaAdquisicion { get; set; }
    [Required] public string Estado { get; set; } = "Activo";
    public string? QRCodeUrl { get; set; }
    public string? Observaciones { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public DateTime? FechaActualizacion { get; set; }
    public int UsuarioRegistroId { get; set; }
    public ICollection<Periferico> Perifericos { get; set; } = new List<Periferico>();
}