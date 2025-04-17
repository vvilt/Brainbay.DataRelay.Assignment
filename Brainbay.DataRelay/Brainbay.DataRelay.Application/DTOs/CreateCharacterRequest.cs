namespace Brainbay.DataRelay.Application.DTOs;

public class CreateCharacterRequest
{
    public string Name { get; set; }
    public Guid? OriginId { get; set; }
    public Guid? LocationId { get; set; }
}