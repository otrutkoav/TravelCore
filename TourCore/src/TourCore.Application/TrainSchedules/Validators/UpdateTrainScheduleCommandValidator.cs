using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.TrainSchedules.Commands;

namespace TourCore.Application.TrainSchedules.Validators
{
    public class UpdateTrainScheduleCommandValidator
    {
        public void ValidateAndThrow(UpdateTrainScheduleCommand cmd)
        {
            var errors = Validate(cmd);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(UpdateTrainScheduleCommand cmd)
        {
            var errors = new List<string>();

            if (cmd.Id <= 0)
                errors.Add("Id must be greater than 0.");

            if (cmd.RailwayTransferId <= 0)
                errors.Add("RailwayTransferId must be greater than 0.");

            return errors;
        }
    }
}