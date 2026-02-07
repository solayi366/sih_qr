using System.ComponentModel.DataAnnotations;

namespace SGAT_QR.Core.Entities;

public class Novedad {
    public int Id { get; set; }
    public int? EquipoId { get; set; }
    public Equipo? Equipo { get; set; }
    public int? PerifericoId { get; set; }
    public Periferico? Periferico { get; set; }
    [Required] public string Descripcion { get; set; } = string.Empty;
    [Required] public string Severidad { get; set; } = "Media";
    [Required] public string Estado { get; set; } = "Reportada";
    public int UsuarioReportaId { get; set; }
    public DateTime FechaReporte { get; set; } = DateTime.Now;
    public string? SolucionAplicada { get; set; }
    public DateTime? FechaResolucion { get; set; }
}