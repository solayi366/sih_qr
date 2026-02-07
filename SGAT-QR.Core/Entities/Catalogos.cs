namespace SGAT_QR.Core.Entities;

public class TipoEquipo {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
}

public class TipoPeriferico {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
}

public class Dependencia {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Estado { get; set; } = true;
}