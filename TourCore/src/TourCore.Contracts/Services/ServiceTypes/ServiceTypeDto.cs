using System;

namespace TourCore.Contracts.Services.ServiceTypes
{
    /// <summary>Полные данные типа услуги.</summary>
    public class ServiceTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public bool IsDuration { get; set; }
        public bool IsRoute { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
