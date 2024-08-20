using System.Net.Http.Json;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using LudoLab_ConnectSys_Frontend.Areas.Principal.Models;
using LudoLab_ConnectSys_Frontend.Services.Contrasena; // Importar el namespace del servicio de contraseña

namespace LudoLab_ConnectSys_Frontend.Services.Security
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly IContrasenaService _contrasenaService;

        public AuthService(HttpClient httpClient, IConfiguration configuration, IJSRuntime jsRuntime,
            NavigationManager navigationManager, IContrasenaService contrasenaService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _contrasenaService = contrasenaService; // Inyectar el servicio de contraseña
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

        public async Task ManejarInicioSesionAsync(LoginRequest solicitudLogin)
        {
            var respuestaLogin = await IniciarSesionAsync(solicitudLogin);

            // Guardar en localStorage
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authTokenLC", respuestaLogin?.Token);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "IdUsuario", respuestaLogin?.IdUsuario);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "IdInstructor", respuestaLogin?.IdInstructor);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "IdEstudiante", respuestaLogin?.IdEstudiante);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "NombreUsuario", respuestaLogin?.NombreUsuario);

            try
            {
                // Verificar si el usuario ha iniciado sesión alguna vez utilizando el servicio de contraseña
                var haIniciadoAntes = await _contrasenaService.HaIniciadoSesionAsync(respuestaLogin.IdUsuario);

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
            catch (Exception ex)
            {
                throw new HttpRequestException("Error al verificar el estado de inicio de sesión del usuario. " +
                                               ex.Message);
            }
        }

        public async Task LimpiarLocalStorageAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
}