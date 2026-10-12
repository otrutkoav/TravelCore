namespace TourCore.Application.Services.ServiceTypes.Commands
{
    public class UpdateServiceTypeCommand
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public bool IsDuration { get; set; }
        public bool IsRoute { get; set; }
    }
}
