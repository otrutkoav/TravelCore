using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Countries.Commands;

namespace TourCore.Application.Countries.Validators
{
    /// <summary>
    /// Валидатор команды создания страны.
    /// </summary>
    public class CreateCountryCommandValidator
    {
        public void ValidateAndThrow(CreateCountryCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(CreateCountryCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (string.IsNullOrWhiteSpace(command.Name))
                AddError(errors, "Name", ErrorCode.Required);
            else if (command.Name.Trim().Length > 25)
                AddError(errors, "Name", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 25)
                AddError(errors, "NameEn", ErrorCode.MaxLength);

            if (string.IsNullOrWhiteSpace(command.Code))
                AddError(errors, "Code", ErrorCode.Required);
            else if (command.Code.Trim().Length > 3)
                AddError(errors, "Code", ErrorCode.MaxLength);

            if (string.IsNullOrWhiteSpace(command.IsoCode2))
                AddError(errors, "IsoCode2", ErrorCode.Required);
            else if (command.IsoCode2.Trim().Length != 2)
                AddError(errors, "IsoCode2", ErrorCode.ExactLength);

            if (string.IsNullOrWhiteSpace(command.IsoCode3))
                AddError(errors, "IsoCode3", ErrorCode.Required);
            else if (command.IsoCode3.Trim().Length != 3)
                AddError(errors, "IsoCode3", ErrorCode.ExactLength);

            if (command.SortOrder < 0)
                AddError(errors, "SortOrder", ErrorCode.Negative);

            if (!string.IsNullOrWhiteSpace(command.DigitalCode) && command.DigitalCode.Trim().Length > 3)
                AddError(errors, "DigitalCode", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.CitizenshipName) && command.CitizenshipName.Trim().Length > 50)
                AddError(errors, "CitizenshipName", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.CitizenshipNameEn) && command.CitizenshipNameEn.Trim().Length > 50)
                AddError(errors, "CitizenshipNameEn", ErrorCode.MaxLength);

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