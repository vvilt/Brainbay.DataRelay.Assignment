namespace Brainbay.DataRelay.Application.DTOs;

public class CharacterSummary
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Location { get; set; }
    public string? Origin { get; set; }
}