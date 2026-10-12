using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Services.Entities;

namespace TourCore.Application.Services.ServiceTypes.Queries
{
    internal static class ServiceTypeSortDefinition
    {
        public static readonly SortDefinition<ServiceType> Instance =
            new SortDefinition<ServiceType>(
                new SortExpression<ServiceType>[]
                {
                    new SortExpression<ServiceType, int>("id", x => x.Id),
                    new SortExpression<ServiceType, string>("name", x => x.Name),
                    new SortExpression<ServiceType, string>("nameEn", x => x.NameEn),
                    new SortExpression<ServiceType, string>("code", x => x.Code),
                    new SortExpression<ServiceType, bool>("isDuration", x => x.IsDuration),
                    new SortExpression<ServiceType, bool>("isRoute", x => x.IsRoute)
                },
                (Expression<Func<ServiceType, string>>)(x => x.Name));
    }
}
