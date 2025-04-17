namespace Brainbay.DataRelay.Domain.Models;

public interface IAuditable
{
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }
}