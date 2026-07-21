namespace VpnHood.Tools.NetTester.Testers;

public interface IStreamTesterClient
{
    public Task Start(long upSize, long downSize, int connectionCount, CancellationToken cancellationToken);
}