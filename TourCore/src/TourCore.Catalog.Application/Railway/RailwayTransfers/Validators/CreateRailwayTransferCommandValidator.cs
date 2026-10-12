using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Railway.RailwayTransfers.Commands;

namespace TourCore.Application.Railway.RailwayTransfers.Validators
{
    public class CreateRailwayTransferCommandValidator
    {
        public void ValidateAndThrow(CreateRailwayTransferCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(CreateRailwayTransferCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (string.IsNullOrWhiteSpace(command.Name))
                AddError(errors, "Name", ErrorCode.Required);
            else if (command.Name.Trim().Length > 100)
                AddError(errors, "Name", ErrorCode.MaxLength);

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