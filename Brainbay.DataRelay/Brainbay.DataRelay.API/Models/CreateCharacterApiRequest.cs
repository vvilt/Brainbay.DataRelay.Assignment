namespace Brainbay.DataRelay.API.Models;

public class CreateCharacterApiRequest
{
    public string Name { get; set; }
    public Guid? OriginId { get; set; }
    public Guid? LocationId { get; set; }
}