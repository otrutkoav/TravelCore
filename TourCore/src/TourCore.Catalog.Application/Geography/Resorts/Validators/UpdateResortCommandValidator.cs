using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Geography.Resorts.Commands;

namespace TourCore.Application.Geography.Resorts.Validators
{
    /// <summary>
    /// Валидатор команды обновления курорта.
    /// </summary>
    public class UpdateResortCommandValidator
    {
        public void ValidateAndThrow(UpdateResortCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(UpdateResortCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (command.Id <= 0)
                AddError(errors, "Id", ErrorCode.GreaterThanZero);

            if (command.CountryId <= 0)
                AddError(errors, "CountryId", ErrorCode.GreaterThanZero);

            if (string.IsNullOrWhiteSpace(command.Name))
                AddError(errors, "Name", ErrorCode.Required);
            else if (command.Name.Trim().Length > 100)
                AddError(errors, "Name", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                AddError(errors, "NameEn", ErrorCode.MaxLength);

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