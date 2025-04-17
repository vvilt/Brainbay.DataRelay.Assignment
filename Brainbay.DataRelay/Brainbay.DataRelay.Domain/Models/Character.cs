namespace Brainbay.DataRelay.Domain.Models;

public class Character : IIdentifiable, ISyncable<int?>, IAuditable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Species { get; set; }
    public string Type { get; set; }
    public string Gender { get; set; }
    public Guid? OriginId { get; private set; }
    public Guid? LocationId { get; private set; }
    public string Image { get; set; }

    private readonly HashSet<Guid> _episodeIds = new HashSet<Guid>();
    public IReadOnlyCollection<Guid> EpisodeIds => _episodeIds;
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
    public int? ExternalId { get; set; }

    public void AssignToOrigin(Guid locationId)
    {
        OriginId = locationId;
    }

    public void AssignToLocation(Guid locationId)
    {
        LocationId = locationId;
    }

    public void AddToEpisode(Guid episodeId)
    {
        _episodeIds.Add(episodeId);
    }

    public void RemoveFromEpisode(Guid episodeId)
    {
        _episodeIds.Remove(episodeId);
    }
}