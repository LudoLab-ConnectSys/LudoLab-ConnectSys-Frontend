namespace LudoLab_ConnectSys_Frontend.Areas.Principal.Models
{
    public class RegistroUsuarioRequest
    {
        public string CedulaUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidosUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string CelularUsuario { get; set; }
        public string TelefonoUsuario { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int DefinicionEtnica { get; set; }
        public string Genero { get; set; }
        public bool TieneDiscapacidad { get; set; }
        public string NumeroCarnetConadis { get; set; }
        public string CallePrincipal { get; set; }
        public string Numeracion { get; set; }
        public string CalleSecundaria { get; set; }
        public string Sector { get; set; }
        public string Referencia { get; set; }
        public string CodigoPostal { get; set; }
        public int ParroquiaId { get; set; }
        public string NumeroUnico { get; set; }
        public string CorreoInstitucional { get; set; }
        public string Facultad { get; set; }
        public string Carrera { get; set; }
        public int Semestre { get; set; }
        public int RolId { get; set; }
    }
}