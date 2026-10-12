using System.Collections.Generic;
using System.Linq;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Finance.RealCourses.Commands;

namespace TourCore.Application.Finance.RealCourses.Validators
{
    public class UpdateRealCourseCommandValidator
    {
        public void ValidateAndThrow(UpdateRealCourseCommand command)
        {
            var errors = Validate(command);

            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyDictionary<string, string[]> Validate(UpdateRealCourseCommand command)
        {
            var errors = new Dictionary<string, List<string>>();

            if (command == null)
            {
                AddError(errors, "General", ErrorCode.Required);
                return ToResult(errors);
            }

            if (command.Id <= 0)
                AddError(errors, "Id", ErrorCode.GreaterThanZero);

            if (string.IsNullOrWhiteSpace(command.FromRateCode))
                AddError(errors, "FromRateCode", ErrorCode.Required);
            else if (command.FromRateCode.Trim().Length > 3)
                AddError(errors, "FromRateCode", ErrorCode.MaxLength);

            if (string.IsNullOrWhiteSpace(command.ToRateCode))
                AddError(errors, "ToRateCode", ErrorCode.Required);
            else if (command.ToRateCode.Trim().Length > 3)
                AddError(errors, "ToRateCode", ErrorCode.MaxLength);

            if (command.Course.HasValue && command.Course.Value < 0)
                AddError(errors, "Course", ErrorCode.Negative);

            if (command.CentralBankCourse.HasValue && command.CentralBankCourse.Value < 0)
                AddError(errors, "CentralBankCourse", ErrorCode.Negative);

            if (command.DateBeg.HasValue &&
                command.DateEnd.HasValue &&
                command.DateBeg.Value.Date > command.DateEnd.Value.Date)
            {
                AddError(errors, "DateBeg", ErrorCode.DateRangeInvalid);
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