using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Railway.RailwayTransfers;
using TourCore.Application.RailwayTransfers.Mappings;
using TourCore.Application.RailwayTransfers.Queries;

namespace TourCore.Application.RailwayTransfers.Handlers
{
    public class GetRailwayTransferByIdHandler : IQueryHandler<GetRailwayTransferByIdQuery, RailwayTransferDto>
    {
        private readonly IRailwayTransferRepository _railwayTransferRepository;

        public GetRailwayTransferByIdHandler(IRailwayTransferRepository railwayTransferRepository)
        {
            _railwayTransferRepository = railwayTransferRepository;
        }

        public async Task<RailwayTransferDto> Handle(GetRailwayTransferByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _railwayTransferRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Railway transfer was not found.");

            return entity.ToDto();
        }
    }
}