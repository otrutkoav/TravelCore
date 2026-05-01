using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Resorts.Mappings;
using TourCore.Application.Resorts.Queries;
using TourCore.Contracts.Geography.Resorts;

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
                throw new NotFoundException(ErrorMessages.ResortNotFound, ErrorCode.ResortNotFound);

            return entity.ToDto();
        }
    }
}