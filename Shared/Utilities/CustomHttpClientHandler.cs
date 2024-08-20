namespace LudoLab_ConnectSys_Frontend.Shared.Utilities;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;

public class CustomHttpClientHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public CustomHttpClientHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var idUsuario = await _localStorage.GetItemAsync<string>("idUsuario");
        var nombreUsuario = await _localStorage.GetItemAsync<string>("nombreUsuario");

        if (!string.IsNullOrEmpty(idUsuario))
        {
            request.Headers.Add("idUsuario", idUsuario);
        }

        if (!string.IsNullOrEmpty(nombreUsuario))
        {
            request.Headers.Add("nombreUsuario", nombreUsuario);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}