using LudoLab_ConnectSys_Frontend.Areas.Principal.Models;

namespace LudoLab_ConnectSys_Frontend.Services.Registro
{
    public interface IRegistroService
    {
        Task<List<Etnia>> ObtenerEtniasAsync();
        Task<List<Pais>> ObtenerPaisesAsync();
        Task<List<Provincia>> ObtenerProvinciasAsync();
        Task<List<Canton>> ObtenerCantonesAsync();
        Task<List<Parroquia>> ObtenerParroquiasAsync();
        Task<bool> RegistrarUsuarioAsync(RegistroUsuarioRequest model);
    }
}