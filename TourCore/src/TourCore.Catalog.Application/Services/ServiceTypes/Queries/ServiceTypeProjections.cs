using System;
using System.Linq.Expressions;
using TourCore.Contracts.Services.ServiceTypes;
using TourCore.Domain.Services.Entities;

namespace TourCore.Application.Services.ServiceTypes.Queries
{
    internal static class ServiceTypeProjections
    {
        public static readonly Expression<Func<ServiceType, ServiceTypeListItemDto>> ListItem =
            x => new ServiceTypeListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                NameEn = x.NameEn,
                Code = x.Code,
                IsDuration = x.IsDuration,
                IsRoute = x.IsRoute
            };
    }
}
