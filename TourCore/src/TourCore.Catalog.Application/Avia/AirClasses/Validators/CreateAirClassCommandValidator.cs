using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Avia.AirClasses.Commands;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Avia.AirClasses.Validators
{
    public class CreateAirClassCommandValidator
    {
        public void ValidateAndThrow(CreateAirClassCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(CreateAirClassCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (string.IsNullOrWhiteSpace(command.Code))
                AddError(errors, "Code", ErrorCode.Required);
            else if (command.Code.Trim().Length > 3)
                AddError(errors, "Code", ErrorCode.MaxLength);

            if (string.IsNullOrWhiteSpace(command.Name))
                AddError(errors, "Name", ErrorCode.Required);
            else if (command.Name.Trim().Length > 100)
                AddError(errors, "Name", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.NameEn) && command.NameEn.Trim().Length > 100)
                AddError(errors, "NameEn", ErrorCode.MaxLength);

            if (!string.IsNullOrWhiteSpace(command.Group) && command.Group.Trim().Length > 128)
                AddError(errors, "Group", ErrorCode.MaxLength);

            if (command.SortOrder < 0)
                AddError(errors, "SortOrder", ErrorCode.Negative);

            return ToResult(errors);
        }

        private static void AddError(IDictionary<string, List<string>> errors, string field, string code)
        {
            if (!errors.ContainsKey(field))
                errors[field] = new List<string>();

            errors[field].Add(code);
        }

        private static IReadOnlyDictionary<string, string[]> ToResult(IDictionary<string, List<string>> errors)
        {
            return errors.ToDictionary(x => x.Key, x => x.Value.ToArray());
        }
    }
}