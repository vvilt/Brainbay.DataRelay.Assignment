namespace Brainbay.DataRelay.Domain.Models;

public interface ISyncable<T>
{
    public T ExternalId { get; set; }
}