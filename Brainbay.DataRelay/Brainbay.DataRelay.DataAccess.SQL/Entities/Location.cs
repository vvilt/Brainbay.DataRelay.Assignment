using Brainbay.DataRelay.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brainbay.DataRelay.DataAccess.SQL.Entities;

public class Location: IIdentifiable, ISyncable<int?>, IAuditable
{
    public Guid Id { get; set; }
    
    [StringLength(128)]
    public string Name { get; set; }
    
    [StringLength(128)]
    public string Type { get; set; }
    
    [StringLength(128)]
    public string Dimension { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
    public int? ExternalId { get; set; }

    [InverseProperty(nameof(Character.Origin))]
    public virtual ICollection<Character> OriginResidents { get; set; }
    
    [InverseProperty(nameof(Character.Location))]
    public virtual ICollection<Character> CurrentResidents { get; set; }
}