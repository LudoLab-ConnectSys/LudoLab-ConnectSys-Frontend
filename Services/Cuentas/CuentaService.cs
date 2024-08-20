using System.Net.Http.Json;

namespace LudoLab_ConnectSys_Frontend.Services.Cuentas
{
    public class CuentaService : ICuentaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CuentaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<UsuarioModel>> ObtenerUsuariosSinContrasenaAsync()
        {
            var url = $"{_configuration["UserServiceBaseUrl"]}/api/usuario/sinContrasena";
            return await _httpClient.GetFromJsonAsync<List<UsuarioModel>>(url);
        }

        public async Task<bool> AprobarUsuarioAsync(int idUsuario)
        {
            var url = $"{_configuration["AccountApprovalServiceBaseUrl"]}/account/approve";
            var response = await _httpClient.PostAsJsonAsync(url, new { idUsuario });

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RechazarUsuarioAsync(int idUsuario)
        {
            var url = $"{_configuration["AccountApprovalServiceBaseUrl"]}/account/deny";
            var response = await _httpClient.PostAsJsonAsync(url, new { idUsuario });

            return response.IsSuccessStatusCode;
        }
    }
}