namespace Brainbay.DataRelay.Sync.ServiceClients;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Species { get; set; }
    public string Type { get; set; }
    public string Gender { get; set; }
    public CharacterLocation Origin { get; set; }
    public CharacterLocation Location { get; set; }
    public string Image { get; set; }
    public List<string> Episode { get; set; } = new List<string>();

    private List<int>? _episodeIds;
    public List<int> EpisodeIds => _episodeIds ??= Episode.Select(e => Convert.ToInt32(new Uri(e).Segments.Last())).ToList();

    public string Url { get; set; }
    public DateTime Created { get; set; }

    public class CharacterLocation
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int? ExternalId => String.IsNullOrWhiteSpace(Url) ? null : Convert.ToInt32(new Uri(Url).Segments.Last());
    }
}