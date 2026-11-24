using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourCore.Pricing.Api.Infrastructure;
using TourCore.Pricing.Application.PricePackages;
using TourCore.Pricing.Contracts.PricePackages;

namespace TourCore.Pricing.Api.Controllers;

/// <summary>Управление ценовыми пакетами.</summary>
[ApiController]
[Route("api/pricing/packages")]
public sealed class PricePackagesController : ControllerBase
{
    private readonly PricePackageService _service;

    public PricePackagesController(PricePackageService service)
    {
        _service = service;
    }

    /// <summary>Получить действующие ценовые пакеты.</summary>
    [HttpGet]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    [ProducesResponseType<IReadOnlyList<PricePackageDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PricePackageDto>>> List(
        [FromQuery] int? countryId,
        [FromQuery] bool? isDisabled,
        [FromQuery] string? search,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.ListAsync(countryId, isDisabled, search, cancellationToken));
    }

    /// <summary>Получить ценовой пакет по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    [ProducesResponseType<PricePackageDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PricePackageDto>> Get(int id, CancellationToken cancellationToken)
    {
        return Ok(await _service.GetAsync(id, cancellationToken));
    }

    /// <summary>Создать ценовой пакет.</summary>
    [HttpPost]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    [ProducesResponseType<PricePackageDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<PricePackageDto>> Create(
        [FromBody] CreatePricePackageRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>Изменить ценовой пакет.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    [ProducesResponseType<PricePackageDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<PricePackageDto>> Update(
        int id,
        [FromBody] UpdatePricePackageRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.UpdateAsync(id, request, cancellationToken));
    }

    /// <summary>Мягко удалить ценовой пакет.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
