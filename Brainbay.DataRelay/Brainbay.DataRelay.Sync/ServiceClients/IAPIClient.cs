namespace Brainbay.DataRelay.Sync.ServiceClients;

public interface IAPIClient
{
    Task<ApiResponse<T>> GetPageableAsync<T>(string uri, CancellationToken cancellationToken);
}