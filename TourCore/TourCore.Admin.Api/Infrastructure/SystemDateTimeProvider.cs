using TourCore.Application.Abstractions.Services;

namespace TourCore.Admin.Api.Infrastructure;

internal sealed class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
