namespace TourCore.Pricing.Contracts.PriceBlocks;

/// <summary>Запрос изменения состояния ценового блока.</summary>
public sealed class ChangePriceBlockStateRequest
{
    /// <summary>Новое состояние: 0 — новый, 1 — на проверке, 2 — активирован для нетто, 3 — активирован, 4 — деактивирован.</summary>
    public byte State { get; init; }
}
