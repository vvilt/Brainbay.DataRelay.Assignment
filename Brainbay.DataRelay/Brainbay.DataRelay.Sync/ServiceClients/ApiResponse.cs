namespace Brainbay.DataRelay.Sync.ServiceClients;

public class ApiResponse<T>
{
    public PageInfo Info { get; set; }
    public List<T> Results { get; set; }
}