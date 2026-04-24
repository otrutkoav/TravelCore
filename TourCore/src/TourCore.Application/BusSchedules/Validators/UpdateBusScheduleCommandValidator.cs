using System.Collections.Generic;
using TourCore.Application.BusSchedules.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.BusSchedules.Validators
{
    public class UpdateBusScheduleCommandValidator
    {
        public void ValidateAndThrow(UpdateBusScheduleCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateBusScheduleCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.Id <= 0)
                errors.Add("Id must be greater than 0.");

            if (command.BusTransferId <= 0)
                errors.Add("BusTransferId must be greater than 0.");

            if (command.DateFrom.HasValue &&
                command.DateTo.HasValue &&
                command.DateFrom.Value.Date > command.DateTo.Value.Date)
            {
                errors.Add("DateFrom cannot be greater than DateTo.");
            }

            if (command.DaysOnRoad.HasValue && command.DaysOnRoad.Value < 0)
                errors.Add("DaysOnRoad cannot be negative.");

            return errors;
        }
    }
}