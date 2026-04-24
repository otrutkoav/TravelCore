using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.SeatingCells.DTOs;
using TourCore.Application.SeatingCells.Mappings;
using TourCore.Application.SeatingCells.Queries;

namespace TourCore.Application.SeatingCells.Handlers
{
    public class GetSeatingCellByIdHandler : IQueryHandler<GetSeatingCellByIdQuery, SeatingCellDto>
    {
        private readonly ISeatingCellRepository _seatingCellRepository;

        public GetSeatingCellByIdHandler(ISeatingCellRepository seatingCellRepository)
        {
            _seatingCellRepository = seatingCellRepository;
        }

        public async Task<SeatingCellDto> Handle(GetSeatingCellByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _seatingCellRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Seating cell was not found.");

            return entity.ToDto();
        }
    }
}