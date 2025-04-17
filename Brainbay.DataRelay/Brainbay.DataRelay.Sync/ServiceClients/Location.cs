namespace Brainbay.DataRelay.Sync.ServiceClients;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Dimension { get; set; }
    public List<string> Residents { get; set; } = new List<string>();
    public string Url { get; set; }
    public DateTime Created { get; set; }
}