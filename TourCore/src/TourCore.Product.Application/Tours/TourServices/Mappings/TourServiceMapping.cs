using TourCore.Contracts.Tours;
using TourCore.Domain.Tours;

namespace TourCore.Application.Tours
{
    internal static class TourServiceMapping
    {
        public static TourServiceDto ToDto(this TourService x) =>
            new TourServiceDto
            {
                Id = x.Id,
                TourId = x.TourId,
                ServiceTypeId = x.ServiceTypeId,
                Name = x.Name,
                OrderIndex = x.OrderIndex,
                ParentServiceId = x.ParentServiceId,
                IsHidden = x.IsHidden,
                IsRemovable = x.IsRemovable,
                IsOptional = x.IsOptional,
                Revision = x.Revision,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
            };
    }
}
