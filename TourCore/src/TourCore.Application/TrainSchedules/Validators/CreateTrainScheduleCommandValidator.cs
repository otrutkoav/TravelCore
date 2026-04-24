using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.TrainSchedules.Commands;

namespace TourCore.Application.TrainSchedules.Validators
{
    public class CreateTrainScheduleCommandValidator
    {
        public void ValidateAndThrow(CreateTrainScheduleCommand cmd)
        {
            var errors = Validate(cmd);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateTrainScheduleCommand cmd)
        {
            var errors = new List<string>();

            if (cmd.RailwayTransferId <= 0)
                errors.Add("RailwayTransferId must be greater than 0.");

            if (cmd.DaysOnRoad.HasValue && cmd.DaysOnRoad < 0)
                errors.Add("DaysOnRoad cannot be negative.");

            if (!string.IsNullOrWhiteSpace(cmd.Remark) && cmd.Remark.Length > 100)
                errors.Add("Remark too long.");

            return errors;
        }
    }
}