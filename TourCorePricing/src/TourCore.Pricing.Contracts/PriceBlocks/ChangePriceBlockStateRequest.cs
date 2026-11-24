namespace TourCore.Pricing.Contracts.PriceBlocks;

/// <summary>Запрос изменения состояния ценового блока.</summary>
public sealed class ChangePriceBlockStateRequest
{
    public byte State { get; init; }
}
