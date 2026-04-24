using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Contracts.Avia.CharterSeasons;
using TourCore.Application.CharterSeasons.Mappings;
using TourCore.Application.CharterSeasons.Queries;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Application.CharterSeasons.Handlers
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
                throw new NotFoundException("Charter season was not found.");

            return entity.ToDto();
        }
    }
}