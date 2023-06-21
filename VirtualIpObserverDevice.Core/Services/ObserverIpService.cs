using System.Net;
using System.Text.Json;
using VirtualIpObserverDevice.Core.Contracts;

namespace VirtualIpObserverDevice.Core.Services;

public class ObserverIpService : IObserverIpService
{
    private readonly HttpClient _httpClient;

    public ObserverIpService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        
    }

    public async Task<string> GetExternalIpServerAddress()
    {
        var httpResponseMessage = await _httpClient.GetAsync("http://ifconfig.me/ip");
        string ipAsString;
        
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            using (var reader = new StreamReader(httpResponseMessage.Content.ReadAsStream()))
            {
                ipAsString = reader.ReadToEnd();
            }
            
            return ipAsString;
        }

        throw new Exception($"Current IP identifier-service respond with status code: {httpResponseMessage.StatusCode}");
    }

    public List<string> GetInternalIpServerAdaptersAddresses()
    {
        String strHostName = Dns.GetHostName();

        // Find host by name    IPHostEntry
        var iphostentry = Dns.GetHostByName(strHostName);
        var ipAddresses = new List<string>();

        // Enumerate IP addresses
        int nIP = 0;   
        foreach(IPAddress ipaddress in iphostentry.AddressList) 
        {
            ipAddresses.Add("IP #" + ++nIP + ": " + ipaddress.ToString());    
        }

        return ipAddresses;
    }
}