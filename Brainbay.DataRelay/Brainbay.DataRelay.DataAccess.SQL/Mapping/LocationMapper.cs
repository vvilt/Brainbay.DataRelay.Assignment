using Brainbay.DataRelay.DataAccess.SQL.Entities;
using Domain = Brainbay.DataRelay.Domain.Models;

namespace Brainbay.DataRelay.DataAccess.SQL.Mapping;

public class LocationMapper : IMapper<Domain::Location, Location>
{
    public Location Map(Domain.Models.Location source)
    {
        return new Location
        {
            Id = source.Id,
            Created = source.Created,
            ExternalId = source.ExternalId,
            Dimension = source.Dimension,
            Modified = source.Modified,
            Name = source.Name,
            Type = source.Type,
        };
    }

    public Domain::Location Map(Location source)
    {
        return new Domain.Models.Location
        {
            Id = source.Id,
            Created = source.Created,
            ExternalId = source.ExternalId,
            Dimension = source.Dimension,
            Modified = source.Modified,
            Name = source.Name,
            Type = source.Type,
        };
    }
}