using Microsoft.EntityFrameworkCore;
using SGAT_QR.Core.Entities;
using SGAT_QR.Core.Interfaces;
using SGAT_QR.Infrastructure.Data;
using QRCoder;

namespace SGAT_QR.Infrastructure.Services;

public class PerifericoService : IPerifericoService
{
    private readonly ApplicationDbContext _context;

    public PerifericoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Periferico>> ObtenerTodosAsync()
    {
        return await _context.Perifericos
            .Include(p => p.TipoPeriferico)
            .Include(p => p.Equipo)
            .OrderByDescending(p => p.FechaRegistro)
            .ToListAsync();
    }

    public async Task<Periferico?> ObtenerPorIdAsync(int id)
    {
        return await _context.Perifericos
            .Include(p => p.TipoPeriferico)
            .Include(p => p.Equipo)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<TipoPeriferico>> ObtenerTiposAsync()
    {
        return await _context.TiposPerifericos.Where(t => t.Estado).ToListAsync();
    }

    public async Task<bool> GuardarAsync(Periferico periferico)
    {
        periferico.QRCodeUrl = GenerarQR(periferico.Serial);

        if (periferico.Id == 0)
        {
            periferico.FechaRegistro = DateTime.Now;
            _context.Perifericos.Add(periferico);
        }
        else
        {
            _context.Perifericos.Update(periferico);
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var p = await _context.Perifericos.FindAsync(id);
        if (p == null) return false;
        _context.Perifericos.Remove(p);
        return await _context.SaveChangesAsync() > 0;
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