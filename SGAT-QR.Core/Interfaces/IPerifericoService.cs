using SGAT_QR.Core.Entities;

namespace SGAT_QR.Core.Interfaces;

public interface IPerifericoService
{
    Task<List<Periferico>> ObtenerTodosAsync();
    Task<Periferico?> ObtenerPorIdAsync(int id);
    Task<List<TipoPeriferico>> ObtenerTiposAsync();
    Task<bool> GuardarAsync(Periferico periferico);
    Task<bool> EliminarAsync(int id);
}