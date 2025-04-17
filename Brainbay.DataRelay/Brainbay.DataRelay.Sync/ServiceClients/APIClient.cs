using System.Net.Http.Json;

namespace Brainbay.DataRelay.Sync.ServiceClients;

public class APIClient : IAPIClient
{
    private readonly HttpClient _httpClient;

    public APIClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<ApiResponse<T>> GetPageableAsync<T>(string uri, CancellationToken cancellationToken)
    {
        return await (await _httpClient.GetAsync(uri
                , cancellationToken))
            .Content.ReadFromJsonAsync<ApiResponse<T>>(cancellationToken);
    }
}