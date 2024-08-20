namespace LudoLab_ConnectSys_Frontend.Services.Cuentas
{
    public interface ICuentaService
    {
        Task<List<UsuarioModel>> ObtenerUsuariosSinContrasenaAsync();
        Task<bool> AprobarUsuarioAsync(int idUsuario);
        Task<bool> RechazarUsuarioAsync(int idUsuario);
    }
}