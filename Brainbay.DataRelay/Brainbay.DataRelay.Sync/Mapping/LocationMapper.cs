using Brainbay.DataRelay.Sync.ServiceClients;
using Domain = Brainbay.DataRelay.Domain;
namespace Brainbay.DataRelay.Sync.Mapping;

public class LocationMapper : IMapper<Domain::Models.Location, Location>
{
    public Domain.Models.Location Map(Location source)
    {
        return new Domain.Models.Location
        {
            ExternalId = source.Id,
            Created = source.Created,
            Dimension = source.Dimension,
            Name = source.Name,
            Type = source.Type
        };
    }

    public Location Map(Domain.Models.Location source)
    {
        throw new NotImplementedException();
    }
}