using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Hotels.AccommodationTypes.Commands;

namespace TourCore.Application.Hotels.AccommodationTypes.Validators
{
    public class CreateAccommodationTypeCommandValidator
    {
        public void ValidateAndThrow(CreateAccommodationTypeCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(CreateAccommodationTypeCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (string.IsNullOrWhiteSpace(command.Code))
                AddError(errors, "Code", ErrorCode.Required);
            else if (command.Code.Trim().Length > 50)
                AddError(errors, "Code", ErrorCode.MaxLength);

            if (string.IsNullOrWhiteSpace(command.Name))
                AddError(errors, "Name", ErrorCode.Required);
            else if (command.Name.Trim().Length > 100)
                AddError(errors, "Name", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                AddError(errors, "NameEn", ErrorCode.MaxLength);

            if (command.AgeFrom.HasValue && command.AgeFrom.Value < 0)
                AddError(errors, "AgeFrom", ErrorCode.Negative);

            if (command.AgeTo.HasValue && command.AgeTo.Value < 0)
                AddError(errors, "AgeTo", ErrorCode.Negative);

            if (command.AgeFrom.HasValue &&
                command.AgeTo.HasValue &&
                command.AgeFrom.Value > command.AgeTo.Value)
            {
                AddError(errors, "AgeFrom", ErrorCode.AgeRangeInvalid);
            }

            if (command.PerRoom.HasValue && command.PerRoom.Value < 0)
                AddError(errors, "PerRoom", ErrorCode.Negative);

            if (command.SortOrder < 0)
                AddError(errors, "SortOrder", ErrorCode.Negative);

            if (command.MainPlacementRuleId.HasValue && command.MainPlacementRuleId.Value <= 0)
                AddError(errors, "MainPlacementRuleId", ErrorCode.GreaterThanZero);

            if (command.ExtraPlacementRuleId.HasValue && command.ExtraPlacementRuleId.Value <= 0)
                AddError(errors, "ExtraPlacementRuleId", ErrorCode.GreaterThanZero);

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