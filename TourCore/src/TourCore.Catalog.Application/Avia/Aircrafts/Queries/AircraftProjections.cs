using System;
using System.Linq.Expressions;
using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.Aircrafts.Queries
{
    internal static class AircraftProjections
    {
        public static readonly Expression<Func<Aircraft, AircraftListItemDto>> ListItem =
            x => new AircraftListItemDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                NameEn = x.NameEn
            };
    }
}