namespace TourCore.Pricing.Contracts.Seasons;

/// <summary>Сезон ценового блока.</summary>
public sealed class SeasonDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string NameLat { get; init; } = null!;
    public bool IsActive { get; init; }
    public bool IsMain { get; init; }
    public string CreatedByUserId { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
