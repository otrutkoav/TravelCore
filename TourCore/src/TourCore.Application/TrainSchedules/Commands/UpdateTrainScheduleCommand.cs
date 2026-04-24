using System;

namespace TourCore.Application.TrainSchedules.Commands
{
    public class UpdateTrainScheduleCommand : CreateTrainScheduleCommand
    {
        public int Id { get; set; }
    }
}