using TourCore.Pricing.Contracts.TransportPrices;

namespace TourCore.Pricing.Contracts.BusPrices;

/// <summary>Изменяемые данные цены автобусного переезда.</summary>
public abstract class BusTransferPriceWriteRequest : TransportPriceWriteRequest
{
    /// <summary>Идентификатор пункта посадки автобусного переезда из Catalog.</summary>
    public int BusTransferPointId { get; init; }
    /// <summary>Минимальное количество пассажиров включительно. Null означает отсутствие границы.</summary>
    public short? PassengerCountFrom { get; init; }
    /// <summary>Максимальное количество пассажиров включительно. Null означает отсутствие границы.</summary>
    public short? PassengerCountTo { get; init; }
}

/// <summary>Запрос создания цены автобусного переезда.</summary>
public sealed class CreateBusTransferPriceRequest : BusTransferPriceWriteRequest;
/// <summary>Запрос изменения цены автобусного переезда.</summary>
public sealed class UpdateBusTransferPriceRequest : BusTransferPriceWriteRequest;

/// <summary>Цена автобусного переезда.</summary>
public sealed class BusTransferPriceDto : TransportPriceDto
{
    /// <summary>Идентификатор пункта посадки автобусного переезда из Catalog.</summary>
    public int BusTransferPointId { get; init; }
    /// <summary>Минимальное количество пассажиров включительно.</summary>
    public short? PassengerCountFrom { get; init; }
    /// <summary>Максимальное количество пассажиров включительно.</summary>
    public short? PassengerCountTo { get; init; }
}
