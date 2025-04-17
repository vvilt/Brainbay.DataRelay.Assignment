namespace Brainbay.DataRelay.Application.DTOs;

public class CachedResult<T>
{
    public bool FromDatabase { get; set; }
    public T Data { get; set; }
}