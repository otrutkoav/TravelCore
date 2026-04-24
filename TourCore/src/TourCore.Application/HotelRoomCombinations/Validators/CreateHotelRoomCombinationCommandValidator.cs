using System.Collections.Generic;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.HotelRoomCombinations.Commands;

namespace TourCore.Application.HotelRoomCombinations.Validators
{
    public class CreateHotelRoomCombinationCommandValidator
    {
        public void ValidateAndThrow(CreateHotelRoomCombinationCommand command)
        {
            var errors = Validate(command);
            if (errors.Count > 0)
                throw new ValidationException(errors);
        }

        public IReadOnlyCollection<string> Validate(CreateHotelRoomCombinationCommand command)
        {
            var errors = new List<string>();

            if (command == null)
            {
                errors.Add("Command is required.");
                return errors;
            }

            if (command.RoomTypeId <= 0)
                errors.Add("RoomTypeId must be greater than 0.");

            if (command.RoomCategoryId <= 0)
                errors.Add("RoomCategoryId must be greater than 0.");

            if (command.AccommodationTypeId <= 0)
                errors.Add("AccommodationTypeId must be greater than 0.");

            if (command.AgeFrom.HasValue && command.AgeFrom.Value < 0)
                errors.Add("AgeFrom cannot be negative.");

            if (command.AgeTo.HasValue && command.AgeTo.Value < 0)
                errors.Add("AgeTo cannot be negative.");

            if (command.AgeFrom.HasValue &&
                command.AgeTo.HasValue &&
                command.AgeFrom.Value > command.AgeTo.Value)
            {
                errors.Add("AgeFrom cannot be greater than AgeTo.");
            }

            return errors;
        }
    }
}