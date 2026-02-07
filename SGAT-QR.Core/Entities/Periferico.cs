using System.ComponentModel.DataAnnotations;

namespace SGAT_QR.Core.Entities;

public class Periferico {
    public int Id { get; set; }
    public int TipoPerifericoId { get; set; }
    public TipoPeriferico? TipoPeriferico { get; set; }
    [Required] [StringLength(100)] public string Marca { get; set; } = string.Empty;
    [Required] [StringLength(100)] public string Modelo { get; set; } = string.Empty;
    [Required] [StringLength(100)] public string Serial { get; set; } = string.Empty;
    public int? EquipoId { get; set; }
    public Equipo? Equipo { get; set; }
    [Required] public string Estado { get; set; } = "Activo";
    public string? QRCodeUrl { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}