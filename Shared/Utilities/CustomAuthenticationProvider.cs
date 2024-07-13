using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using System.Net.Http.Headers;

namespace LudoLab_ConnectSys_Frontend.Shared.Utilities
{
    public class CustomAuthenticationProvider : IAuthenticationProvider
    {
        private readonly HttpClient _httpClient;

        public CustomAuthenticationProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var token = await _httpClient.GetStringAsync("/auth/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
