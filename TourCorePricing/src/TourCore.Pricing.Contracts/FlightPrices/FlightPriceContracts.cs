namespace TourCore.Pricing.Contracts.FlightPrices;

/// <summary>Изменяемые данные цены пассажира на отдельный сегмент авиаперелёта.</summary>
public abstract class FlightPriceWriteRequest
{
    /// <summary>Идентификатор ценового блока, которому принадлежит цена.</summary>
    public int PriceBlockId { get; init; }
    /// <summary>Идентификатор сегмента перелёта (Charter) из Catalog.</summary>
    public int CharterId { get; init; }
    /// <summary>Идентификатор класса обслуживания из Catalog.</summary>
    public int AirClassId { get; init; }
    /// <summary>Первая дата вылета, для которой действует цена.</summary>
    public DateOnly TravelDateFrom { get; init; }
    /// <summary>Последняя дата вылета, для которой действует цена.</summary>
    public DateOnly TravelDateTo { get; init; }
    /// <summary>Битовая маска дней недели: понедельник = 1, вторник = 2, ... воскресенье = 64; 127 — все дни.</summary>
    public byte DaysOfWeekMask { get; init; }
    /// <summary>Начало периода продаж. Если не задано, ограничение снизу отсутствует.</summary>
    public DateOnly? SaleDateFrom { get; init; }
    /// <summary>Окончание периода продаж. Если не задано, ограничение сверху отсутствует.</summary>
    public DateOnly? SaleDateTo { get; init; }
    /// <summary>Минимальный возраст пассажира включительно. Null означает отсутствие нижней границы.</summary>
    public short? AgeFrom { get; init; }
    /// <summary>Максимальный возраст пассажира включительно. Null означает отсутствие верхней границы.</summary>
    public short? AgeTo { get; init; }
    /// <summary>Нетто-сумма — закупочная стоимость для одного пассажира.</summary>
    public decimal? NettoAmount { get; init; }
    /// <summary>Брутто-сумма — стоимость продажи для одного пассажира.</summary>
    public decimal? BruttoAmount { get; init; }
    /// <summary>Валюта цены из Catalog. Обязательна, если отличается от валюты ценового блока.</summary>
    public int? CurrencyId { get; init; }
    /// <summary>Приоритет правила при пересечении цен: большее значение побеждает.</summary>
    public int Priority { get; init; }
}

/// <summary>Запрос создания цены авиаперелёта.</summary>
public sealed class CreateFlightPriceRequest : FlightPriceWriteRequest;
/// <summary>Запрос изменения цены авиаперелёта.</summary>
public sealed class UpdateFlightPriceRequest : FlightPriceWriteRequest;

/// <summary>Цена пассажира на отдельный сегмент авиаперелёта.</summary>
public sealed class FlightPriceDto : FlightPriceWriteRequest
{
    /// <summary>Идентификатор цены.</summary>
    public int Id { get; init; }
    /// <summary>Идентификатор пользователя, создавшего цену.</summary>
    public string CreatedByUserId { get; init; } = null!;
    /// <summary>Дата и время создания в UTC.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Дата и время последнего изменения в UTC.</summary>
    public DateTime? UpdatedAt { get; init; }
}
