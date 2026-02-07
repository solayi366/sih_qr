using Microsoft.EntityFrameworkCore;
using SGAT_QR.Core.Entities;
using SGAT_QR.Core.Interfaces;
using SGAT_QR.Infrastructure.Data;
using QRCoder;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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

    public async Task<List<TipoEquipo>> ObtenerTiposAsync() => await _context.TiposEquipo.Where(t => t.Estado).ToListAsync();
    public async Task<List<Dependencia>> ObtenerDependenciasAsync() => await _context.Dependencias.Where(d => d.Estado).ToListAsync();

    public async Task<bool> GuardarAsync(Equipo equipo)
    {
        equipo.QRCodeUrl = GenerarQR(equipo.Nomenclatura);
        if (equipo.Id == 0) { equipo.FechaRegistro = DateTime.Now; _context.Equipos.Add(equipo); }
        else { equipo.FechaActualizacion = DateTime.Now; _context.Equipos.Update(equipo); }
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var equipo = await _context.Equipos.FindAsync(id);
        if (equipo == null) return false;
        _context.Equipos.Remove(equipo);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<int> ContarTotalAsync() => await _context.Equipos.CountAsync();

    public async Task<byte[]> GenerarExcelAsync()
    {
        var datos = await ObtenerTodosAsync();
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Equipos");
        var headers = new string[] { "Nomenclatura", "Tipo", "Marca", "Modelo", "Serial", "Usuario", "Estado" };
        
        for (int i = 0; i < headers.Length; i++)
        {
            var cell = worksheet.Cell(1, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#9B111E");
            cell.Style.Font.FontColor = XLColor.White;
        }

        for (int i = 0; i < datos.Count; i++)
        {
            worksheet.Cell(i + 2, 1).Value = datos[i].Nomenclatura;
            worksheet.Cell(i + 2, 2).Value = datos[i].TipoEquipo?.Nombre;
            worksheet.Cell(i + 2, 3).Value = datos[i].Marca;
            worksheet.Cell(i + 2, 4).Value = datos[i].Modelo;
            worksheet.Cell(i + 2, 5).Value = datos[i].Serial;
            worksheet.Cell(i + 2, 6).Value = datos[i].UsuarioAsignado;
            worksheet.Cell(i + 2, 7).Value = datos[i].Estado;
        }
        worksheet.Columns().AdjustToContents();
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> GenerarEtiquetaPdfAsync(int id)
    {
        var e = await ObtenerPorIdAsync(id);
        if (e == null) return Array.Empty<byte>();

        QuestPDF.Settings.License = LicenseType.Community;

        // Tamaño exacto: 7.62cm x 2.50cm
        var customPageSize = new PageSize(7.62f, 2.50f, Unit.Centimetre);

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(customPageSize);
                page.Margin(0.2f, Unit.Centimetre); // Margen mínimo para aprovechar espacio
                
                page.Content().Row(row =>
                {
                    // Lado izquierdo: Datos del activo
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("PROPIEDAD DE: SGAT").FontSize(6).Bold().FontColor("#9B111E");
                        col.Item().PaddingTop(-2).Text(e.Nomenclatura).FontSize(10).Black().Bold();
                        col.Item().Text($"Serial: {e.Serial}").FontSize(6);
                        col.Item().Text($"Tipo: {e.TipoEquipo?.Nombre}").FontSize(6);
                        col.Item().Text($"{e.Marca} {e.Modelo}").FontSize(6).Italic();
                    });

                    // Lado derecho: QR (debe ser lo suficientemente grande para escáneres de mano)
                    if (!string.IsNullOrEmpty(e.QRCodeUrl))
                    {
                        var qrBase64 = e.QRCodeUrl.Contains(",") ? e.QRCodeUrl.Split(',')[1] : e.QRCodeUrl;
                        row.ConstantItem(1.8f, Unit.Centimetre).AlignRight().AlignMiddle().Image(Convert.FromBase64String(qrBase64));
                    }
                });
            });
        });

        return documento.GeneratePdf();
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