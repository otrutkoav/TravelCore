using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Transfers.Transfers.Commands;

namespace TourCore.Application.Transfers.Transfers.Validators
{
    public class CreateTransferCommandValidator
    {
        public void ValidateAndThrow(CreateTransferCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(CreateTransferCommand command)
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

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                AddError(errors, "NameEn", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.DurationText) && command.DurationText.Trim().Length > 5)
                AddError(errors, "DurationText", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.PlaceFrom) && command.PlaceFrom.Trim().Length > 300)
                AddError(errors, "PlaceFrom", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.PlaceTo) && command.PlaceTo.Trim().Length > 300)
                AddError(errors, "PlaceTo", ErrorCode.MaxLength);

            if (command.CityId.HasValue && command.CityId.Value <= 0)
                AddError(errors, "CityId", ErrorCode.GreaterThanZero);

            if (command.DirectionId.HasValue && command.DirectionId.Value <= 0)
                AddError(errors, "DirectionId", ErrorCode.GreaterThanZero);

            if (!string.IsNullOrWhiteSpace(command.Url) && command.Url.Trim().Length > 192)
                AddError(errors, "Url", ErrorCode.MaxLength);

            if (command.ShowOrder < 0)
                AddError(errors, "ShowOrder", ErrorCode.Negative);

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