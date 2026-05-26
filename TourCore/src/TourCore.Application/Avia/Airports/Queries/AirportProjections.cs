using System;
using System.Linq.Expressions;
using TourCore.Contracts.Avia.Airports;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.Airports.Queries
{
    internal static class AirportProjections
    {
        public static readonly Expression<Func<Airport, AirportListItemDto>> ListItem =
            x => new AirportListItemDto
            {
                Id = x.Id,
                CityId = x.CityId,
                Code = x.Code,
                Name = x.Name,
                NameEn = x.NameEn,
                IcaoCode = x.IcaoCode,
                LetterCode = x.LetterCode
            };
    }
}