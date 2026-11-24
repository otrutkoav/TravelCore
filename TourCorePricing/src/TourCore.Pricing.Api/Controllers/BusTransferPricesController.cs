using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourCore.Pricing.Api.Infrastructure;
using TourCore.Pricing.Application.BusPrices;
using TourCore.Pricing.Contracts.BusPrices;
using TourCore.Pricing.Contracts.Common;

namespace TourCore.Pricing.Api.Controllers;

/// <summary>Цены автобусных переездов.</summary>
[ApiController]
[Route("api/pricing/bus-transfer-prices")]
public sealed class BusTransferPricesController : ControllerBase
{
    private readonly BusTransferPriceService _service;
    public BusTransferPricesController(BusTransferPriceService service) { _service = service; }
    /// <summary>Получить цены автобусных переездов с необязательной фильтрацией.</summary>
    [HttpGet, Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<IReadOnlyList<BusTransferPriceDto>>> List([FromQuery] int? priceBlockId, [FromQuery] int? busTransferPointId, [FromQuery] int? transportId, CancellationToken ct) => Ok(await _service.ListAsync(priceBlockId, busTransferPointId, transportId, ct));
    /// <summary>Получить цену автобусного переезда по идентификатору.</summary>
    [HttpGet("{id:int}"), Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<BusTransferPriceDto>> Get(int id, CancellationToken ct) => Ok(await _service.GetAsync(id, ct));
    /// <summary>Создать цену автобусного переезда.</summary>
    [HttpPost, Authorize(Policy = PricingAuthorizationPolicies.Write)]
    [ProducesResponseType<PricingApiErrorDto>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BusTransferPriceDto>> Create(CreateBusTransferPriceRequest request, CancellationToken ct)
    { var result = await _service.CreateAsync(request, ct); return CreatedAtAction(nameof(Get), new { id = result.Id }, result); }
    /// <summary>Изменить цену автобусного переезда.</summary>
    [HttpPut("{id:int}"), Authorize(Policy = PricingAuthorizationPolicies.Write)]
    [ProducesResponseType<PricingApiErrorDto>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BusTransferPriceDto>> Update(int id, UpdateBusTransferPriceRequest request, CancellationToken ct) => Ok(await _service.UpdateAsync(id, request, ct));
    /// <summary>Удалить цену автобусного переезда.</summary>
    [HttpDelete("{id:int}"), Authorize(Policy = PricingAuthorizationPolicies.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct) { await _service.DeleteAsync(id, ct); return NoContent(); }
}
