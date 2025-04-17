namespace Brainbay.DataRelay.Sync;

public interface ISynchronizer
{
    Task SyncAsync(CancellationToken cancellationToken = default);
}