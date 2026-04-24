using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.BusTransfers.DTOs;
using TourCore.Application.BusTransfers.Mappings;
using TourCore.Application.BusTransfers.Queries;
using TourCore.Application.Common.Models;

namespace TourCore.Application.BusTransfers.Handlers
{
    public class GetBusTransfersHandler : IQueryHandler<GetBusTransfersQuery, ListResult<BusTransferListItemDto>>
    {
        private readonly IBusTransferRepository _busTransferRepository;

        public GetBusTransfersHandler(IBusTransferRepository busTransferRepository)
        {
            _busTransferRepository = busTransferRepository;
        }

        public async Task<ListResult<BusTransferListItemDto>> Handle(GetBusTransfersQuery query, CancellationToken cancellationToken)
        {
            var entities = await _busTransferRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
                if (!string.IsNullOrWhiteSpace(query.Filter.Search))
                {
                    var search = query.Filter.Search.Trim();

                    items = items.Where(x =>
                        !string.IsNullOrWhiteSpace(x.Name) &&
                        x.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                if (query.Filter.CountryFromId.HasValue)
                    items = items.Where(x => x.CountryFromId == query.Filter.CountryFromId.Value);

                if (query.Filter.CityFromId.HasValue)
                    items = items.Where(x => x.CityFromId == query.Filter.CityFromId.Value);

                if (query.Filter.CountryToId.HasValue)
                    items = items.Where(x => x.CountryToId == query.Filter.CountryToId.Value);

                if (query.Filter.CityToId.HasValue)
                    items = items.Where(x => x.CityToId == query.Filter.CityToId.Value);
            }

            var result = items
                .OrderBy(x => x.CountryFromId)
                .ThenBy(x => x.CityFromId)
                .ThenBy(x => x.CountryToId)
                .ThenBy(x => x.CityToId)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<BusTransferListItemDto>.Create(result);
        }
    }
}