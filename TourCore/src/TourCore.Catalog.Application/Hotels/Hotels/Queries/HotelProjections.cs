using System;
using System.Linq.Expressions;
using TourCore.Contracts.Hotels.Hotels;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Application.Hotels.Hotels.Queries
{
    internal static class HotelProjections
    {
        public static readonly Expression<Func<Hotel, HotelListItemDto>> ListItem =
            x => new HotelListItemDto
            {
                Id = x.Id,
                CountryId = x.CountryId,
                CityId = x.CityId,
                ResortId = x.ResortId,
                CategoryId = x.CategoryId,
                Name = x.Name,
                NameEn = x.NameEn,
                Stars = x.Stars,
                Code = x.Code,
                Address = x.Address,
                Phone = x.Phone,
                Fax = x.Fax,
                Email = x.Email,
                Website = x.Website,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                IsCruise = x.IsCruise,
                SortOrder = x.SortOrder,
                Rank = x.Rank
            };
    }
}