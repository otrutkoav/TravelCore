using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourCore.Pricing.Api.Infrastructure;
using TourCore.Pricing.Application.FlightPrices;
using TourCore.Pricing.Contracts.FlightPrices;
using TourCore.Pricing.Contracts.Common;

namespace TourCore.Pricing.Api.Controllers;

/// <summary>Цены пассажиров на отдельные сегменты авиаперелёта.</summary>
[ApiController]
[Route("api/pricing/flight-prices")]
public sealed class FlightPricesController : ControllerBase
{
    private readonly FlightPriceService _service;
    public FlightPricesController(FlightPriceService service) { _service = service; }

    /// <summary>Получить цены сегментов авиаперелётов.</summary>
    /// <param name="priceBlockId">Фильтр по ценовому блоку.</param>
    /// <param name="charterId">Фильтр по сегменту перелёта.</param>
    /// <param name="airClassId">Фильтр по классу обслуживания.</param>
    /// <param name="ct">Токен отмены запроса.</param>
    [HttpGet]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<IReadOnlyList<FlightPriceDto>>> List(
        [FromQuery] int? priceBlockId, [FromQuery] int? charterId,
        [FromQuery] int? airClassId, CancellationToken ct) =>
        Ok(await _service.ListAsync(priceBlockId, charterId, airClassId, ct));

    /// <summary>Получить цену сегмента авиаперелёта по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<FlightPriceDto>> Get(int id, CancellationToken ct) =>
        Ok(await _service.GetAsync(id, ct));

    /// <summary>Создать цену пассажира на сегмент авиаперелёта.</summary>
    [HttpPost]
    [ProducesResponseType<PricingApiErrorDto>(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<FlightPriceDto>> Create(CreateFlightPriceRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>Изменить цену пассажира на сегмент авиаперелёта.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType<PricingApiErrorDto>(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<FlightPriceDto>> Update(int id, UpdateFlightPriceRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateAsync(id, request, ct));

    /// <summary>Удалить цену сегмента авиаперелёта.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}
