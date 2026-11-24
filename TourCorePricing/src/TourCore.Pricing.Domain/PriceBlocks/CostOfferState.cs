namespace TourCore.Pricing.Domain.PriceBlocks;

/// <summary>
/// Состояния ценового блока.
/// </summary>
public enum CostOfferState : byte
{
    /// <summary>Новый.</summary>
    New = 0,

    /// <summary>На проверке.</summary>
    InChecked = 1,

    /// <summary>Активированный для цен нетто.</summary>
    ActivatedForNetto = 2,

    /// <summary>Активированный.</summary>
    Activated = 3,

    /// <summary>Деактивированный.</summary>
    Deactivated = 4
}
