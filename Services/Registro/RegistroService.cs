using System.Net.Http.Json;
using LudoLab_ConnectSys_Frontend.Areas.Principal.Models;
using Microsoft.Extensions.Configuration;

namespace LudoLab_ConnectSys_Frontend.Services.Registro
{
    public class RegistroService : IRegistroService
    {
        private readonly HttpClient _httpClient;

        public RegistroService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            // Leer la base URL desde appsettings.json
            var baseUrl = configuration["RegisterServiceBaseUrl"];
            if (!string.IsNullOrEmpty(baseUrl))
            {
                _httpClient.BaseAddress = new Uri(baseUrl);
            }
            else
            {
                throw new InvalidOperationException("The base URL is not configured properly.");
            }
        }

        public async Task<List<Etnia>> ObtenerEtniasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Etnia>>("/api/localizacion/etnias");
        }

        public async Task<List<Pais>> ObtenerPaisesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Pais>>("/api/localizacion/paises");
        }

        public async Task<List<Provincia>> ObtenerProvinciasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Provincia>>("/api/localizacion/provincias");
        }

        public async Task<List<Canton>> ObtenerCantonesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Canton>>("/api/localizacion/cantones");
        }

        public async Task<List<Parroquia>> ObtenerParroquiasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Parroquia>>("/api/localizacion/parroquias");
        }

        public async Task<bool> RegistrarUsuarioAsync(RegistroUsuarioRequest model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/registro", model);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Error en el registro: " + content);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el registro: " + ex.Message);
                return false;
            }
        }
    }
}