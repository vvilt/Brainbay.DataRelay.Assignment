using Brainbay.DataRelay.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Brainbay.DataRelay.DataAccess.SQL.Entities;

public class Episode : IIdentifiable, ISyncable<int?>, IAuditable
{
    public Guid Id { get; set; }
    
    [StringLength(128)]
    public string Name { get; set; }
    public DateOnly? AirDate { get; set; }

    [StringLength(32)]
    public string Code { get; set; }
    public virtual ICollection<Character> Characters { get; set; }
    public int? ExternalId { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
}