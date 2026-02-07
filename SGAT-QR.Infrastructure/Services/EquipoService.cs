using Microsoft.EntityFrameworkCore;
using SGAT_QR.Core.Entities;
using SGAT_QR.Core.Interfaces;
using SGAT_QR.Infrastructure.Data;
using QRCoder;

namespace SGAT_QR.Infrastructure.Services;

public class EquipoService : IEquipoService
{
    private readonly ApplicationDbContext _context;

    public EquipoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Equipo>> ObtenerTodosAsync()
    {
        return await _context.Equipos
            .Include(e => e.TipoEquipo)
            .Include(e => e.Dependencia)
            .OrderByDescending(e => e.FechaRegistro)
            .ToListAsync();
    }

    public async Task<Equipo?> ObtenerPorIdAsync(int id)
    {
        return await _context.Equipos
            .Include(e => e.TipoEquipo)
            .Include(e => e.Dependencia)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<TipoEquipo>> ObtenerTiposAsync()
    {
        return await _context.TiposEquipo.Where(t => t.Estado).ToListAsync();
    }

    public async Task<List<Dependencia>> ObtenerDependenciasAsync()
    {
        return await _context.Dependencias.Where(d => d.Estado).ToListAsync();
    }

    public async Task<bool> GuardarAsync(Equipo equipo)
    {
        equipo.QRCodeUrl = GenerarQR(equipo.Nomenclatura);

        if (equipo.Id == 0)
        {
            equipo.FechaRegistro = DateTime.Now;
            _context.Equipos.Add(equipo);
        }
        else
        {
            equipo.FechaActualizacion = DateTime.Now;
            _context.Equipos.Update(equipo);
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var equipo = await _context.Equipos.FindAsync(id);
        if (equipo == null) return false;
        _context.Equipos.Remove(equipo);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<int> ContarTotalAsync()
    {
        return await _context.Equipos.CountAsync();
    }

    private string GenerarQR(string texto)
    {
        if (string.IsNullOrEmpty(texto)) return string.Empty;
        using QRCodeGenerator qrGenerator = new QRCodeGenerator();
        using QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
        using PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
        return $"data:image/png;base64,{Convert.ToBase64String(qrCodeAsPngByteArr)}";
    }
}