using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using LudoLab_ConnectSys_Frontend.Areas.Principal.Models;

namespace LudoLab_ConnectSys_Frontend.Services.Security
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public AuthService(HttpClient httpClient, IConfiguration configuration, IJSRuntime jsRuntime,
            NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }

        public async Task<LoginResponse> IniciarSesionAsync(LoginRequest solicitudLogin)
        {
            var authServiceBaseUrl = _configuration["AuthServiceBaseUrl"];
            var response = await _httpClient.PostAsJsonAsync($"{authServiceBaseUrl}/auth/login", solicitudLogin);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error durante el inicio de sesión: {error}");
            }
        }

        public async Task<bool> HaIniciadoSesionAntesAsync(int idUsuario)
        {
            var passwordServiceBaseUrl = _configuration["PasswordServiceBaseUrl"];
            var respuestaUltimoInicio =
                await _httpClient.GetAsync($"{passwordServiceBaseUrl}/password/lastlogin/{idUsuario}");

            if (respuestaUltimoInicio.IsSuccessStatusCode)
            {
                return await respuestaUltimoInicio.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                return false;
            }
        }

        public async Task ManejarInicioSesionAsync(LoginRequest solicitudLogin)
        {
            var respuestaLogin = await IniciarSesionAsync(solicitudLogin);

            // Guardar en localStorage
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authTokenLC", respuestaLogin?.Token);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "IdUsuario", respuestaLogin?.IdUsuario);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "IdInstructor", respuestaLogin?.IdInstructor);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "IdEstudiante", respuestaLogin?.IdEstudiante);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "NombreUsuario", respuestaLogin?.NombreUsuario);

            // Verificar si el usuario ha iniciado sesión alguna vez
            var haIniciadoAntes = await HaIniciadoSesionAntesAsync(respuestaLogin!.IdUsuario);

            if (!haIniciadoAntes)
            {
                _navigationManager.NavigateTo("/password");
                return;
            }

            // Redirigir al usuario a la página correspondiente según el rol
            switch (solicitudLogin.Rol)
            {
                case "Administrador":
                    _navigationManager.NavigateTo("/adminDashboard");
                    break;
                case "Estudiante":
                    _navigationManager.NavigateTo("/studentDashboard");
                    break;
                case "Instructor":
                    _navigationManager.NavigateTo("/instructorDashboard");
                    break;
                default:
                    throw new HttpRequestException("Rol no válido");
            }
        }

        public async Task LimpiarLocalStorageAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
}