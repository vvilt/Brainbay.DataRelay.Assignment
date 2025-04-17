using System.ComponentModel.DataAnnotations;
using Brainbay.DataRelay.Domain.Models;

namespace Brainbay.DataRelay.DataAccess.SQL.Entities;
public class Character : IIdentifiable, ISyncable<int?>, IAuditable
{
    public Guid Id { get; set; }
    
    [StringLength(128)]
    public string Name { get; set; }

    [StringLength(32)]
    public string? Status { get; set; }

    [StringLength(32)]
    public string? Species { get; set; }
    
    [StringLength(32)]
    public string? Type { get; set; }
    
    [StringLength(32)]
    public string? Gender { get; set; }

    public Guid? OriginId { get; set; }
    public virtual Location? Origin { get; set; }

    public Guid? LocationId { get; set; }
    public virtual Location? Location { get; set; }

    [StringLength(128)]
    public string? Image { get; set; }
    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
    public int? ExternalId { get; set; }
}