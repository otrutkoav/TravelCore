namespace TourCore.Application.Services.ServiceTypes.Commands
{
    public class CreateServiceTypeCommand
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public bool IsDuration { get; set; }
        public bool IsRoute { get; set; }
    }
}
