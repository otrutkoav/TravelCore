using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourCore.Pricing.Api.Infrastructure;
using TourCore.Pricing.Application.RailwayPrices;
using TourCore.Pricing.Contracts.RailwayPrices;
using TourCore.Pricing.Contracts.Common;

namespace TourCore.Pricing.Api.Controllers;

/// <summary>Цены железнодорожных переездов.</summary>
[ApiController]
[Route("api/pricing/railway-transfer-prices")]
public sealed class RailwayTransferPricesController : ControllerBase
{
    private readonly RailwayTransferPriceService _service;
    public RailwayTransferPricesController(RailwayTransferPriceService service) { _service = service; }
    /// <summary>Получить цены железнодорожных переездов с необязательной фильтрацией.</summary>
    [HttpGet, Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<IReadOnlyList<RailwayTransferPriceDto>>> List([FromQuery] int? priceBlockId, [FromQuery] int? railwayTransferId, [FromQuery] int? transportId, CancellationToken ct) => Ok(await _service.ListAsync(priceBlockId, railwayTransferId, transportId, ct));
    /// <summary>Получить цену железнодорожного переезда по идентификатору.</summary>
    [HttpGet("{id:int}"), Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<RailwayTransferPriceDto>> Get(int id, CancellationToken ct) => Ok(await _service.GetAsync(id, ct));
    /// <summary>Создать цену железнодорожного переезда.</summary>
    [HttpPost, Authorize(Policy = PricingAuthorizationPolicies.Write)]
    [ProducesResponseType<PricingApiErrorDto>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RailwayTransferPriceDto>> Create(CreateRailwayTransferPriceRequest request, CancellationToken ct)
    { var result = await _service.CreateAsync(request, ct); return CreatedAtAction(nameof(Get), new { id = result.Id }, result); }
    /// <summary>Изменить цену железнодорожного переезда.</summary>
    [HttpPut("{id:int}"), Authorize(Policy = PricingAuthorizationPolicies.Write)]
    [ProducesResponseType<PricingApiErrorDto>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RailwayTransferPriceDto>> Update(int id, UpdateRailwayTransferPriceRequest request, CancellationToken ct) => Ok(await _service.UpdateAsync(id, request, ct));
    /// <summary>Удалить цену железнодорожного переезда.</summary>
    [HttpDelete("{id:int}"), Authorize(Policy = PricingAuthorizationPolicies.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct) { await _service.DeleteAsync(id, ct); return NoContent(); }
}
