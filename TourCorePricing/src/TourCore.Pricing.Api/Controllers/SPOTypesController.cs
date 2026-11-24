using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourCore.Pricing.Api.Infrastructure;
using TourCore.Pricing.Application.SPOTypes;
using TourCore.Pricing.Contracts.SPOTypes;

namespace TourCore.Pricing.Api.Controllers;

/// <summary>Управление типами SPO будущих ценовых блоков.</summary>
[ApiController]
[Route("api/pricing/spo-types")]
public sealed class SPOTypesController : ControllerBase
{
    private readonly SPOTypeService _service;

    public SPOTypesController(SPOTypeService service)
    {
        _service = service;
    }

    /// <summary>Получить все типы SPO.</summary>
    [HttpGet]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    [ProducesResponseType<IReadOnlyList<SPOTypeDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SPOTypeDto>>> List(CancellationToken cancellationToken)
    {
        return Ok(await _service.ListAsync(cancellationToken));
    }

    /// <summary>Получить тип SPO по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    [ProducesResponseType<SPOTypeDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SPOTypeDto>> Get(int id, CancellationToken cancellationToken)
    {
        return Ok(await _service.GetAsync(id, cancellationToken));
    }

    /// <summary>Создать тип SPO.</summary>
    [HttpPost]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    [ProducesResponseType<SPOTypeDto>(StatusCodes.Status201Created)]
    public async Task<ActionResult<SPOTypeDto>> Create(
        [FromBody] CreateSPOTypeRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>Изменить тип SPO.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    [ProducesResponseType<SPOTypeDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<SPOTypeDto>> Update(
        int id,
        [FromBody] UpdateSPOTypeRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.UpdateAsync(id, request, cancellationToken));
    }

    /// <summary>Удалить тип SPO.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
