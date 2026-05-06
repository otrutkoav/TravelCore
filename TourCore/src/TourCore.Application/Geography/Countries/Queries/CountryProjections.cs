using System;
using System.Linq.Expressions;
using TourCore.Contracts.Geography.Countries;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Application.Geography.Countries.Queries
{
    internal static class CountryProjections
    {
        public static readonly Expression<Func<Country, CountryListItemDto>> ListItem =
            x => new CountryListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                NameEn = x.NameEn,
                Code = x.Code,
                IsoCode2 = x.IsoCode2,
                IsoCode3 = x.IsoCode3,
                DigitalCode = x.DigitalCode,
                CitizenshipName = x.CitizenshipName,
                CitizenshipNameEn = x.CitizenshipNameEn,
                SortOrder = x.SortOrder
            };
    }
}