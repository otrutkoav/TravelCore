using System;

namespace TourCore.Application.Railway.TrainSchedules.Commands
{
    public class UpdateTrainScheduleCommand : CreateTrainScheduleCommand
    {
        public int Id { get; set; }
    }
}