using Microsoft.EntityFrameworkCore;
using SGAT_QR.Core.Entities;
using SGAT_QR.Core.Interfaces;
using SGAT_QR.Infrastructure.Data;

namespace SGAT_QR.Infrastructure.Services;

public class NovedadService : INovedadService
{
    private readonly ApplicationDbContext _context;

    public NovedadService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Novedad>> ObtenerTodasAsync()
    {
        return await _context.Novedades
            .Include(n => n.Equipo)
            .Include(n => n.Periferico)
            .OrderByDescending(n => n.FechaReporte)
            .ToListAsync();
    }

    public async Task<Novedad?> ObtenerPorIdAsync(int id)
    {
        return await _context.Novedades
            .Include(n => n.Equipo)
            .Include(n => n.Periferico)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<bool> GuardarAsync(Novedad novedad)
    {
        if (novedad.Id == 0)
        {
            novedad.FechaReporte = DateTime.Now;
            if (string.IsNullOrEmpty(novedad.Estado)) novedad.Estado = "Reportada";
            _context.Novedades.Add(novedad);
        }
        else
        {
            _context.Novedades.Update(novedad);
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ResolverNovedadAsync(int id, string solucion)
    {
        var novedad = await _context.Novedades.FindAsync(id);
        if (novedad == null) return false;

        novedad.SolucionAplicada = solucion;
        novedad.FechaResolucion = DateTime.Now;
        novedad.Estado = "Resuelta";

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var novedad = await _context.Novedades.FindAsync(id);
        if (novedad == null) return false;

        _context.Novedades.Remove(novedad);
        return await _context.SaveChangesAsync() > 0;
    }
}