using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.Hotels.Queries
{
    internal static class HotelSortDefinition
    {
        public static readonly SortDefinition<Hotel> Instance =
            new SortDefinition<Hotel>(
                new SortExpression<Hotel>[]
                {
                    new SortExpression<Hotel, int>("id", x => x.Id),
                    new SortExpression<Hotel, int>("countryId", x => x.CountryId),
                    new SortExpression<Hotel, int>("cityId", x => x.CityId),
                    new SortExpression<Hotel, int?>("resortId", x => x.ResortId),
                    new SortExpression<Hotel, int?>("categoryId", x => x.CategoryId),
                    new SortExpression<Hotel, string>("name", x => x.Name),
                    new SortExpression<Hotel, string>("nameEn", x => x.NameEn),
                    new SortExpression<Hotel, string>("stars", x => x.Stars),
                    new SortExpression<Hotel, string>("code", x => x.Code),
                    new SortExpression<Hotel, string>("address", x => x.Address),
                    new SortExpression<Hotel, string>("phone", x => x.Phone),
                    new SortExpression<Hotel, string>("fax", x => x.Fax),
                    new SortExpression<Hotel, string>("email", x => x.Email),
                    new SortExpression<Hotel, string>("website", x => x.Website),
                    new SortExpression<Hotel, string>("latitude", x => x.Latitude),
                    new SortExpression<Hotel, string>("longitude", x => x.Longitude),
                    new SortExpression<Hotel, bool>("isCruise", x => x.IsCruise),
                    new SortExpression<Hotel, int>("sortOrder", x => x.SortOrder),
                    new SortExpression<Hotel, int?>("rank", x => x.Rank)
                },
                (Expression<Func<Hotel, int>>)(x => x.SortOrder));
    }
}