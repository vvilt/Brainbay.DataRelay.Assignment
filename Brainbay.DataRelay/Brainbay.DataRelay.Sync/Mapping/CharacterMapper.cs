namespace Brainbay.DataRelay.Sync.Mapping;
using Domain = Domain.Models;
using API = ServiceClients;
public class CharacterMapper : IMapper<Domain::Character, API::Character>
{
    public Domain.Character Map(API::Character source)
    {
        return new Domain.Character
        {
            Created = source.Created,
            ExternalId = source.Id,
            Gender = source.Gender,
            Image = source.Image,
            Name = source.Name,
            Species = source.Species,
            Status = source.Status,
            Type = source.Type,
        };
    }

    public API.Character Map(Domain.Character source)
    {
        throw new NotImplementedException();
    }
}