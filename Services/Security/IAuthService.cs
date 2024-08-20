using LudoLab_ConnectSys_Frontend.Areas.Principal.Models;

namespace LudoLab_ConnectSys_Frontend.Services.Security
{
    public interface IAuthService
    {
        Task<LoginResponse> IniciarSesionAsync(LoginRequest solicitudLogin);
        Task<bool> HaIniciadoSesionAntesAsync(int idUsuario);
        Task ManejarInicioSesionAsync(LoginRequest solicitudLogin);
        Task LimpiarLocalStorageAsync();
    }
}