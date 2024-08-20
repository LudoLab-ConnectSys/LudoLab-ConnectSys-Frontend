namespace LudoLab_ConnectSys_Frontend.Services.Auditoria;

using System.Net.Http.Json;
using DirectorioDeArchivos.Shared;

public class AuditLogService
{
    private readonly HttpClient _httpClient;

    public AuditLogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AuditLog>> GetAuditLogsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<AuditLog>>("http://localhost:5500/audit");
    }
}