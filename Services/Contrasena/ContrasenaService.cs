using System.Net.Http.Json;

namespace LudoLab_ConnectSys_Frontend.Services.Contrasena
{
    public class ContrasenaService : IContrasenaService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ContrasenaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Método para verificar si el usuario ha iniciado sesión anteriormente
        public async Task<bool> HaIniciadoSesionAsync(int idUsuario)
        {
            var url = $"{_configuration["PasswordServiceBaseUrl"]}/api/contrasena/haIniciadoSesion/{idUsuario}";
            var respuesta = await _httpClient.GetAsync(url);
            Console.WriteLine("Respuesta: " + respuesta);
            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadFromJsonAsync<HaIniciadoSesionResponse>();
                return resultado?.HaIniciado ?? false;
            }

            return false;
        }

        // Método para cambiar la contraseña de un usuario
        public async Task<string> CambiarContrasenaAsync(int idUsuario, string nuevaContrasena)
        {
            var url =
                $"{_configuration["PasswordServiceBaseUrl"]}/api/contrasena/cambiarContrasena?idUsuario={idUsuario}";

            // Creas un objeto que contiene la nueva contraseña
            var content = new { NuevaContrasena = nuevaContrasena };

            // Lo envías como JSON
            var respuesta = await _httpClient.PostAsJsonAsync(url, content);

            if (respuesta.IsSuccessStatusCode)
            {
                return "Contraseña cambiada con éxito";
            }
            else
            {
                var error = await respuesta.Content.ReadAsStringAsync();
                return $"Error al cambiar la contraseña: {error}";
            }
        }


        // Método para obtener el ID de un usuario según su cédula
        public async Task<int> ObtenerIdUsuarioPorCedulaAsync(string cedulaUsuario)
        {
            var url =
                $"{_configuration["PasswordServiceBaseUrl"]}/api/contrasena/obtenerIdUsuarioPorCedula/{cedulaUsuario}";
            var respuesta = await _httpClient.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadFromJsonAsync<IdUsuarioResponse>();
                return resultado.IdUsuario;
            }

            return 0;
        }
    }

    // Clase para deserializar la respuesta de HaIniciadoSesion
    public class HaIniciadoSesionResponse
    {
        public bool HaIniciado { get; set; }
    }

    // Clase para deserializar la respuesta de ObtenerIdUsuarioPorCedula
    public class IdUsuarioResponse
    {
        public int IdUsuario { get; set; }
    }
}