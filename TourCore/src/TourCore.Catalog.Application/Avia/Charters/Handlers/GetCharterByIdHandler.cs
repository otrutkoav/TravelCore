using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Contracts.Avia.Charters;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.Charters.Mappings;
using TourCore.Application.Avia.Charters.Queries;

namespace TourCore.Application.Avia.Charters.Handlers
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
                throw new NotFoundException(ErrorMessages.CharterNotFound, ErrorCode.CharterNotFound);

            return entity.ToDto();
        }
    }
}