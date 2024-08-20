namespace LudoLab_ConnectSys_Frontend.Services.Contrasena
{
    public interface IContrasenaService
    {
        Task<bool> HaIniciadoSesionAsync(int idUsuario);
        Task<string> CambiarContrasenaAsync(int idUsuario, string nuevaContrasena);
        Task<int> ObtenerIdUsuarioPorCedulaAsync(string cedulaUsuario);
    }
}