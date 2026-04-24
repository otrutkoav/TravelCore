using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Geography.Resorts;
using TourCore.Application.Resorts.Mappings;
using TourCore.Application.Resorts.Queries;

namespace TourCore.Application.Resorts.Handlers
{
    public class GetResortByIdHandler : IQueryHandler<GetResortByIdQuery, ResortDto>
    {
        private readonly IResortRepository _resortRepository;

        public GetResortByIdHandler(IResortRepository resortRepository)
        {
            _resortRepository = resortRepository;
        }

        public async Task<ResortDto> Handle(GetResortByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _resortRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Resort was not found.");

            return entity.ToDto();
        }
    }
}