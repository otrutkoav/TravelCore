using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourCore.Pricing.Api.Infrastructure;
using TourCore.Pricing.Application.PriceBlocks;
using TourCore.Pricing.Contracts.PriceBlocks;

namespace TourCore.Pricing.Api.Controllers;

/// <summary>Управление ценовыми блоками.</summary>
[ApiController]
[Route("api/pricing/blocks")]
public sealed class PriceBlocksController : ControllerBase
{
    private readonly PriceBlockService _service;
    public PriceBlocksController(PriceBlockService service) { _service = service; }

    /// <summary>Получить ценовые блоки.</summary>
    [HttpGet]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<IReadOnlyList<PriceBlockDto>>> List(
        [FromQuery] int? pricePackageId,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.ListAsync(pricePackageId, cancellationToken));
    }

    /// <summary>Получить ценовой блок по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Read)]
    public async Task<ActionResult<PriceBlockDto>> Get(int id, CancellationToken cancellationToken)
    {
        return Ok(await _service.GetAsync(id, cancellationToken));
    }

    /// <summary>Создать ценовой блок.</summary>
    [HttpPost]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<PriceBlockDto>> Create(
        [FromBody] CreatePriceBlockRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>Изменить ценовой блок.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<PriceBlockDto>> Update(
        int id,
        [FromBody] UpdatePriceBlockRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.UpdateAsync(id, request, cancellationToken));
    }

    /// <summary>Изменить состояние ценового блока.</summary>
    [HttpPut("{id:int}/state")]
    [Authorize(Policy = PricingAuthorizationPolicies.Write)]
    public async Task<ActionResult<PriceBlockDto>> ChangeState(
        int id,
        [FromBody] ChangePriceBlockStateRequest request,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.ChangeStateAsync(id, request, cancellationToken));
    }

    /// <summary>Удалить ценовой блок.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = PricingAuthorizationPolicies.Delete)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
