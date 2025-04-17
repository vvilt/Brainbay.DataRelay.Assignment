namespace Brainbay.DataRelay.Domain.Models;

public class Episode : IIdentifiable, ISyncable<int?>, IAuditable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly? AirDate { get; set; }
    public string Code { get; set; }
    public ICollection<Character> Characters { get; set; }
    public int? ExternalId { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
}