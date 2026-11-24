namespace TourCore.Pricing.Contracts.Seasons;

/// <summary>Сезон ценового блока.</summary>
public sealed class SeasonDto
{
    /// <summary>Идентификатор сезона.</summary>
    public int Id { get; init; }
    /// <summary>Название сезона на русском языке.</summary>
    public string Name { get; init; } = null!;
    /// <summary>Латинское или международное название.</summary>
    public string NameLat { get; init; } = null!;
    /// <summary>Доступен ли сезон для использования.</summary>
    public bool IsActive { get; init; }
    /// <summary>Является ли сезон основным.</summary>
    public bool IsMain { get; init; }
    /// <summary>Идентификатор пользователя, создавшего сезон.</summary>
    public string CreatedByUserId { get; init; } = null!;
    /// <summary>Дата и время создания в UTC.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Дата и время последнего изменения в UTC.</summary>
    public DateTime? UpdatedAt { get; init; }
}
