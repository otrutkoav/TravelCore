namespace TourCore.Application.ServiceTypes.DTOs
{
    public class ServiceTypeListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }

        public bool HasPrimaryParameter { get; set; }
        public bool HasSecondaryParameter { get; set; }

        public bool IsRoute { get; set; }
    }
}