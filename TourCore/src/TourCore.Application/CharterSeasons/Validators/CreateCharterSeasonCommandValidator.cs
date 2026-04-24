using System.Collections.Generic;
using TourCore.Application.CharterSeasons.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.CharterSeasons.Validators
{
    public class CreateCharterSeasonCommandValidator
    {
        public void ValidateAndThrow(CreateCharterSeasonCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateCharterSeasonCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.CharterId <= 0)
                errors.Add("CharterId must be greater than 0.");

            if (command.DateFrom.HasValue &&
                command.DateTo.HasValue &&
                command.DateFrom.Value.Date > command.DateTo.Value.Date)
            {
                errors.Add("DateFrom cannot be greater than DateTo.");
            }

            if (!string.IsNullOrWhiteSpace(command.Remark) && command.Remark.Trim().Length > 20)
                errors.Add("Remark must be 20 characters or less.");

            return errors;
        }
    }
}