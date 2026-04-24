using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Avia.Charters;
using TourCore.Application.Charters.Mappings;
using TourCore.Application.Charters.Queries;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.Charters.Handlers
{
    public class GetCharterByIdHandler : IQueryHandler<GetCharterByIdQuery, CharterDto>
    {
        private readonly ICharterRepository _charterRepository;

        public GetCharterByIdHandler(ICharterRepository charterRepository)
        {
            _charterRepository = charterRepository;
        }

        public async Task<CharterDto> Handle(GetCharterByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _charterRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Charter was not found.");

            return entity.ToDto();
        }
    }
}