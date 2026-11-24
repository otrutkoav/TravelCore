using TourCore.Pricing.Contracts.TransportPrices;

namespace TourCore.Pricing.Contracts.RailwayPrices;

/// <summary>Изменяемые данные цены железнодорожного переезда.</summary>
public abstract class RailwayTransferPriceWriteRequest : TransportPriceWriteRequest
{
    /// <summary>Идентификатор железнодорожного переезда из Catalog.</summary>
    public int RailwayTransferId { get; init; }
}

/// <summary>Запрос создания цены железнодорожного переезда.</summary>
public sealed class CreateRailwayTransferPriceRequest : RailwayTransferPriceWriteRequest;
/// <summary>Запрос изменения цены железнодорожного переезда.</summary>
public sealed class UpdateRailwayTransferPriceRequest : RailwayTransferPriceWriteRequest;

/// <summary>Цена железнодорожного переезда.</summary>
public sealed class RailwayTransferPriceDto : TransportPriceDto
{
    /// <summary>Идентификатор железнодорожного переезда из Catalog.</summary>
    public int RailwayTransferId { get; init; }
}
