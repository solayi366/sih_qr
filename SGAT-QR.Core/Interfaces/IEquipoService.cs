using SGAT_QR.Core.Entities;

namespace SGAT_QR.Core.Interfaces;

public interface IEquipoService
{
    Task<List<Equipo>> ObtenerTodosAsync();
    Task<Equipo?> ObtenerPorIdAsync(int id);
    Task<List<TipoEquipo>> ObtenerTiposAsync();
    Task<List<Dependencia>> ObtenerDependenciasAsync();
    Task<bool> GuardarAsync(Equipo equipo);
    Task<bool> EliminarAsync(int id);
    Task<int> ContarTotalAsync();
    Task<byte[]> GenerarExcelAsync();
    Task<byte[]> GenerarEtiquetaPdfAsync(int id); // Nuevo: Generación de etiqueta PDF
}