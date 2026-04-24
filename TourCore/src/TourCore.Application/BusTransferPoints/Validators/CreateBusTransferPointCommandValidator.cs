using System.Collections.Generic;
using TourCore.Application.BusTransferPoints.Commands;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.BusTransferPoints.Validators
{
    public class CreateBusTransferPointCommandValidator
    {
        public void ValidateAndThrow(CreateBusTransferPointCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateBusTransferPointCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.BusTransferId <= 0)
                errors.Add("BusTransferId must be greater than 0.");

            if (command.CountryFromId <= 0)
                errors.Add("CountryFromId must be greater than 0.");

            if (command.CityFromId <= 0)
                errors.Add("CityFromId must be greater than 0.");

            if (command.CountryToId <= 0)
                errors.Add("CountryToId must be greater than 0.");

            if (command.CityToId <= 0)
                errors.Add("CityToId must be greater than 0.");

            if (command.CityFromId > 0 &&
                command.CityToId > 0 &&
                command.CityFromId == command.CityToId)
            {
                errors.Add("CityFromId and CityToId must be different.");
            }

            if (command.DayFrom.HasValue && command.DayFrom.Value < 0)
                errors.Add("DayFrom cannot be negative.");

            if (command.DayTo.HasValue && command.DayTo.Value < 0)
                errors.Add("DayTo cannot be negative.");

            return errors;
        }
    }
}