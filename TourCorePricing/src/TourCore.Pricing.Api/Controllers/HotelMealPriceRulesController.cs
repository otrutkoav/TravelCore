using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourCore.Pricing.Api.Infrastructure;
using TourCore.Pricing.Application.HotelPrices;
using TourCore.Pricing.Contracts.HotelPrices;
using TourCore.Pricing.Contracts.Common;

namespace TourCore.Pricing.Api.Controllers;

/// <summary>Правила стоимости питания и апгрейдов питания в отелях.</summary>
[ApiController]
[Route("api/pricing/hotel-meal-rules")]
public sealed class HotelMealPriceRulesController : ControllerBase
{
    private readonly HotelPriceService _service;
    public HotelMealPriceRulesController(HotelPriceService service) { _service = service; }

    /// <summary>Получить правила стоимости питания и апгрейдов.</summary>
    /// <param name="priceBlockId">Фильтр по ценовому блоку.</param>
    /// <param name="hotelId">Фильтр по отелю.</param>
    /// <param name="ct">Токен отмены запроса.</param>
    [HttpGet]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<IReadOnlyList<HotelMealPriceRuleDto>>> List(
        [FromQuery] int? priceBlockId, [FromQuery] int? hotelId, CancellationToken ct) =>
        Ok(await _service.ListMealRulesAsync(priceBlockId, hotelId, ct));

    /// <summary>Получить правило стоимости питания по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<HotelMealPriceRuleDto>> Get(int id, CancellationToken ct) =>
        Ok(await _service.GetMealRuleAsync(id, ct));

    /// <summary>Создать правило стоимости питания или апгрейда.</summary>
    [HttpPost]
    [ProducesResponseType<PricingApiErrorDto>(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<HotelMealPriceRuleDto>> Create(CreateHotelMealPriceRuleRequest request, CancellationToken ct)
    {
        var result = await _service.CreateMealRuleAsync(request, ct);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>Изменить правило стоимости питания или апгрейда.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType<PricingApiErrorDto>(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<HotelMealPriceRuleDto>> Update(
        int id, UpdateHotelMealPriceRuleRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateMealRuleAsync(id, request, ct));

    /// <summary>Удалить правило стоимости питания или апгрейда.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _service.DeleteMealRuleAsync(id, ct);
        return NoContent();
    }
}
