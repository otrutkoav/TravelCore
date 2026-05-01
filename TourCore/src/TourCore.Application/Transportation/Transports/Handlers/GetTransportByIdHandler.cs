using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Transportation.Transports;
using TourCore.Application.Abstractions.Persistence.Transportation;
using TourCore.Application.Transportation.Transports.Mappings;
using TourCore.Application.Transportation.Transports.Queries;

namespace TourCore.Application.Transportation.Transports.Handlers
{
    public class GetTransportByIdHandler : IQueryHandler<GetTransportByIdQuery, TransportDto>
    {
        private readonly ITransportRepository _transportRepository;

        public GetTransportByIdHandler(ITransportRepository transportRepository)
        {
            _transportRepository = transportRepository;
        }

        public async Task<TransportDto> Handle(GetTransportByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _transportRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Transport was not found.");

            return entity.ToDto();
        }
    }
}