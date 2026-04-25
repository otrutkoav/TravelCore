using TourCore.Contracts.Finance.Rates;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Application.Rates.Mappings
{
    public static class RateMappingExtensions
    {
        public static RateDto ToDto(this Rate entity)
        {
            return new RateDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                IsoCode = entity.IsoCode,
                IsMain = entity.IsMain,
                IsNational = entity.IsNational,
                ShowInSearch = entity.ShowInSearch,
                Symbol = entity.Symbol
            };
        }

        public static RateListItemDto ToListItemDto(this Rate entity)
        {
            return new RateListItemDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                IsoCode = entity.IsoCode,
                IsMain = entity.IsMain,
                IsNational = entity.IsNational,
                ShowInSearch = entity.ShowInSearch
            };
        }
    }
}