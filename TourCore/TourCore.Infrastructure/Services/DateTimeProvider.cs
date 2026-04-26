using System;
using TourCore.Application.Abstractions.Services;

namespace TourCore.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}