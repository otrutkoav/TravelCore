using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Railway.RailwayTransfers;
using TourCore.Application.RailwayTransfers.Mappings;
using TourCore.Application.RailwayTransfers.Queries;

namespace TourCore.Application.RailwayTransfers.Handlers
{
    public class GetRailwayTransfersHandler : IQueryHandler<GetRailwayTransfersQuery, ListResult<RailwayTransferListItemDto>>
    {
        private readonly IRailwayTransferRepository _railwayTransferRepository;

        public GetRailwayTransfersHandler(IRailwayTransferRepository railwayTransferRepository)
        {
            _railwayTransferRepository = railwayTransferRepository;
        }

        public async Task<ListResult<RailwayTransferListItemDto>> Handle(GetRailwayTransfersQuery query, CancellationToken cancellationToken)
        {
            var entities = await _railwayTransferRepository.ListAsync(cancellationToken);
            var items = entities.AsEnumerable();

            if (query != null && query.Filter != null)
            {
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

            return ListResult<RailwayTransferListItemDto>.Create(result);
        }
    }
}