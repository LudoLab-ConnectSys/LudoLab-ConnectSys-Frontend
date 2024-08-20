namespace LudoLab_ConnectSys_Frontend.Services.Cuentas;

public class UsuarioModel
{
    public int IdUsuario { get; set; }

    public string CedulaUsuario { get; set; }

    public string NombreUsuario { get; set; }
    public string ApellidosUsuario { get; set; }

    public int? EdadUsuario { get; set; }
    public string CorreoUsuario { get; set; }

    public string? CelularUsuario { get; set; }

    public string? TelefonoUsuario { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public int DefinicionEtnica { get; set; }

    public string Genero { get; set; }

    public bool? TieneDiscapacidad { get; set; }

    public string? NumeroCarnetConadis { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? UltimoLogin { get; set; }

    public bool EstadoActivo { get; set; }
}