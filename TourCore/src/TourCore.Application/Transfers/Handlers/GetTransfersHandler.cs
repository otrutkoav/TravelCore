using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Models;
using TourCore.Application.Transfers.Mappings;
using TourCore.Application.Transfers.Queries;
using TourCore.Contracts.Transfers.Transfers;

namespace TourCore.Application.Transfers.Handlers
{
    public class GetTransfersHandler : IQueryHandler<GetTransfersQuery, ListResult<TransferListItemDto>>
    {
        private readonly ITransferRepository _transferRepository;

        public GetTransfersHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public async Task<ListResult<TransferListItemDto>> Handle(GetTransfersQuery query, CancellationToken cancellationToken)
        {
            var entities = await _transferRepository.ListAsync(cancellationToken);
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

                if (query.Filter.CityId.HasValue)
                    items = items.Where(x => x.CityId == query.Filter.CityId.Value);

                if (query.Filter.DirectionId.HasValue)
                    items = items.Where(x => x.DirectionId == query.Filter.DirectionId.Value);

                if (query.Filter.IsMain.HasValue)
                    items = items.Where(x => x.IsMain == query.Filter.IsMain.Value);
            }

            var result = items
                .OrderBy(x => x.ShowOrder)
                .ThenBy(x => x.Name)
                .Select(x => x.ToListItemDto())
                .ToArray();

            return ListResult<TransferListItemDto>.Create(result);
        }
    }
}