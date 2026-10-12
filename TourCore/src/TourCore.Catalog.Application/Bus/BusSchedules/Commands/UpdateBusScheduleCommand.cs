namespace TourCore.Application.Bus.BusSchedules.Commands
{
    public class UpdateBusScheduleCommand : CreateBusScheduleCommand
    {
        public int Id { get; set; }
    }
}