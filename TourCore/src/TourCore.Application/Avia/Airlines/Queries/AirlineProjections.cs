using System;
using System.Linq.Expressions;
using TourCore.Contracts.Avia.Airlines;
using TourCore.Domain.Avia.Entities;

namespace TourCore.Application.Avia.Airlines.Queries
{
    internal static class AirlineProjections
    {
        public static readonly Expression<Func<Airline, AirlineListItemDto>> ListItem =
            x => new AirlineListItemDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                NameEn = x.NameEn,
                IcaoCode = x.IcaoCode
            };
    }
}