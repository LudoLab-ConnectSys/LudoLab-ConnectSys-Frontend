namespace LudoLab_ConnectSys_Frontend.Areas.Principal.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public int? IdInstructor { get; set; }
        public int? IdEstudiante { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
    }
}