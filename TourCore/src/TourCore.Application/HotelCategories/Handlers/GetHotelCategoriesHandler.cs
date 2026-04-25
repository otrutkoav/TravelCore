using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Hotels.HotelCategories;
using TourCore.Application.HotelCategories.Mappings;
using TourCore.Application.HotelCategories.Queries;

namespace TourCore.Application.HotelCategories.Handlers
{
    public class GetHotelCategoriesHandler : IQueryHandler<GetHotelCategoriesQuery, ListResult<HotelCategoryListItemDto>>
    {
        private readonly IHotelCategoryRepository _hotelCategoryRepository;

        public GetHotelCategoriesHandler(IHotelCategoryRepository hotelCategoryRepository)
        {
            _hotelCategoryRepository = hotelCategoryRepository;
        }

        public async Task<ListResult<HotelCategoryListItemDto>> Handle(GetHotelCategoriesQuery query, CancellationToken cancellationToken)
        {
            var entities = await _hotelCategoryRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null && !string.IsNullOrWhiteSpace(query.Filter.Search))
            {
                var search = query.Filter.Search.Trim();

                items = items.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.Name) && x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.NameEn) && x.NameEn.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrWhiteSpace(x.GlobalCode) && x.GlobalCode.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            var result = items
                .OrderBy(x => x.PrintOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<HotelCategoryListItemDto>.Create(result);
        }
    }
}