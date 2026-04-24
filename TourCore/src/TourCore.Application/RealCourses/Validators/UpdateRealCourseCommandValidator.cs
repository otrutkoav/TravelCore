using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.RealCourses.Commands;

namespace TourCore.Application.RealCourses.Validators
{
    public class UpdateRealCourseCommandValidator
    {
        public void ValidateAndThrow(UpdateRealCourseCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateRealCourseCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            if (string.IsNullOrWhiteSpace(command.FromRateCode))
                errors.Add("FromRateCode is required.");
            else if (command.FromRateCode.Trim().Length > 3)
                errors.Add("FromRateCode must be 3 characters or less.");

            if (string.IsNullOrWhiteSpace(command.ToRateCode))
                errors.Add("ToRateCode is required.");
            else if (command.ToRateCode.Trim().Length > 3)
                errors.Add("ToRateCode must be 3 characters or less.");

            if (command.Course.HasValue && command.Course.Value < 0)
                errors.Add("Course cannot be negative.");

            if (command.CentralBankCourse.HasValue && command.CentralBankCourse.Value < 0)
                errors.Add("CentralBankCourse cannot be negative.");

            if (command.DateBeg.HasValue && command.DateEnd.HasValue &&
                command.DateBeg.Value.Date > command.DateEnd.Value.Date)
            {
                errors.Add("DateBeg cannot be greater than DateEnd.");
            }

            return errors;
        }
    }
}