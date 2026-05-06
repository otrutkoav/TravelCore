using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Geography.Countries.Queries
{
    internal static class CountrySortDefinition
    {
        public static readonly SortDefinition<Country> Instance =
            new SortDefinition<Country>(
                new SortExpression<Country>[]
                {
                    new SortExpression<Country, int>("id", x => x.Id),
                    new SortExpression<Country, string>("name", x => x.Name),
                    new SortExpression<Country, string>("nameEn", x => x.NameEn),
                    new SortExpression<Country, string>("code", x => x.Code),
                    new SortExpression<Country, string>("isoCode2", x => x.IsoCode2),
                    new SortExpression<Country, string>("isoCode3", x => x.IsoCode3),
                    new SortExpression<Country, string>("digitalCode", x => x.DigitalCode),
                    new SortExpression<Country, string>("citizenshipName", x => x.CitizenshipName),
                    new SortExpression<Country, string>("citizenshipNameEn", x => x.CitizenshipNameEn),
                    new SortExpression<Country, int>("sortOrder", x => x.SortOrder)
                },
                (Expression<Func<Country, int>>)(x => x.SortOrder));
    }
}