namespace TourCore.Application.Services.ServiceTypes.Commands
{
    public class DeleteServiceTypeCommand
    {
        public DeleteServiceTypeCommand(int id) { Id = id; }
        public int Id { get; }
    }
}
