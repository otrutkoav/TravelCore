using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Tours
{
    public class TourHotelCommandValidator
    {
        public void ValidateAndThrow(ReplaceTourHotelsCommand command)
        {
            var errors = new List<string>();

            if (command == null)
                throw new ValidationException(new[] { "Request body is required." });

            var groups = (command.Groups ?? new List<TourHotelGroupInput>()).ToArray();
            ValidateOrderIndexes(groups.Select(x => x.OrderIndex), "Group", errors);

            foreach (var group in groups)
            {
                if (string.IsNullOrWhiteSpace(group.Name))
                    errors.Add("Hotel group name is required.");
                else if (group.Name.Trim().Length > 128)
                    errors.Add("Hotel group name must be 128 characters or less.");

                if (group.Priority <= 0)
                    errors.Add("Hotel group priority must be greater than zero.");

                var hotels = (group.Hotels ?? new List<TourHotelInput>()).ToArray();
                if (hotels.Length == 0)
                {
                    errors.Add("Every hotel group must contain at least one hotel template.");
                    continue;
                }

                ValidateOrderIndexes(hotels.Select(x => x.OrderIndex), "Hotel", errors);

                foreach (var hotel in hotels)
                {
                    if (hotel.CountryId <= 0)
                        errors.Add("CountryId must be greater than zero.");
                    var cityIds = (hotel.CityIds ?? new List<int>()).ToArray();
                    if (cityIds.Length == 0)
                        errors.Add("Every hotel template must contain at least one city.");
                    if (cityIds.Any(x => x <= 0))
                        errors.Add("CityIds must contain only positive identifiers.");
                    if (cityIds.Distinct().Count() != cityIds.Length)
                        errors.Add("CityIds must not contain duplicates.");

                    var durations = (hotel.DurationsInNights ?? new List<int>()).ToArray();
                    if (durations.Length == 0)
                        errors.Add("Every hotel template must contain at least one duration.");
                    if (durations.Any(x => x <= 0))
                        errors.Add("DurationsInNights must contain only positive values.");
                    if (durations.Distinct().Count() != durations.Length)
                        errors.Add("DurationsInNights must not contain duplicates.");

                    var hotelIds = (hotel.HotelIds ?? new List<int>()).ToArray();
                    if (hotelIds.Length == 0)
                        errors.Add("Every hotel template must contain at least one allowed hotel.");
                    if (hotelIds.Any(x => x <= 0))
                        errors.Add("HotelIds must contain only positive identifiers.");
                    if (hotelIds.Distinct().Count() != hotelIds.Length)
                        errors.Add("HotelIds must not contain duplicates.");
                }
            }

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        private static void ValidateOrderIndexes(
            IEnumerable<int> indexes,
            string entityName,
            ICollection<string> errors
        )
        {
            var values = indexes.OrderBy(x => x).ToArray();
            var expected = Enumerable.Range(1, values.Length).ToArray();

            if (!values.SequenceEqual(expected))
                errors.Add($"{entityName} OrderIndex values must form the sequence 1..N.");
        }
    }
}
