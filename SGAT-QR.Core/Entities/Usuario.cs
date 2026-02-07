using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SGAT_QR.Core.Entities;

public class Usuario : IdentityUser<int>
{
    [Required]
    [StringLength(150)]
    public string NombreCompleto { get; set; } = string.Empty;

    [Required]
    public string Rol { get; set; } = "Usuario_Final"; 

    public bool Activo { get; set; } = true;

    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}