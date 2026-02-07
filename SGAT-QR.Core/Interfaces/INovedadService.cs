using SGAT_QR.Core.Entities;

namespace SGAT_QR.Core.Interfaces;

public interface INovedadService
{
    Task<List<Novedad>> ObtenerTodasAsync();
    Task<Novedad?> ObtenerPorIdAsync(int id);
    Task<bool> GuardarAsync(Novedad novedad);
    Task<bool> EliminarAsync(int id);
    Task<bool> ResolverNovedadAsync(int id, string solucion);
    Task<int> ContarPendientesAsync(); // Método para el Dashboard
}