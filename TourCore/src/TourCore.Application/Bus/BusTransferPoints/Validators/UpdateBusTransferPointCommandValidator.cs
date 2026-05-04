using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Bus.BusTransferPoints.Commands;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Bus.BusTransferPoints.Validators
{
    public class UpdateBusTransferPointCommandValidator
    {
        public void ValidateAndThrow(UpdateBusTransferPointCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(UpdateBusTransferPointCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (command.Id <= 0)
                AddError(errors, "Id", ErrorCode.GreaterThanZero);

            if (command.BusTransferId <= 0)
                AddError(errors, "BusTransferId", ErrorCode.GreaterThanZero);

            if (command.CountryFromId <= 0)
                AddError(errors, "CountryFromId", ErrorCode.GreaterThanZero);

            if (command.CityFromId <= 0)
                AddError(errors, "CityFromId", ErrorCode.GreaterThanZero);

            if (command.CountryToId <= 0)
                AddError(errors, "CountryToId", ErrorCode.GreaterThanZero);

            if (command.CityToId <= 0)
                AddError(errors, "CityToId", ErrorCode.GreaterThanZero);

            if (command.CityFromId > 0 &&
                command.CityToId > 0 &&
                command.CityFromId == command.CityToId)
            {
                AddError(errors, "CityToId", ErrorCode.SameCities);
            }

            if (command.DayFrom.HasValue && command.DayFrom.Value < 0)
                AddError(errors, "DayFrom", ErrorCode.Negative);

            if (command.DayTo.HasValue && command.DayTo.Value < 0)
                AddError(errors, "DayTo", ErrorCode.Negative);

            return ToResult(errors);
        }

        private static void AddError(
            IDictionary<string, List<string>> errors,
            string field,
            string code)
        {
            if (!errors.ContainsKey(field))
                errors[field] = new List<string>();

            errors[field].Add(code);
        }

        private static IReadOnlyDictionary<string, string[]> ToResult(
            IDictionary<string, List<string>> errors)
        {
            return errors.ToDictionary(
                x => x.Key,
                x => x.Value.ToArray());
        }
    }
}