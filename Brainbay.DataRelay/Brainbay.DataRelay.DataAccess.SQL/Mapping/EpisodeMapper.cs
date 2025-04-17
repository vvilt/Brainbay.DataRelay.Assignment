using Brainbay.DataRelay.DataAccess.SQL.Entities;
using Domain = Brainbay.DataRelay.Domain.Models;

namespace Brainbay.DataRelay.DataAccess.SQL.Mapping;

public class EpisodeMapper : IMapper<Domain::Episode, Episode>
{
    public Episode Map(Domain.Models.Episode source)
    {
        return new Episode
        {
            Id = source.Id,
            ExternalId = source.ExternalId,
            Created = source.Created,
            AirDate = source.AirDate,
            Code = source.Code,
            Modified = source.Modified,
            Name = source.Name
        };
    }

    public Domain.Models.Episode Map(Episode source)
    {
        throw new NotImplementedException();
    }
}