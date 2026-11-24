namespace TourCore.Pricing.Contracts.SPOTypes;

/// <summary>Тип SPO будущего ценового блока.</summary>
public sealed class SPOTypeDto
{
    /// <summary>Стабильный идентификатор типа.</summary>
    public int Id { get; init; }

    /// <summary>Название.</summary>
    public string Name { get; init; } = null!;

    /// <summary>Латинское или альтернативное название.</summary>
    public string NameLat { get; init; } = null!;

    /// <summary>Идентификатор создавшего пользователя.</summary>
    public string CreatedByUserId { get; init; } = null!;

    /// <summary>Дата создания в UTC.</summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>Дата последнего изменения в UTC.</summary>
    public DateTime? UpdatedAt { get; init; }
}
