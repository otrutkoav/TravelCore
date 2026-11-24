namespace TourCore.Pricing.Contracts.Common;

/// <summary>Стандартная ошибка Pricing API.</summary>
public sealed class PricingApiErrorDto
{
    /// <summary>Стабильный машинный код ошибки, например hotel_not_found.</summary>
    public string Code { get; init; } = null!;

    /// <summary>Понятное пользователю описание ошибки.</summary>
    public string Message { get; init; } = null!;

    /// <summary>Ошибки отдельных полей для validation_error; для остальных ошибок null.</summary>
    public IReadOnlyDictionary<string, string[]>? Details { get; init; }
}
