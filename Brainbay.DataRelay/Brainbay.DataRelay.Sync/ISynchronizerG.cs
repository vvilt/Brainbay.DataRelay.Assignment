namespace Brainbay.DataRelay.Sync;

public interface ISynchronizer<TDomain, TApi>
{
    Task SyncAsync(Action<TDomain, TApi> beforeSave = null
        , Action<TDomain, TApi> afterSave = null
        , Func<TApi, bool> filterApiResourcePredicate = null
        , CancellationToken cancellationToken = default);
}