using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualIpObserverDevice.Core.Contracts;

namespace VirtualIpObserverDevice.Controllers;

[ApiController]
[Route("observe-ips")]
public class IpObserverController : ControllerBase
{
    private readonly IObserverIpService _observerIpService;

    public IpObserverController(IObserverIpService observerIpService)
    {
        _observerIpService = observerIpService;
    }

    /// <summary>
    /// Метод позволяет получить публичный сетевой адрес сервера
    /// </summary>
    /// <returns></returns>
    [HttpGet("external")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetExternalServerIp()
    {
        var publicIpAddress = await _observerIpService.GetExternalIpServerAddress();

        return Ok(publicIpAddress);
    }

    /// <summary>
    /// Метод позволяет получить сетевые адреса адаптеров, подключенных к текущему серверу
    /// </summary>
    /// <returns></returns>
    [HttpGet("internal-adapters")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetInternalIpServerAdaptersAddresses()
    {
        var ipAddresses = _observerIpService.GetInternalIpServerAdaptersAddresses();

        return Ok(ipAddresses);
    }
}