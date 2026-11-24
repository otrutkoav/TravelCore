using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourCore.Pricing.Api.Infrastructure;
using TourCore.Pricing.Application.Seasons;
using TourCore.Pricing.Contracts.Seasons;

namespace TourCore.Pricing.Api.Controllers;

/// <summary>Управление сезонами ценовых блоков.</summary>
[ApiController]
[Route("api/pricing/seasons")]
public sealed class SeasonsController : ControllerBase
{
    private readonly SeasonService _service;

    public SeasonsController(SeasonService service)
    {
        _service = service;
    }

    /// <summary>Получить сезоны.</summary>
    [HttpGet]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    [ProducesResponseType<IReadOnlyList<SeasonDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SeasonDto>>> List(
        [FromQuery] bool? isActive,
        CancellationToken cancellationToken) =>
        Ok(await _service.ListAsync(isActive, cancellationToken));

    /// <summary>Получить сезон по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    [ProducesResponseType<SeasonDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SeasonDto>> Get(int id, CancellationToken cancellationToken) =>
        Ok(await _service.GetAsync(id, cancellationToken));

    /// <summary>Создать сезон.</summary>
    [HttpPost]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    [ProducesResponseType<SeasonDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<SeasonDto>> Create(
        [FromBody] CreateSeasonRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>Изменить сезон.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    [ProducesResponseType<SeasonDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<SeasonDto>> Update(
        int id,
        [FromBody] UpdateSeasonRequest request,
        CancellationToken cancellationToken) =>
        Ok(await _service.UpdateAsync(id, request, cancellationToken));

    /// <summary>Удалить сезон.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
