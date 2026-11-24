namespace TourCore.Pricing.Contracts.TransportPrices;

/// <summary>Общие изменяемые данные цены наземного переезда.</summary>
public abstract class TransportPriceWriteRequest
{
    /// <summary>Идентификатор ценового блока, которому принадлежит цена.</summary>
    public int PriceBlockId { get; init; }
    /// <summary>Идентификатор вида транспорта из Catalog.</summary>
    public int TransportId { get; init; }
    /// <summary>Начало периода поездки. Null означает отсутствие нижней границы.</summary>
    public DateOnly? TravelDateFrom { get; init; }
    /// <summary>Окончание периода поездки. Null означает отсутствие верхней границы.</summary>
    public DateOnly? TravelDateTo { get; init; }
    /// <summary>Битовая маска дней недели: понедельник = 1, ... воскресенье = 64; 127 — все дни.</summary>
    public byte DaysOfWeekMask { get; init; }
    /// <summary>Начало периода продаж. Null означает отсутствие ограничения.</summary>
    public DateOnly? SaleDateFrom { get; init; }
    /// <summary>Окончание периода продаж. Null означает отсутствие ограничения.</summary>
    public DateOnly? SaleDateTo { get; init; }
    /// <summary>Минимальный возраст пассажира включительно.</summary>
    public short? AgeFrom { get; init; }
    /// <summary>Максимальный возраст пассажира включительно.</summary>
    public short? AgeTo { get; init; }
    /// <summary>Получатель суммы: 0 — один пассажир, 1 — вся транспортная услуга.</summary>
    public byte Scope { get; init; }
    /// <summary>Применение по времени: 0 — однократно за переезд, 1 — за каждый день.</summary>
    public byte PeriodUnit { get; init; }
    /// <summary>Нетто-сумма — закупочная стоимость.</summary>
    public decimal? NettoAmount { get; init; }
    /// <summary>Брутто-сумма — стоимость продажи.</summary>
    public decimal? BruttoAmount { get; init; }
    /// <summary>Валюта цены из Catalog. Null означает валюту ценового блока.</summary>
    public int? CurrencyId { get; init; }
    /// <summary>Приоритет при пересечении правил: большее значение побеждает.</summary>
    public int Priority { get; init; }
}

/// <summary>Общие возвращаемые данные цены наземного переезда.</summary>
public abstract class TransportPriceDto : TransportPriceWriteRequest
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
