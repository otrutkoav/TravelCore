using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Finance.Rates;
using TourCore.Application.Rates.Mappings;
using TourCore.Application.Rates.Queries;

namespace TourCore.Application.Rates.Handlers
{
    public class GetRateByIdHandler : IQueryHandler<GetRateByIdQuery, RateDto>
    {
        private readonly IRateRepository _rateRepository;

        public GetRateByIdHandler(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public async Task<RateDto> Handle(GetRateByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _rateRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException("Rate was not found.");

            return entity.ToDto();
        }
    }
}