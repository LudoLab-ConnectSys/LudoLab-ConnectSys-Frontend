using CurrieTechnologies.Razor.SweetAlert2;
using LudoLab_ConnectSys_Frontend;
using LudoLab_ConnectSys_Frontend.Shared.Utilities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using Blazored.LocalStorage;
using LudoLab_ConnectSys_Frontend.Areas.Principal.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuracion del HttpClient para interactuar con tu backend
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSweetAlert2();

// Configuracion de la autenticacion MSAL
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/User.Read");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/sites.fullcontrol.all");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/sites.manage.all");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/sites.read.all");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/sites.readwrite.all");
});

// Configuracion del HttpClient para interactuar con Microsoft Graph
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

// Configuracion del GraphServiceClient
builder.Services.AddScoped(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    return new GraphServiceClient(httpClient);
});

// Configuración del HttpClient para el backend que genera JWT
builder.Services.AddScoped(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    return client;
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();