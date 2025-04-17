namespace Brainbay.DataRelay.Domain.Models;

public class Location: IIdentifiable, ISyncable<int?>, IAuditable
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Dimension { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
    public Guid Id { get; set; }
    public int? ExternalId { get; set; }
}