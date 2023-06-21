namespace VirtualIpObserverDevice.Core.Contracts;

public interface IObserverIpService
{
    Task<string> GetExternalIpServerAddress();

    List<string> GetInternalIpServerAdaptersAddresses();
}