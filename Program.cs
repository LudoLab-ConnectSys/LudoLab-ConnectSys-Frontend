using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using LudoLab_ConnectSys_Frontend;
using LudoLab_ConnectSys_Frontend.Services.Auditoria;
using LudoLab_ConnectSys_Frontend.Services.Registro;
using LudoLab_ConnectSys_Frontend.Services.Security;
using LudoLab_ConnectSys_Frontend.Shared.Utilities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Para usar LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Registrar el servicio de AuditLog
builder.Services.AddScoped<AuditLogService>();

// Mandor en el header el idUsuario y nombreUsuario
// Agregar el CustomHttpClientHandler
builder.Services.AddTransient<CustomHttpClientHandler>();

// Configurar HttpClient para usar CustomHttpClientHandler
builder.Services.AddHttpClient("AuthenticatedClient")
    .AddHttpMessageHandler<CustomHttpClientHandler>();

// Registrar AuthService usando la interfaz IAuthService
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRegistroService, RegistroService>();

// Configuraci�n del HttpClient para interactuar con tu backend
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSweetAlert2();

// Configuraci�n de la autenticaci�n MSAL
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/User.Read");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/sites.fullcontrol.all");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/sites.manage.all");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/sites.read.all");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/sites.readwrite.all");

    options.ProviderOptions.Cache.CacheLocation = "localStorage"; // Para almacenamiento local
    options.ProviderOptions.Cache.CacheLocation = "sessionStorage"; // Para almacenamiento en sesión
    options.ProviderOptions.Cache.StoreAuthStateInCookie = true;
    options.ProviderOptions.LoginMode = "popup";


});

// Configuraci�n del HttpClient para interactuar con Microsoft Graph
builder.Services.AddScoped(sp =>
{
    var authorizationMessageHandler = sp.GetRequiredService<AuthorizationMessageHandler>();
    authorizationMessageHandler.InnerHandler = new HttpClientHandler();
    authorizationMessageHandler.ConfigureHandler(
        authorizedUrls: new[] { "https://graph.microsoft.com/v1.0" },
        scopes: new[]
        {
            "User.Read",
            "sites.fullcontrol.all",
            "sites.manage.all",
            "sites.read.all",
            "sites.readwrite.all"
        });

    return new HttpClient(authorizationMessageHandler);
});

// Registro del CustomAuthenticationProvider
builder.Services.AddScoped<IAuthenticationProvider, CustomAuthenticationProvider>();

// Configuraci�n del GraphServiceClient
builder.Services.AddScoped(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    return new GraphServiceClient(httpClient);
});

await builder.Build().RunAsync();
