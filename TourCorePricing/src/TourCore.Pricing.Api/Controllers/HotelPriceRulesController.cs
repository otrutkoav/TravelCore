using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourCore.Pricing.Api.Infrastructure;
using TourCore.Pricing.Application.HotelPrices;
using TourCore.Pricing.Contracts.HotelPrices;
using TourCore.Pricing.Contracts.Common;

namespace TourCore.Pricing.Api.Controllers;

/// <summary>Правила стоимости проживания и их расчётные компоненты.</summary>
[ApiController]
[Route("api/pricing/hotel-rules")]
public sealed class HotelPriceRulesController : ControllerBase
{
    private readonly HotelPriceService _service;
    public HotelPriceRulesController(HotelPriceService service) { _service = service; }

    /// <summary>Получить правила стоимости проживания.</summary>
    /// <param name="priceBlockId">Фильтр по ценовому блоку.</param>
    /// <param name="hotelId">Фильтр по отелю.</param>
    /// <param name="ct">Токен отмены запроса.</param>
    [HttpGet]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<IReadOnlyList<HotelPriceRuleDto>>> List(
        [FromQuery] int? priceBlockId, [FromQuery] int? hotelId, CancellationToken ct) =>
        Ok(await _service.ListRulesAsync(priceBlockId, hotelId, ct));

    /// <summary>Получить правило стоимости проживания по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<HotelPriceRuleDto>> Get(int id, CancellationToken ct) =>
        Ok(await _service.GetRuleAsync(id, ct));

    /// <summary>Создать правило стоимости проживания.</summary>
    [HttpPost]
    [ProducesResponseType<PricingApiErrorDto>(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<HotelPriceRuleDto>> Create(CreateHotelPriceRuleRequest request, CancellationToken ct)
    {
        var result = await _service.CreateRuleAsync(request, ct);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>Изменить правило стоимости проживания.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType<PricingApiErrorDto>(StatusCodes.Status404NotFound)]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<HotelPriceRuleDto>> Update(int id, UpdateHotelPriceRuleRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateRuleAsync(id, request, ct));

    /// <summary>Удалить правило стоимости проживания вместе с компонентами.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _service.DeleteRuleAsync(id, ct);
        return NoContent();
    }

    /// <summary>Получить компоненты формулы гостиничной цены.</summary>
    [HttpGet("{ruleId:int}/components")]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<IReadOnlyList<HotelPriceComponentDto>>> ListComponents(int ruleId, CancellationToken ct) =>
        Ok(await _service.ListComponentsAsync(ruleId, ct));

    /// <summary>Добавить компонент в формулу гостиничной цены.</summary>
    [HttpPost("{ruleId:int}/components")]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<HotelPriceComponentDto>> CreateComponent(
        int ruleId, CreateHotelPriceComponentRequest request, CancellationToken ct)
    {
        var result = await _service.CreateComponentAsync(ruleId, request, ct);
        return CreatedAtAction(nameof(GetComponent), new { id = result.Id }, result);
    }

    /// <summary>Получить компонент формулы по идентификатору.</summary>
    [HttpGet("components/{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<HotelPriceComponentDto>> GetComponent(int id, CancellationToken ct) =>
        Ok(await _service.GetComponentAsync(id, ct));

    /// <summary>Изменить компонент формулы гостиничной цены.</summary>
    [HttpPut("components/{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<HotelPriceComponentDto>> UpdateComponent(
        int id, UpdateHotelPriceComponentRequest request, CancellationToken ct) =>
        Ok(await _service.UpdateComponentAsync(id, request, ct));

    /// <summary>Удалить компонент формулы гостиничной цены.</summary>
    [HttpDelete("components/{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Delete)]
    public async Task<IActionResult> DeleteComponent(int id, CancellationToken ct)
    {
        await _service.DeleteComponentAsync(id, ct);
        return NoContent();
    }
}
