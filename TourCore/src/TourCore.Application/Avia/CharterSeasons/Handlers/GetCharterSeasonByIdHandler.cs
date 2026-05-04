using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Avia.CharterSeasons.Mappings;
using TourCore.Application.Avia.CharterSeasons.Queries;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Avia.CharterSeasons;

namespace TourCore.Application.Avia.CharterSeasons.Handlers
{
    public class GetCharterSeasonByIdHandler : IQueryHandler<GetCharterSeasonByIdQuery, CharterSeasonDto>
    {
        private readonly ICharterSeasonRepository _charterSeasonRepository;

        public GetCharterSeasonByIdHandler(ICharterSeasonRepository charterSeasonRepository)
        {
            _charterSeasonRepository = charterSeasonRepository;
        }

        public async Task<CharterSeasonDto> Handle(GetCharterSeasonByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _charterSeasonRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.CharterNotFound, ErrorCode.CharterNotFound);

            return entity.ToDto();
        }
    }
}