namespace Brainbay.DataRelay.Sync.Mapping;
using Domain = Domain.Models;
using API = ServiceClients;
public class EpisodeMapper : IMapper<Domain::Episode, API::Episode>
{
    public Domain.Episode Map(API::Episode source)
    {
        return new Domain.Episode
        {
            ExternalId = source.Id,
            AirDate = string.IsNullOrWhiteSpace(source.AirDate) ? null : DateOnly.Parse(source.AirDate),
            Code = source.EpisodeCode,
            Created = source.Created,
            Name = source.Name
        };
    }

    public API.Episode Map(Domain.Episode source)
    {
        throw new NotImplementedException();
    }
}