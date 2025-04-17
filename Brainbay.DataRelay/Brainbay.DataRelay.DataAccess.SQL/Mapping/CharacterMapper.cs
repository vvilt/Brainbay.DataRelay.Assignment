using Brainbay.DataRelay.DataAccess.SQL.Entities;
using Domain = Brainbay.DataRelay.Domain.Models;

namespace Brainbay.DataRelay.DataAccess.SQL.Mapping;

public class CharacterMapper : IMapper<Domain::Character, Character>
{
    public Character Map(Domain.Models.Character source)
    {
        return new Character
        {
            Id = source.Id,
            Created = source.Created,
            ExternalId = source.ExternalId,
            Gender = source.Gender,
            Image = source.Image,
            Name = source.Name,
            Species = source.Species,
            Status = source.Status,
            Type = source.Type,
            OriginId = source.OriginId,
            LocationId = source.LocationId
        };
    }

    public Domain::Character Map(Character source)
    {
        return new Domain::Character
        {
            Id = source.Id,
            Created = source.Created,
            ExternalId = source.ExternalId,
            Gender = source.Gender,
            Image = source.Image,
            Name = source.Name,
            Species = source.Species,
            Status = source.Status,
            Type = source.Type
        };
    }
}