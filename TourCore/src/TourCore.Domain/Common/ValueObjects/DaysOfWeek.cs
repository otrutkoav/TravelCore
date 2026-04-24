using System;
using System.Collections.Generic;
using System.Linq;
using TourCore.Domain.Common.Enums;
using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Common.ValueObjects
{
    /// <summary>
    /// Набор дней недели, в которые действует расписание.
    /// Legacy-формат: строка вида "1234567",
    /// где 1 = понедельник, 7 = воскресенье.
    /// </summary>
    public sealed class DaysOfWeek : ValueObject
    {
        public DayOfWeekFlags Value { get; private set; }

        public DaysOfWeek(DayOfWeekFlags value)
        {
            if (value == DayOfWeekFlags.None)
                throw new DomainException("At least one day of week must be specified.");

            Value = value;
        }

        public bool Contains(DayOfWeek dayOfWeek)
        {
            return (Value & ToFlag(dayOfWeek)) == ToFlag(dayOfWeek);
        }

        public static DaysOfWeek FromLegacy(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            var result = DayOfWeekFlags.None;

            foreach (var ch in value.Trim())
            {
                result |= ParseLegacyDay(ch);
            }

            return new DaysOfWeek(result);
        }

        public static DaysOfWeek FromDays(IEnumerable<DayOfWeek> days)
        {
            if (days == null)
                throw new DomainException("Days of week collection is required.");

            var result = DayOfWeekFlags.None;

            foreach (var day in days.Distinct())
            {
                result |= ToFlag(day);
            }

            return new DaysOfWeek(result);
        }

        public IReadOnlyCollection<DayOfWeek> ToDays()
        {
            return new[]
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday
            }
            .Where(Contains)
            .ToList()
            .AsReadOnly();
        }

        public string ToLegacy()
        {
            return string.Concat(
                ToDays().Select(ToLegacyDay));
        }

        private static DayOfWeekFlags ParseLegacyDay(char ch)
        {
            switch (ch)
            {
                case '1':
                    return DayOfWeekFlags.Monday;
                case '2':
                    return DayOfWeekFlags.Tuesday;
                case '3':
                    return DayOfWeekFlags.Wednesday;
                case '4':
                    return DayOfWeekFlags.Thursday;
                case '5':
                    return DayOfWeekFlags.Friday;
                case '6':
                    return DayOfWeekFlags.Saturday;
                case '7':
                    return DayOfWeekFlags.Sunday;
                default:
                    throw new DomainException("Invalid legacy day of week value: " + ch);
            }
        }

        private static DayOfWeekFlags ToFlag(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return DayOfWeekFlags.Monday;
                case DayOfWeek.Tuesday:
                    return DayOfWeekFlags.Tuesday;
                case DayOfWeek.Wednesday:
                    return DayOfWeekFlags.Wednesday;
                case DayOfWeek.Thursday:
                    return DayOfWeekFlags.Thursday;
                case DayOfWeek.Friday:
                    return DayOfWeekFlags.Friday;
                case DayOfWeek.Saturday:
                    return DayOfWeekFlags.Saturday;
                case DayOfWeek.Sunday:
                    return DayOfWeekFlags.Sunday;
                default:
                    throw new DomainException("Invalid day of week value: " + dayOfWeek);
            }
        }

        private static char ToLegacyDay(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return '1';
                case DayOfWeek.Tuesday:
                    return '2';
                case DayOfWeek.Wednesday:
                    return '3';
                case DayOfWeek.Thursday:
                    return '4';
                case DayOfWeek.Friday:
                    return '5';
                case DayOfWeek.Saturday:
                    return '6';
                case DayOfWeek.Sunday:
                    return '7';
                default:
                    throw new DomainException("Invalid day of week value: " + dayOfWeek);
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}