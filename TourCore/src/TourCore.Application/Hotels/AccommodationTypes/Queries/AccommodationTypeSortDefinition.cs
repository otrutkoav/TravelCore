using System;
using System.Linq.Expressions;
using TourCore.Application.Common.Queries;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.AccommodationTypes.Queries
{
    internal static class AccommodationTypeSortDefinition
    {
        public static readonly SortDefinition<AccommodationType> Instance =
            new SortDefinition<AccommodationType>(
                new SortExpression<AccommodationType>[]
                {
                    new SortExpression<AccommodationType, int>("id", x => x.Id),
                    new SortExpression<AccommodationType, string>("code", x => x.Code),
                    new SortExpression<AccommodationType, string>("name", x => x.Name),
                    new SortExpression<AccommodationType, string>("nameEn", x => x.NameEn),
                    new SortExpression<AccommodationType, bool>("isMain", x => x.IsMain),
                    new SortExpression<AccommodationType, short?>("ageFrom", x => x.AgeFrom),
                    new SortExpression<AccommodationType, short?>("ageTo", x => x.AgeTo),
                    new SortExpression<AccommodationType, short?>("perRoom", x => x.PerRoom),
                    new SortExpression<AccommodationType, int>("sortOrder", x => x.SortOrder),
                    new SortExpression<AccommodationType, string>("description", x => x.Description),
                    new SortExpression<AccommodationType, int?>("mainPlacementRuleId", x => x.MainPlacementRuleId),
                    new SortExpression<AccommodationType, int?>("extraPlacementRuleId", x => x.ExtraPlacementRuleId)
                },
                (Expression<Func<AccommodationType, int>>)(x => x.SortOrder));
    }
}