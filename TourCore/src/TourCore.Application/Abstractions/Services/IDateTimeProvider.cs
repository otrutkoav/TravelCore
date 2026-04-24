using System;

namespace TourCore.Application.Abstractions.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}