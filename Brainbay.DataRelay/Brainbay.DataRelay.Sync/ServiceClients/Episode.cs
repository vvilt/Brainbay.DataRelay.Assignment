using System.Text.Json.Serialization;

namespace Brainbay.DataRelay.Sync.ServiceClients;

public class Episode
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonPropertyName("air_date")]
    public string AirDate { get; set; }
    
    [JsonPropertyName("episode")]
    public string EpisodeCode { get; set; }
    public List<string> Characters { get; set; } = new List<string>();
    public string Url { get; set; }
    public DateTime Created { get; set; }
}