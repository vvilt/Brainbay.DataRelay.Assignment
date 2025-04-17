namespace Brainbay.DataRelay.DataAccess.SQL.Mapping;

public interface IMapper<T1, T2>
{
    public T2 Map(T1 source);
    public T1 Map(T2 source);
}